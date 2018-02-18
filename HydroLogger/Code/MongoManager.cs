using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

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
                if (!item.IsValid())
                    return;

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

        public List<HydroItem> SelectAll()
        {
            MongoClient client = new MongoClient(_url);
            IMongoDatabase database = client.GetDatabase(_url.DatabaseName);

            var collection = database.GetCollection<BsonDocument>(Constants.Database.CollectionName);

            if (collection == null)
                return null;

            //var filter = Builders<BsonDocument>.Filter.Where(b => b[Constants.Database.Temperature] == "20");
            //var cursor = collection.Find(filter).ToCursor();
            //List<BsonDocument> documents = cursor.ToList();

            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();            
            List<HydroItem> items = new List<HydroItem>();

            foreach (BsonDocument bd in documents)
            {
                items.Add(new HydroItem(bd));
            }
            return items;
        }
    }
}