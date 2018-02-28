using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using static HydroLogger.Code.Constants.Database;

namespace HydroLogger.Pages
{
    public partial class Services : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetChartData(string data)
        {
            try
            {
                PositionDateRangeItem pdItem = new PositionDateRangeItem();
                pdItem = JsonConvert.DeserializeObject<PositionDateRangeItem>(data);
                List<QueryResultItem> chartData = new List<QueryResultItem>();

                MongoManager mongoManager = new MongoManager();
                List<CollectionItem> positions = new List<CollectionItem>();

                if (string.IsNullOrEmpty(pdItem.Positions))
                    positions = mongoManager.GetAllCollectionsOfType(Constants.Database.CollectionType.Humiture);
                else
                {
                    string[] pos = pdItem.Positions.Split(',');

                    foreach (string s in pos)
                        positions.Add(new CollectionItem(s, CollectionType.Humiture));
                }

                chartData = mongoManager.SelectFromCollections(positions, FilterBuilder.Humiture.BuildFilter(pdItem.FromDate, pdItem.ToDate));
                return JsonConvert.SerializeObject(chartData);
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
                itemsExisting = mongoManager.SelectFromCollection(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter());

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
                            mongoManager.Delete(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter(item.UploaderId));
                            mongoManager.Insert(document, new CollectionItem(Constants.Database.Settings));
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
                        mongoManager.Insert(document, new CollectionItem(Constants.Database.Settings));
                    }
                }

                //Deleting
                itemsExisting = mongoManager.SelectFromCollection(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter());

                if (itemsSaved.Count != itemsExisting.Count)
                {
                    foreach (UploaderConfigItem item in itemsExisting)
                    {
                        if (!itemsSaved.Any(x => x.UploaderId == item.UploaderId))
                            mongoManager.Delete(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter(item.UploaderId));
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
                items = mongoManager.SelectFromCollection(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter());

                return JsonConvert.SerializeObject(items);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return "";
        }

        [WebMethod]
        public static string GetAllPositions()
        {
            try
            {
                List<string> collectionNames = new List<string>();
                List<CollectionItem> collectionItems = new List<CollectionItem>();

                MongoManager mongoManager = new MongoManager();
                collectionItems = mongoManager.GetAllCollectionsOfType(Constants.Database.CollectionType.Humiture);

                foreach (CollectionItem item in collectionItems)
                    collectionNames.Add(item.Name);

                return JsonConvert.SerializeObject(collectionNames);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return "";
        }
    }
}