using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac.Core;
using BankingTool.Model;
using BankingTool.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Api.Middleware
{
    public static class CustomMiddleware
    {
        public static IApplicationBuilder AddMiddleware(this WebApplication app)
        {
            app.UseMiddleware<CryptographyMiddleware>();
            return app;
        }
        public static IApplicationBuilder AddDefaultMiddleware(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.DefaultModelsExpandDepth(-1));
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }

        public static IServiceCollection AddDefaultService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddMemoryCache();
            services.AddLazyCache();
            services.AddHttpContextAccessor();

            var appSettings = configuration.GetSection("AppSettings");

            services.Configure<AppSettingsDto>(appSettings);

            // Add services to the container.
            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            return services;
        }

        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            string branch = ThisAssembly.Git.Branch;
            string connectionString = branch.Contains("dev", StringComparison.OrdinalIgnoreCase)
                ? configuration.GetConnectionString("BankingToolConnection")
                : configuration.GetConnectionString("BankingToolConnection_Local");

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
            return services;
        }
        public static IServiceCollection AddSingletonService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            var securityKey = configuration["AppSettings:SecurityKey"];
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
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                    ValidateLifetime = true
                };
            });
            return services;
        }
    }
}
