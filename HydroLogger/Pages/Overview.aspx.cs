using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace HydroLogger.Pages
{
    public partial class Overview : System.Web.UI.Page
    {
        public string JsonData = "{}";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                    mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                List<QueryResultItem> overviewResults = mongoManager.SelectFromCollections(mongoManager.GetAllCollections(), FilterBuilder.BuildFilter(DateTime.Now.AddDays(-1), DateTime.Now));

                JsonData = JsonConvert.SerializeObject(overviewResults);
            }
            catch (Exception ex)
            {
            }
        }
    }
}