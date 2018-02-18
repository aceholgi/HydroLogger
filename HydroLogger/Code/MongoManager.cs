using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace HydroLogger.Code
{
    public class MongoManager
    {
        private MongoUrl _url;

        public MongoManager(MongoUrl url)
        {
            if (!string.IsNullOrEmpty(url.ToString()))
                _url = url;
        }

        public void Insert(HydroItem item)
        {
            try
            {
                MongoClient client = new MongoClient(_url);
                IMongoDatabase database = client.GetDatabase(_url.DatabaseName);

                var collection = database.GetCollection<BsonDocument>(Constants.Database.CollectionName);

                if (collection == null)
                {
                    database.CreateCollection("entries");
                    collection = database.GetCollection<BsonDocument>(Constants.Database.CollectionName);
                }

                collection.InsertOne(item.ToBsonDocument());
            }
            catch (Exception ex)
            {

            }
        }
    }
}