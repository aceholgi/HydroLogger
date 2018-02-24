using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace HydroLogger.Code.Manager
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

        public void Insert(HumitureItem item, string position)
        {
            try
            {
                if (_database == null || !item.IsValid())
                    return;

                position = HttpUtility.HtmlEncode(position);

                var collection = _database.GetCollection<BsonDocument>(Constants.Database.CollectionNamePrefix + position);

                if (collection == null)
                {
                    _database.CreateCollection(Constants.Database.CollectionNamePrefix + Constants.Database.Fields.Position);
                    collection = _database.GetCollection<BsonDocument>(Constants.Database.CollectionNamePrefix + position);
                }

                collection.InsertOne(item.ToBson());
            }
            catch (Exception ex)
            {

            }
        }

        public List<string> GetAllCollections()
        {
            if (_database == null)
                return new List<string>();

            List<string> collectionNames = new List<string>();

            foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                if (item.ToString().Contains(Constants.Database.CollectionNamePrefix))
                {
                    BisonCollectionItem collectionData = JsonConvert.DeserializeObject<BisonCollectionItem>(item.ToString());

                    if (!string.IsNullOrEmpty(collectionData.Name))
                        collectionNames.Add(collectionData.Name);
                }
            }

            return collectionNames;
        }

        public QueryResultItem SelectFromCollection(string collectionName, FilterDefinition<HumitureItem> filter)
        {
            if (_database == null)
                return new QueryResultItem();

            var collection = _database.GetCollection<HumitureItem>(collectionName);

            if (collection == null)
                return new QueryResultItem();

            return new QueryResultItem(HttpUtility.HtmlDecode(collectionName).Substring(Constants.Database.CollectionNamePrefix.Length), collection.Find(filter).ToList());
        }

        public List<QueryResultItem> SelectFromCollections(List<string> collectionNames, FilterDefinition<HumitureItem> filter)
        {
            if (_database == null || collectionNames == null)
                return new List<QueryResultItem>();

            List<QueryResultItem> results = new List<QueryResultItem>();

            foreach (string s in collectionNames)
                results.Add(SelectFromCollection(s, filter));

            return results;
        }
    }
}