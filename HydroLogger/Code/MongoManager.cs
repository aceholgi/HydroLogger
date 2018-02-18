using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace HydroLogger.Code
{
    public class MongoManager
    {
        private MongoUrl _url;
        private MongoClient _client;
        private IMongoDatabase _database = null;


        public MongoManager(MongoUrl url)
        {
            if (!string.IsNullOrEmpty(url.ToString()))
            {
                _url = url;
                _client = new MongoClient(_url);
                _database = _client.GetDatabase(_url.DatabaseName);
            }
        }

        public void Insert(HydroItem item)
        {
            try
            {
                if (!item.IsValid())
                    return;

                MongoClient client = new MongoClient(_url);
                IMongoDatabase database = client.GetDatabase(_url.DatabaseName);

                var collection = database.GetCollection<BsonDocument>(Constants.Database.CollectionName + item.Position);

                if (collection == null)
                {
                    database.CreateCollection(Constants.Database.CollectionName + item.Position);
                    collection = database.GetCollection<BsonDocument>(Constants.Database.CollectionName + item.Position);
                }

                collection.InsertOne(item.ToBsonDocument());
            }
            catch (Exception ex)
            {

            }
        }

        private List<string> _GetAllCollections()
        {
            List<string> res = new List<string>();

            foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();

                if (item.ToString().Contains(Constants.Database.CollectionName))
                {
                    BisonCollectionDTO collectionData = ser.Deserialize<BisonCollectionDTO>(item.ToString());
                    if (!string.IsNullOrEmpty(collectionData.Name))
                        res.Add(collectionData.Name);
                }
            }

            return res;
        }

        private List<HydroItem> _SelectAllFromCollection(string collectionName)
        {
            List<HydroItem> res = new List<HydroItem>();

            MongoClient client = new MongoClient(_url);
            IMongoDatabase database = client.GetDatabase(_url.DatabaseName);

            var collection = database.GetCollection<BsonDocument>(collectionName);

            if (collection == null)
                return res;

            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();

            foreach (BsonDocument bd in documents)
            {
                res.Add(new HydroItem(bd));
            }
            return res;
        }

        private List<List<HydroItem>> _SelectAllFromAllCollections()
        {
            List<string> allCollections = _GetAllCollections();
            List<List<HydroItem>> collectionItems = new List<List<HydroItem>>();

            foreach (string s in allCollections)
                collectionItems.Add(_SelectAllFromCollection(s));

            return collectionItems;
        }

        public List<CollectionDTO> SelectAllCollectionItemsConcattet()
        {
            List<CollectionDTO> res = new List<CollectionDTO>();

            List<List<HydroItem>> allCollectionItems = _SelectAllFromAllCollections();

            foreach (List<HydroItem> list in allCollectionItems)
            {
                CollectionDTO collection = new CollectionDTO();
                if (list.Count > 0)
                {
                    collection.Name = list[0].Position;

                    int index = 20;
                    foreach (HydroItem item in list)
                    {
                        index++;
                        collection.TemperaturesList.Add(item.Temperature);
                        collection.HumiditysList.Add(item.Humidity);
                        if (index == 30)
                        {
                            collection.DatesList.Add("'" + item.Date.ToString("HH:mm") + "'");
                            index = 0;
                        }
                        else
                            collection.DatesList.Add("''");
                    }
                    collection.Concat();

                    res.Add(collection);
                }
            }

            return res;
        }
    }
}