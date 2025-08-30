using System.Text.Json.Nodes;
using BankingTool.Model;
using BankingTool.Service;
using Microsoft.Extensions.Options;

namespace BankingTool.Api.Middleware
{
    public class CryptographyMiddleware(RequestDelegate next, IEncriptDecriptService encryptionService, IOptions<AppSettingsDto> appSetting)
    {
        private readonly RequestDelegate _next = next;
        private readonly AppSettingsDto _appSetting = appSetting.Value;
        private readonly IEncriptDecriptService _encryptionService = encryptionService;

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);

            if (context.Response.StatusCode >= 200 && context.Response.StatusCode <= 210 &&
            context.Response.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true)
            {
                var json = await new StreamReader(memoryStream).ReadToEndAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    var jsonNode = JsonNode.Parse(json);
                    EncryptField(jsonNode);

                    memoryStream.SetLength(0);
                    await using var writer = new StreamWriter(memoryStream, leaveOpen: true);
                    await writer.WriteAsync(jsonNode.ToJsonString());
                    await writer.FlushAsync();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
            }

            context.Response.ContentLength = memoryStream.Length;
            await memoryStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }

        private void EncryptField(JsonNode? node)
        {
            var fieldsToEncrypt = _appSetting.FieldsToEncrypt.Split(",") ?? ["AccountNumber"];
            if (node is JsonObject jsonObject)
            {
                foreach (var property in jsonObject.ToArray())
                {
                    if (fieldsToEncrypt.Any(key => string.Equals(property.Key, key, StringComparison.OrdinalIgnoreCase)) &&
                        property.Value?.GetValueKind() == System.Text.Json.JsonValueKind.String)
                    {
                        var value = property.Value.GetValue<string>();
                        var encrypted = _encryptionService.EncryptData(value);
                        jsonObject[property.Key] = JsonValue.Create(encrypted);
                    }
                    else
                    {
                        EncryptField(property.Value);
                    }
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    EncryptField(item);
                }
            }
        }
    }
}
