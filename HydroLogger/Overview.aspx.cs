using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace HydroLogger
{
    public partial class Overview : Page
    {
        public string JsonData = "{}";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                    mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));
                
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<CollectionDTO> collections = mongoManager.SelectAllCollectionItems();

                JsonData = ser.Serialize(collections);
            }
            catch (Exception ex)
            {
            }
        }
    }
}