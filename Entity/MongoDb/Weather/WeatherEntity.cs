using DTOs.Web.Response;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity.MongoDb.Weather
{
    public class WeatherEntity : WeatherResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Date { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
