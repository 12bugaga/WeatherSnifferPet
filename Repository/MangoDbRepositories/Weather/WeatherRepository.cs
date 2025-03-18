using Entity.MongoDb.Weather;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repository.DbContext;

namespace Repository.MangoDbRepositories.Weather
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IMongoCollection<WeatherEntity> _weathersCollection;

        public WeatherRepository(IOptions<DatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _weathersCollection = mongoDatabase.GetCollection<WeatherEntity>(
                bookStoreDatabaseSettings.Value.WeatherCollectionName);
        }

        public async Task<List<WeatherEntity>> GetAsync()
            => await _weathersCollection.Find(_ => true).ToListAsync();

        public async Task<WeatherEntity?> GetAsync(string id)
            => await _weathersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(WeatherEntity newWeather)
            => await _weathersCollection.InsertOneAsync(newWeather);

        public async Task UpdateAsync(string id, WeatherEntity updatedWather)
            => await _weathersCollection.ReplaceOneAsync(x => x.Id == id, updatedWather);

        public async Task RemoveAsync(string id)
            => await _weathersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
