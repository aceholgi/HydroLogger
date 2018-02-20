using MongoDB.Bson;
using System;
using System.Web.Script.Serialization;

namespace HydroLogger.Code.DTO
{
    public class HumitureItem
    {
        [ScriptIgnore]
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
    }
}