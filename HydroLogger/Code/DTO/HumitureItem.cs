using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace HydroLogger.Code.DTO
{
    public class HumitureItem
    {
        [JsonIgnore]
        public BsonObjectId _id { get; set; }

        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public HumitureItem()
        {

        }

        public HumitureItem(DateTime date, float temperature, float humidity)
        {
            Date = date;
            Temperature = temperature;
            Humidity = humidity;
        }

        public bool IsValid()
        {
            return (Date != null && Temperature > 0 && Humidity > 0);
        }

        public BsonDocument ToBson()
        {
            return new BsonDocument
            {
                new BsonElement(Constants.Database.Fields.Date, BsonValue.Create(Date)),
                new BsonElement(Constants.Database.Fields.Temperature, BsonValue.Create(Temperature)),
                new BsonElement(Constants.Database.Fields.Humidity, BsonValue.Create(Humidity))
            };
            
        }
    }
}