using BankingTool.Api.Middleware;

namespace BankingTool.Api.ApplicationSetUp
{
    public static class MiddlewareSetup
    {
        public static IApplicationBuilder AddMiddleware(this WebApplication app,IConfiguration configuration)
        {
            var isEncryptionEnabled = configuration.GetValue<bool>("AppSettings:IsEncryptionEnabled");
            if (isEncryptionEnabled)
            {
                app.UseMiddleware<CryptographyMiddleware>();
            }
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
                _ = endpoints.MapControllers();
            });
            return app;
        }
    }
}
