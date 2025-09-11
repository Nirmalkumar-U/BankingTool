using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace BankingTool.Api;
public static class Program
{
    public static void Main(string[] args)
    {
        var (startup, builder) = ConfigureServices(args);
        var app = builder.Build();
        ConfigureMiddleware(app, startup);
        app.Run();
    }
    public static (Startup, WebApplicationBuilder) ConfigureServices(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration.GetConfiguration();
        var startup = new Startup(configuration);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//Custom DI
        builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureDI);
        startup.ConfigureServices(builder.Services);
        return (startup, builder);
    }
    public static void ConfigureMiddleware(WebApplication app, Startup startup)
    {
        var httpContextAccessor = app.Services.GetService<IHttpContextAccessor>();
        var hostApplicationLifetime = app.Services.GetService<IHostApplicationLifetime>();
        startup.Configure(app, app.Environment, hostApplicationLifetime, httpContextAccessor);
    }
    public static IConfiguration GetConfiguration(this ConfigurationManager configurationManager)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        var builder = configurationManager
            .SetBasePath(basePath)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
        builder.AddEnvironmentVariables();
        return builder.Build();
    }
}