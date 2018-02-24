using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace HydroLogger.Code.Manager
{
    public static class LoggingManager
    {
        public static void LogWaring(string message, string location)
        {
            _Log(message, location, new Exception(""));
        }

        public static void LogError(string location, Exception ex)
        {
            _Log("", location, ex);
        }

        private static void _Log(string message, string location, Exception ex)
        {
            try
            {
                MongoManager mongoManager = new MongoManager();

                List<BsonElement> elements = new List<BsonElement>
                {
                    new BsonElement(Constants.Database.Fields.Logging.Date, BsonValue.Create(DateTime.Now)),
                    new BsonElement(Constants.Database.Fields.Logging.Message, BsonValue.Create(message + "")),
                    new BsonElement(Constants.Database.Fields.Logging.Location, BsonValue.Create(location + "")),
                    new BsonElement(Constants.Database.Fields.Logging.Exception, BsonValue.Create(ex.ToString() + ""))
                };

                mongoManager.Insert(new BsonDocument(elements), Constants.Database.LoggingCollection);
            }
            catch (Exception wellYouReFucked)
            {
                //https://www.youtube.com/watch?v=lwx2ce_AyOE
            }
        }
    }
}