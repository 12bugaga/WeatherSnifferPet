using Entity.MongoDb.Weather;

namespace Service.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherEntity> GetWeather(double lat, double lon);
        Task<List<WeatherEntity>> GetWeathers();
    }
}
