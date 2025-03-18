using Entity.MongoDb.Weather;
using Newtonsoft.Json;
using Repository.MangoDbRepositories.Weather;
using System.Web;

namespace Service.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IHttpClientFactory httpClientFactory, IWeatherRepository weatherRepository)
        {
            _httpClientFactory = httpClientFactory;
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherEntity> GetWeather(double lat, double lon)
        {
            var builder = new UriBuilder("https://api.api-ninjas.com/v1/weather");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["lat"] = lat.ToString();
            query["lon"] = lon.ToString();
            builder.Query = query.ToString();
            string path = builder.ToString();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            requestMessage.Headers.Add("X-Api-Key", Environment.GetEnvironmentVariable("API_KEY"));

            using var httpClient = _httpClientFactory.CreateClient("HttpClientLogging");
            var httpResponse = await httpClient.SendAsync(requestMessage);

            if (httpResponse == null)
                throw new Exception("External connections service response is null");

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var response = new WeatherEntity();

            if (!httpResponse.IsSuccessStatusCode)
                return response;

            response = JsonConvert.DeserializeObject<WeatherEntity>(jsonResponse);

            response.lat = lat;
            response.lon = lon;
            response.Date = DateTime.UtcNow;

            // Log to MongoDb
            await _weatherRepository.CreateAsync(response);
            
            return response;
        }

        public async Task<List<WeatherEntity>> GetWeathers()
        {
            return await _weatherRepository.GetAsync();
        }

    }
}
