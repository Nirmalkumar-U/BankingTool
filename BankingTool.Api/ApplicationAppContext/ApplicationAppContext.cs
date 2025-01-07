namespace BankingTool.Api
{
    public static class ApplicationAppContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static IConfiguration _configuration;
        public static void Configure(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public static HttpContext HttpContext => _httpContextAccessor.HttpContext;
        public static IConfiguration Configuration => _configuration;
        public static string Environment => System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static string GetConfigValue(string key)
        {
            return _configuration["AppSettings:" + key];
        }
    }
}
