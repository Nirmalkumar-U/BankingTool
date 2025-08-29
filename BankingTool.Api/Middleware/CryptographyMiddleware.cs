using System.Text.Json.Nodes;
using BankingTool.Service;

namespace BankingTool.Api.Middleware
{
    public class CryptographyMiddleware(RequestDelegate next, IEncriptDecriptService encryptionService)
    {
        private readonly RequestDelegate _next = next;
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
                    EncryptAccountNumbers(jsonNode);

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

        private void EncryptAccountNumbers(JsonNode? node)
        {
            if (node is JsonObject jsonObject)
            {
                foreach (var property in jsonObject.ToArray())
                {
                    if (string.Equals(property.Key, "AccountNumber", StringComparison.OrdinalIgnoreCase) &&
                        property.Value?.GetValueKind() == System.Text.Json.JsonValueKind.String)
                    {
                        var value = property.Value.GetValue<string>();
                        var encrypted = _encryptionService.EncryptData(value);
                        jsonObject[property.Key] = JsonValue.Create(encrypted);
                    }
                    else
                    {
                        EncryptAccountNumbers(property.Value);
                    }
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    EncryptAccountNumbers(item);
                }
            }
        }
    }
}
