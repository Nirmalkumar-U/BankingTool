using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace BankingTool.Api;
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration.GetConfiguration();

        var startup = new Startup(configuration);

        // Using a custom DI container.
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        var httpContextAccessor = app.Services.GetService<IHttpContextAccessor>();
        var hostApplicationLifetime = app.Services.GetService<IHostApplicationLifetime>();

        startup.Configure(app, app.Environment, hostApplicationLifetime, httpContextAccessor);

        app.Run();
    }

    private static IConfiguration GetConfiguration(this ConfigurationManager configurationManager)
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