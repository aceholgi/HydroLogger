using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace HydroLogger
{
    public partial class Uploader : System.Web.UI.Page
    {
        private bool _isAuthenticated = false;
        private string _action = "";
        private string _position = "";
        private string _temperature = "";
        private string _humidity = "";
        private string _apisecret = "";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                    mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                if (!string.IsNullOrEmpty(Request.Params.Get("action")))
                    _action = Request.Params.Get("action");
                if (!string.IsNullOrEmpty(Request.Params.Get("position")))
                    _position = Request.Params.Get("position");
                if (!string.IsNullOrEmpty(Request.Params.Get("temp")))
                    _temperature = Request.Params.Get("temp");
                if (!string.IsNullOrEmpty(Request.Params.Get("hum")))
                    _humidity = Request.Params.Get("hum");
                if (!string.IsNullOrEmpty(Request.Params.Get("apisecret")))
                    _apisecret = Request.Params.Get("apisecret");

                _isAuthenticated = AuthentificationManager.Authenticate(_apisecret);

                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                    mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                if (_isAuthenticated && _action == "AddRecord")
                {
                    mongoManager.Insert(new HydroItem(DateTime.Now, _position, _humidity, _temperature));
                    Server.Transfer(null);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}