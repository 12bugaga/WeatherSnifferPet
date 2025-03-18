using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Service.Services.Weather;
using System.Text.Json;

namespace Service.Jobs.Weather
{
    public class WeatherSnifferJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WeatherSnifferJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var weatherService = scope.ServiceProvider.GetService<IWeatherService>();
                var weather = await weatherService.GetWeather(55, 35);

                Console.WriteLine(@$"Hello, WeatherSnifferJob executed -- {DateTime.Now} -- {JsonSerializer.Serialize(weather)}");
            }
        }
    }
}
