using DTOs.Web.WebLogs;
using Microsoft.Extensions.Primitives;

namespace WebWeather.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var log = GetRequest(context);
            await _next(context);
            log.Response = GetResponse(context);

            // Write logs to ELK


            return;
        }

        private RequestLog GetRequest(HttpContext context)
        {
            var request = new RequestLog
            {
                Date = DateTime.UtcNow,
                RequestStr = GetDescription(context.Request),
            };

            return request;
        }

        private ResponseLog GetResponse(HttpContext context)
        {
            var request = new ResponseLog
            {
                Date = DateTime.UtcNow,
                ResponseStr = GetDescription(context.Response),
            };

            return request;
        }

        private string GetDescription(HttpRequest request)
        {
            List<string> list = new List<string>
            {
                "METHOD " + request.Method,
                $"Path {request.Path}",
                "Query " + string.Join(" ", request.Query),
                "ContentType " + request.ContentType
            };
            List<string> list2 = new List<string> { "Headers" };
            foreach (KeyValuePair<string, StringValues> header in request.Headers)
            {
                list2.Add($"{header.Key}: {header.Value}");
            }

            list.Add(string.Join("\r", list2));
            return string.Join("\r", list);
        }

        private string GetDescription(HttpResponse response)
        {
            if (response == null)
            {
                return "";
            }

            List<string> list = new List<string>
        {
            $"Status code {response.StatusCode}",
            "ContentType " + response.ContentType
        };
            List<string> list2 = new List<string> { "Headers" };
            foreach (KeyValuePair<string, StringValues> header in response.Headers)
            {
                list2.Add($"{header.Key}: {header.Value}");
            }

            list.Add(string.Join("\r", list2));
            return string.Join("\r", list);
        }
    }
}
