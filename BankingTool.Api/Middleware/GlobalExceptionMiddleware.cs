using System.Net;

namespace BankingTool.Api.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate request)
    {
        private readonly RequestDelegate _request = request;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message;
            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "A required argument was null.";
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "An invalid argument was provided.";
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "You are not authorized to perform this action.";
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;
                default:
                    Console.WriteLine($"Unhandled Exception: {exception}");
                    message = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Details = exception.StackTrace
            };

            string jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
