using Microsoft.Extensions.DependencyInjection;

namespace Service.Services.LoggingHandler
{
    public class HttpClientLoggingHandler : DelegatingHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        //private readonly ILoggerRepository _loggerRepository;

        public HttpClientLoggingHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new(System.Net.HttpStatusCode.InternalServerError);

            response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
