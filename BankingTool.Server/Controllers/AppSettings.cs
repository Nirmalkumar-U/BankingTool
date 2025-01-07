using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.AngularWeb.Controllers
{
    public class AppSettings
    {
        public String? BaseUrl { get; set; }
        public String? ApiKey { get; set; }
    }
}