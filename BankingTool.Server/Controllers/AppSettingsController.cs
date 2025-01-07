using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AssetManagement.AngularWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppSettingsController(IOptions<AppSettings> appSettingsOptions) : ControllerBase
    {
        private readonly AppSettings appSettings = appSettingsOptions.Value;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appSettings);
        }

    }

}