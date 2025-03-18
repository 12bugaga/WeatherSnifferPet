using System.Net;
using System.Text.Json;
using System.Text;
using DTOs.Web.WebResponse;

namespace WebWeather.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var exceptionMessages = new StringBuilder(e.Message);

                var exception = e;
                exceptionMessages.Append(exception.Message);
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    exceptionMessages.Append("|");
                    exceptionMessages.Append(exception.Message);
                }

                var exceptionMessagesString = exceptionMessages.ToString();
                _logger.LogCritical(e, exceptionMessagesString);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var internalError = new ResponseShell($"{HttpStatusCode.InternalServerError}, {exceptionMessagesString} + {exception.StackTrace}");

                var json = JsonSerializer.Serialize(internalError, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
