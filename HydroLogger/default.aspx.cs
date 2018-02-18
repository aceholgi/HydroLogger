using HydroLogger.Code;
using HydroLogger.Code.DTO;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace HydroLogger
{
    public partial class _default : System.Web.UI.Page
    {
        private bool _isAuthenticated = false;
        private string _action = "";
        private string _position = "";
        private string _temp = "";
        private string _hum = "";
        private string _apisecret = "";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //action=AddRecord&position=Wohnzimmer&temp=22.00&hum=55.00&apisecret=007
                if (!string.IsNullOrEmpty(Request.Params.Get("action")))
                    _action = Request.Params.Get("action");
                if (!string.IsNullOrEmpty(Request.Params.Get("position")))
                    _position = Request.Params.Get("position");
                if (!string.IsNullOrEmpty(Request.Params.Get("temp")))
                    _temp = Request.Params.Get("temp");
                if (!string.IsNullOrEmpty(Request.Params.Get("hum")))
                    _hum = Request.Params.Get("hum");
                if (!string.IsNullOrEmpty(Request.Params.Get("apisecret")))
                    _apisecret = Request.Params.Get("apisecret");

                _isAuthenticated = _authenticate(_apisecret);

                if (_isAuthenticated)   //AUTHENTICATED
                {
                    if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                        mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                    switch (_action)
                    {
                        case "AddRecord":
                            mongoManager.Insert(new HydroItem { Date = DateTime.Now, Humidity = _hum, Position = _position, Temperature = _temp });
                            break;
                        default:
                            break;
                    }
                }
                else     //UNAUTHENTICATED
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool _authenticate(string apiSecretParam)
        {
            if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret] != null)
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret].ToString() + "" == apiSecretParam)
                    return true;
            return false;
        }
    }
}