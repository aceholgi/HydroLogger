using HydroLogger.Code;
using HydroLogger.Code.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public string SeriesTemp = "[]";
        public string SeriesHumid = "[]";
        public string Labels = "[]";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _setParamVars();

                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo] != null)
                    mongoManager = new MongoManager(new MongoUrl(ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Mongo].ToString()));

                if (_isAuthenticated && _action == "AddRecord")
                {
                    mongoManager.Insert(new HydroItem { Date = DateTime.Now, Humidity = _hum, Position = _position, Temperature = _temp });
                    Server.Transfer(null);
                    return;
                }

                List<HydroItem> allItems = mongoManager.SelectAll();

                List<string> temps = new List<string>();
                List<string> humids = new List<string>();
                List<string> dates = new List<string>();

                int index = 0;
                foreach (HydroItem item in allItems)
                {
                    temps.Add(item.Temperature);
                    humids.Add(item.Humidity);
                    if (index == 20)
                    {
                        dates.Add(item.Date + "");
                        index = 0;
                    }
                }

                SeriesTemp = _ConcatList(temps);
                SeriesHumid = _ConcatList(humids);
                Labels = _ConcatList(dates);
            }
            catch (Exception ex)
            {
            }
        }

        private void _setParamVars()
        {
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
        }

        private bool _authenticate(string apiSecretParam)
        {
            if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret] != null)
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret].ToString() + "" == apiSecretParam)
                    return true;
            return false;
        }

        private string _ConcatList(List<string> list)
        {
            string ret = "";
            foreach (string s in list)
                ret += s + ',';

            return ret.Substring(0, ret.Length - 1);
        }
    }
}