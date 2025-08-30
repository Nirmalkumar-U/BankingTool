using System.Text.Json.Nodes;
using AssetManagement.AngularWeb.Controllers;
using BankingTool.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BankingTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppSettingsController(IOptions<AppSettings> appSettingsOptions, IEncriptDecriptService encriptDecriptService) : ControllerBase
    {
        private readonly AppSettings appSettings = appSettingsOptions.Value;
        private readonly IEncriptDecriptService _encryptionService = encriptDecriptService;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appSettings);
        }
        [HttpPost]
        public IActionResult Post([FromBody] JsonNode body)
        {
            if (body == null) return BadRequest("Invalid JSON body.");

            DecryptAccountNumbers(body);

            return Ok(body);
        }

        private void DecryptAccountNumbers(JsonNode? node)
        {
            if (node is JsonObject jsonObject)
            {
                foreach (var property in jsonObject.ToArray())
                {
                    if (string.Equals(property.Key, "AccountNumber", StringComparison.OrdinalIgnoreCase) &&
                        property.Value?.GetValueKind() == System.Text.Json.JsonValueKind.String)
                    {
                        var value = property.Value.GetValue<string>();
                        var decrypted = _encryptionService.DecryptData(value);
                        jsonObject[property.Key] = JsonValue.Create(decrypted);
                    }
                    else
                    {
                        DecryptAccountNumbers(property.Value);
                    }
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    DecryptAccountNumbers(item);
                }
            }
        }
    }
}