using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;

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

                List<ResultDTO> overviewResults = mongoManager.SelectFromCollections(mongoManager.GetAllCollections(), FilterBuilder.BuildFilter(DateTime.Now.AddDays(-1), DateTime.Now));

                JavaScriptSerializer ser = new JavaScriptSerializer();
                JsonData = ser.Serialize(overviewResults);
            }
            catch (Exception ex)
            {
            }
        }
    }
}