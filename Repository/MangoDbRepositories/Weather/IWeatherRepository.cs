using Entity.MongoDb.Weather;

namespace Repository.MangoDbRepositories.Weather
{
    public interface IWeatherRepository
    {
        Task<List<WeatherEntity>> GetAsync();
        Task<WeatherEntity?> GetAsync(string id);
        Task CreateAsync(WeatherEntity newWeather);
        Task UpdateAsync(string id, WeatherEntity updatedWather);
        Task RemoveAsync(string id);
    }
}
