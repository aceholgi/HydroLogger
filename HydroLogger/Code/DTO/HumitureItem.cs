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
    }
}