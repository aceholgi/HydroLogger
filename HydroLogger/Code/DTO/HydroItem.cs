using MongoDB.Bson;
using System;

namespace HydroLogger.Code.DTO
{
    public class HydroItem
    {
        public DateTime Date { get; set; }
        public string Position { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }

        public HydroItem()
        {

        }

        public HydroItem(DateTime date, string position, string temperature, string humidity)
        {
            Date = date;
            Position = position;
            Temperature = temperature;
            Humidity = humidity;
        }

        public BsonDocument ItemToBson()
        {
            BsonDocument doc = new BsonDocument
                {
                    {Constants.Database.Date, Date },
                    {Constants.Database.Humidity, Humidity },
                    {Constants.Database.Temperature, Temperature },
                    {Constants.Database.Position, Position },
                };
            return doc;
        }

        public HydroItem(BsonDocument document)
        {
            DateTime d = new DateTime(0);
            DateTime.TryParse(document.GetElement(Constants.Database.Date) + "", out d);
            Date = d;

            Position = document.GetElement(Constants.Database.Humidity) + "";
            Temperature = document.GetElement(Constants.Database.Temperature) + "";
            Humidity = document.GetElement(Constants.Database.Position) + "";
        }
    }
}