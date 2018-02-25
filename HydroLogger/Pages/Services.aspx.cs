using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HydroLogger.Pages
{
    public partial class Services : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetOverviewData()
        {
            try
            {
                MongoManager mongoManager = new MongoManager();

                List<QueryResultItem> overviewResults = mongoManager.SelectFromCollections(mongoManager.GetAllCollections(Constants.Database.CollectionNamePrefix), FilterBuilder.Humiture.BuildFilter(DateTime.Now.AddDays(-1), DateTime.Now));
                return JsonConvert.SerializeObject(overviewResults);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return "";
        }

        [WebMethod]
        public static void SaveUploaderConfig(string data)
        {
            try
            {
                MongoManager mongoManager = new MongoManager();

                List<UploaderConfigItem> itemsSaved = new List<UploaderConfigItem>();
                List<UploaderConfigItem> itemsExisting = new List<UploaderConfigItem>();
                List<UploaderConfigItem> alreadyChecked = new List<UploaderConfigItem>();

                itemsSaved = JsonConvert.DeserializeObject<List<UploaderConfigItem>>(data);
                itemsExisting = mongoManager.SelectFromCollection(Constants.Database.SettingsCollection, FilterBuilder.UploaderConfig.BuildFilter());

                foreach (UploaderConfigItem item in itemsSaved)
                {
                    if (itemsExisting.Any(x => x.UploaderId == item.UploaderId))    //Item schon vorhanden
                    {
                        if (itemsExisting.Any(x => x.UploaderId == item.UploaderId && x.Position != item.Position))         //Positionseigenschaft hat sich geändert
                        {
                            List<BsonElement> elements = new List<BsonElement>
                                {
                                new BsonElement(Constants.Database.Fields.UploaderConfig.Id, BsonValue.Create(item.UploaderId)),
                                new BsonElement(Constants.Database.Fields.UploaderConfig.Position, BsonValue.Create(item.Position))
                                };

                            BsonDocument document = new BsonDocument(elements);
                            mongoManager.Delete(Constants.Database.SettingsCollection, FilterBuilder.UploaderConfig.BuildFilter(item.UploaderId));
                            mongoManager.Insert(document, Constants.Database.SettingsCollection);
                        }
                    }
                    else    //Item noch nicht vorhanden
                    {
                        List<BsonElement> elements = new List<BsonElement>
                            {
                            new BsonElement(Constants.Database.Fields.UploaderConfig.Id, BsonValue.Create(item.UploaderId)),
                            new BsonElement(Constants.Database.Fields.UploaderConfig.Position, BsonValue.Create(item.Position))
                            };

                        BsonDocument document = new BsonDocument(elements);
                        mongoManager.Insert(document, Constants.Database.SettingsCollection);
                    }
                }

                //Deleting
                itemsExisting = mongoManager.SelectFromCollection(Constants.Database.SettingsCollection, FilterBuilder.UploaderConfig.BuildFilter());

                if (itemsSaved.Count != itemsExisting.Count)
                {
                    foreach (UploaderConfigItem item in itemsExisting)
                    {
                        if (!itemsSaved.Any(x => x.UploaderId == item.UploaderId))
                            mongoManager.Delete(Constants.Database.SettingsCollection, FilterBuilder.UploaderConfig.BuildFilter(item.UploaderId));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        [WebMethod]
        public static string LoadUploaderConfig()
        {
            try
            {
                //if (!AuthentificationManager.AuthenticateUser(HttpUtility.HtmlEncode(key + "")))
                //    return "{\"error\":\"Unauthenticated\"}";

                MongoManager mongoManager;
                List<UploaderConfigItem> items = new List<UploaderConfigItem>();

                mongoManager = new MongoManager();
                items = mongoManager.SelectFromCollection(Constants.Database.SettingsCollection, FilterBuilder.UploaderConfig.BuildFilter());

                return JsonConvert.SerializeObject(items);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return "";
        }
    }
}