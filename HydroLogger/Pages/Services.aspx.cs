using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace HydroLogger.Pages
{
    public partial class Services : System.Web.UI.Page
    {
        [WebMethod]
        public static void SaveUploaderConfig(string data)
        {
           try
           {
               MongoManager mongoManager;
               mongoManager = new MongoManager();

               List<UploaderConfigItem> itemsNew = new List<UploaderConfigItem>();
               List<UploaderConfigItem> itemsExisting = new List<UploaderConfigItem>();

               itemsNew = JsonConvert.DeserializeObject<List<UploaderConfigItem>>(data);
               itemsExisting = mongoManager.SelectFromCollection(Constants.Database.SettingsCollection, FilterBuilder.BuildUploaderConfigFilter());

               foreach (UploaderConfigItem item in itemsNew)
               {
                   if (itemsExisting.Any(x => x.UploaderId == item.UploaderId))
                   {
                   //    if (itemsExisting.Any(x => x.UploaderId == item.UploaderId && x.Position != item.Position))
                   //    {
                   //        List<BsonElement> elements = new List<BsonElement>
                   //        {
                   //        new BsonElement(Constants.Database.Fields.UploaderConfig.Id, BsonValue.Create(item.UploaderId)),
                   //        new BsonElement(Constants.Database.Fields.UploaderConfig.Position, BsonValue.Create(item.Position))
                   //        };
                   //
                   //        BsonDocument document = new BsonDocument(elements);
                   //        mongoManager.Delete(Constants.Database.SettingsCollection, FilterBuilder.BuildFilter());
                   //        mongoManager.Insert(document, Constants.Database.SettingsCollection);
                   //    }
                   }
                   else
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
                MongoManager mongoManager;
                List<UploaderConfigItem> items = new List<UploaderConfigItem>();

                mongoManager = new MongoManager();
                items = mongoManager.SelectFromCollection(Constants.Database.SettingsCollection, FilterBuilder.BuildUploaderConfigFilter());

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

//https://mongodb.github.io/mongo-csharp-driver/2.5/getting_started/quick_tour/
//var filter = Builders<BsonDocument>.Filter.Eq("i", 110);