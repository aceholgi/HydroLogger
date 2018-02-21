using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

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
            JavaScriptSerializer ser = new JavaScriptSerializer();

            foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                if (item.ToString().Contains(Constants.Database.CollectionNamePrefix))
                {
                    BisonCollectionDTO collectionData = ser.Deserialize<BisonCollectionDTO>(item.ToString());
                    if (!string.IsNullOrEmpty(collectionData.Name))
                        collectionNames.Add(collectionData.Name);
                }
            }

            return collectionNames;
        }

        public ResultDTO SelectFromCollection(string collectionName, FilterDefinition<HumitureItem> filter)
        {
            if (_database == null)
                return new ResultDTO();

            var collection = _database.GetCollection<HumitureItem>(collectionName);

            if (collection == null)
                return new ResultDTO();

            return new ResultDTO(HttpUtility.HtmlDecode(collectionName).Substring(Constants.Database.CollectionNamePrefix.Length), collection.Find(filter).ToList());
        }

        public List<ResultDTO> SelectFromCollections(List<string> collectionNames, FilterDefinition<HumitureItem> filter)
        {
            if (_database == null || collectionNames == null)
                return new List<ResultDTO>();

            List<ResultDTO> results = new List<ResultDTO>();

            foreach (string s in collectionNames)
                results.Add(SelectFromCollection(s, filter));

            return results;
        }

        //public List<CollectionDTO> SelectFromAllCollectionWithDateOffset(DateTime dateOffset)
        //{
        //    List<CollectionDTO> res = new List<CollectionDTO>();
        //
        //    List<List<HumitureItem>> allCollectionItems = _SelectFromAllCollectionsWithDateOffset(dateOffset);
        //
        //    foreach (List<HumitureItem> list in allCollectionItems)
        //    {
        //        CollectionDTO collection = new CollectionDTO();
        //        if (list.Count > 0)
        //        {
        //            collection.Name = HttpUtility.HtmlDecode(list[0].Position);
        //
        //            int index = 20;
        //            foreach (HumitureItem item in list)
        //            {
        //                index++;
        //                collection.Temperatures.Add(item.Temperature);
        //                collection.Humiditys.Add(item.Humidity);
        //                collection.Dates.Add(item.Date.ToString("o"));
        //            }
        //            res.Add(collection);
        //        }
        //    }
        //    return res;
        //}
    }
}