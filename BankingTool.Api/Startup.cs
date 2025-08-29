using Autofac;
using BankingTool.Api.CustomeDI;
using BankingTool.Api.Middleware;

namespace BankingTool.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BankingToolModule());
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultService(Configuration);
            services.AddConnection(Configuration);
            services.AddSingletonService();
            services.AddJWTService(Configuration);
        }
        public void Configure(WebApplication app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, IHttpContextAccessor httpContextAccessor)
        {
            app.AddMiddleware();
            app.AddDefaultMiddleware(env);
            ApplicationAppContext.Configure(httpContextAccessor, Configuration);
        }
    }
}
