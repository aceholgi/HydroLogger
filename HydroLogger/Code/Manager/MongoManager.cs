using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using static HydroLogger.Code.Constants.Database;

namespace HydroLogger.Code.Manager
{
    public class MongoManager
    {
        private MongoClient _client;
        private IMongoDatabase _database = null;

        public MongoManager()
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] == null)
                    return;

                MongoUrl url = new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ConnectionString + "");

                if (!string.IsNullOrEmpty(url.ToString()))
                {
                    _client = new MongoClient(url);
                    _database = _client.GetDatabase(url.DatabaseName);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void Insert(BsonDocument document, CollectionItem collectionItem)
        {
            try
            {
                if (_database == null)
                    return;

                var collection = _database.GetCollection<BsonDocument>(collectionItem.FullEncodedName);

                if (collection == null)
                    return;

                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public List<CollectionItem> GetAllCollectionsOfType(CollectionType type)
        {
            try
            {
                if (_database == null)
                    return new List<CollectionItem>();

                List<CollectionItem> collections = new List<CollectionItem>();

                foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync().Result)
                {

                    CollectionItem collItem = new CollectionItem(JsonConvert.DeserializeObject<BisonCollectionItem>(item.ToString()).Name);

                    if (type == CollectionType.All || collItem.Type == type)
                        collections.Add(collItem);
                }

                return collections;
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new List<CollectionItem>();
        }

        #region Humiture Items
        public QueryResultItem SelectFromCollection(CollectionItem collectionItem, FilterDefinition<HumitureItem> filter)
        {
            try
            {
                QueryResultItem item = new QueryResultItem();

                if (_database == null)
                    return item;

                var collection = _database.GetCollection<HumitureItem>(collectionItem.FullEncodedName);

                if (collection == null)
                    return item;

                item.HumitureItems = collection.Find(filter).ToList();
                item.Name = collectionItem.Name;

                return item;
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new QueryResultItem();
        }

        public List<QueryResultItem> SelectFromCollections(List<CollectionItem> collectionItems, FilterDefinition<HumitureItem> filter)
        {
            try
            {
                if (_database == null)
                    return new List<QueryResultItem>();

                List<QueryResultItem> results = new List<QueryResultItem>();

                foreach (CollectionItem collectionItem in collectionItems)
                    results.Add(SelectFromCollection(collectionItem, filter));

                return results;
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new List<QueryResultItem>();
        }
        #endregion

        #region Uploader Config Items
        public List<UploaderConfigItem> SelectFromCollection(CollectionItem collectionItem, FilterDefinition<UploaderConfigItem> filter)
        {
            try
            {
                List<UploaderConfigItem> items = new List<UploaderConfigItem>();

                if (_database == null)
                    return items;

                var collection = _database.GetCollection<UploaderConfigItem>(collectionItem.FullEncodedName);

                if (collection == null)
                    return items;

                return collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new List<UploaderConfigItem>();
        }

        public void Delete(CollectionItem collectionItem, FilterDefinition<UploaderConfigItem> filter)
        {
            try
            {
                if (_database == null)
                    return;

                var collection = _database.GetCollection<UploaderConfigItem>(collectionItem.FullEncodedName);

                if (collection == null)
                    return;

                collection.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion
    }
}