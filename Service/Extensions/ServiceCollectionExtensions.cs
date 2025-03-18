using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Repository.MangoDbRepositories.Weather;
using Service.Jobs.Weather;
using Service.Services.LoggingHandler;
using Service.Services.Weather;

namespace Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            // For use IHttpClientFactory
            services.AddTransient<HttpClientLoggingHandler>();
            services.AddHttpClient("HttpClientLogging").AddHttpMessageHandler<HttpClientLoggingHandler>();

            services.AddScoped<IWeatherService, WeatherService>();

            services.AddSingleton<IWeatherRepository, WeatherRepository>();

            services.AddTransient<IJobFactory, WeatherSnifferFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Job 
            services.AddTransient<WeatherSnifferFactory>();
            services.AddScoped<WeatherSnifferJob>();

            return services;
        }
    }
}
