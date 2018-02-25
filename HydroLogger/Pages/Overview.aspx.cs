using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
                mongoManager = new MongoManager();

                List<QueryResultItem> overviewResults = mongoManager.SelectFromCollections(mongoManager.GetAllCollections(Constants.Database.CollectionNamePrefix), FilterBuilder.Humiture.BuildFilter(DateTime.Now.AddDays(-1), DateTime.Now));

                JsonData = JsonConvert.SerializeObject(overviewResults);
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}