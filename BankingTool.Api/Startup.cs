using System.Text;
using Autofac;
using BankingTool.Api.CustomeDI;
using BankingTool.Api.Middleware;
using BankingTool.Model;
using BankingTool.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Api
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Configure custom container.
            builder.RegisterModule(new BankingToolModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSignalR(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("BankingToolConnection"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            var appSettings = Configuration.GetSection("AppSettings");

            services.Configure<AppSettingsDto>(appSettings);

            // Add services to the container.
            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });

            //services.Configure<GzipCompressionProviderOptions>(options =>
            //{
            //    options.Level = CompressionLevel.Optimal;
            //});

            //services.AddResponseCompression(options =>
            //{
            //    options.EnableForHttps = true;
            //    options.Providers.Add<GzipCompressionProvider>();
            //});

            // Configure JWT authentication
            var securityKey = Configuration["AppSettings:SecurityKey"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            services.AddAuthentication(op =>
            {
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["AppSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                    ValidateLifetime = true
                };
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, IHttpContextAccessor httpContextAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            ApplicationAppContext.Configure(httpContextAccessor, Configuration);
        }
    }
}
