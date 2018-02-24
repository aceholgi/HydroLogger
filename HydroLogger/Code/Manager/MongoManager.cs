using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Web;

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

        public void Insert(BsonDocument document, string collectionName)
        {
            try
            {
                if (_database == null || string.IsNullOrEmpty(collectionName))
                    return;

                collectionName = HttpUtility.HtmlEncode(collectionName);

                var collection = _database.GetCollection<BsonDocument>(collectionName);

                if (collection == null)
                    return;

                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public List<string> GetAllCollections()
        {
            try
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
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new List<string>();
        }

        #region Humiture Items
        public QueryResultItem SelectFromCollection(string collectionName, FilterDefinition<HumitureItem> filter)
        {
            try
            {
                QueryResultItem item = new QueryResultItem();

                if (_database == null)
                    return item;

                var collection = _database.GetCollection<HumitureItem>(collectionName);

                if (collection == null)
                    return item;

                item.HumitureItems = collection.Find(filter).ToList();
                item.Name = HttpUtility.HtmlDecode(collectionName).Substring(Constants.Database.CollectionNamePrefix.Length);

                return item;
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return new QueryResultItem();
        }

        public List<QueryResultItem> SelectFromCollections(List<string> collectionNames, FilterDefinition<HumitureItem> filter)
        {
            try
            {
                if (_database == null || collectionNames == null)
                    return new List<QueryResultItem>();

                List<QueryResultItem> results = new List<QueryResultItem>();

                foreach (string name in collectionNames)
                    results.Add(SelectFromCollection(name, filter));

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
        public List<UploaderConfigItem> SelectFromCollection(string collectionName, FilterDefinition<UploaderConfigItem> filter)
        {
            try
            {
                List<UploaderConfigItem> items = new List<UploaderConfigItem>();

                if (_database == null)
                    return items;

                var collection = _database.GetCollection<UploaderConfigItem>(collectionName);

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

        public void Delete(string collectionName, FilterDefinition<UploaderConfigItem> filter)
        {
            try
            {
                if (_database == null || string.IsNullOrEmpty(collectionName))
                    return;

                collectionName = HttpUtility.HtmlEncode(collectionName);

                var collection = _database.GetCollection<BsonDocument>(collectionName);

                if (collection == null)
                    return;


                var filterBuilder = Builders<BsonDocument>.Filter;

                var f = filterBuilder.Eq(Constants.Database.Fields.UploaderConfig.Position, 1);


                collection.DeleteOne(f);

                //                collection.DeleteOne(filter, new System.Threading.CancellationToken(false), null);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion
    }
}