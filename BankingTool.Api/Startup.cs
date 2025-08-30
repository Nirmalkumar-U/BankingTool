using Autofac;
using BankingTool.Api.ApplicationSetUp;
using BankingTool.Api.CustomeDI;

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
            app.AddMiddleware(Configuration);
            app.AddDefaultMiddleware(env);
            ApplicationAppContext.Configure(httpContextAccessor, Configuration);
        }
    }
}
