using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace HydroLogger.Pages
{
    public partial class Uploader : System.Web.UI.Page
    {
        private string _action = "";
        private string _position = "";
        private float _temperature = -1;
        private float _humidity = -1;
        private string _apisecret = "";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Params.Get("action")))
                    _action = Request.Params.Get("action");

                if (!string.IsNullOrEmpty(Request.Params.Get("position")))
                    _position = Request.Params.Get("position");

                if (!string.IsNullOrEmpty(Request.Params.Get("temp")))
                    float.TryParse(Request.Params.Get("temp"), out _temperature);

                if (!string.IsNullOrEmpty(Request.Params.Get("hum")))
                    float.TryParse(Request.Params.Get("hum"), out _humidity);

                if (!string.IsNullOrEmpty(Request.Params.Get("apisecret")))
                    _apisecret = Request.Params.Get("apisecret");

                if (string.IsNullOrEmpty(_action) || string.IsNullOrEmpty(_position) || string.IsNullOrEmpty(_apisecret) || _temperature == -1 || _humidity == -1)
                    return;

                if (!AuthentificationManager.Authenticate(_apisecret))
                    return;

                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] == null)
                    return;

                mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                if (_action == "AddRecord")
                {
                    mongoManager.Insert(new HumitureItem(DateTime.UtcNow, _temperature, _humidity), _position);
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