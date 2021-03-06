﻿using HydroLogger.Code;
using HydroLogger.Code.DTO;
using HydroLogger.Code.Manager;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HydroLogger.Pages
{
    public partial class Uploader : System.Web.UI.Page
    {
        private string _action = "";
        private string _uploaderId = "";
        private float _temperature = -100;
        private float _humidity = -100;
        private string _apisecret = "";

        private MongoManager mongoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Params.Get("action")))
                    _action = Request.Params.Get("action");

                if (!string.IsNullOrEmpty(Request.Params.Get("uploaderId")))
                    _uploaderId = Request.Params.Get("uploaderId");

                if (!string.IsNullOrEmpty(Request.Params.Get("temp")))
                    float.TryParse(Request.Params.Get("temp"), out _temperature);

                if (!string.IsNullOrEmpty(Request.Params.Get("hum")))
                    float.TryParse(Request.Params.Get("hum"), out _humidity);

                if (!string.IsNullOrEmpty(Request.Params.Get("apisecret")))
                    _apisecret = Request.Params.Get("apisecret");

                if (string.IsNullOrEmpty(_action) || string.IsNullOrEmpty(_uploaderId) || string.IsNullOrEmpty(_apisecret) || _temperature == -100 || _humidity == -100)
                    return;

                if (!AuthentificationManager.AuthenticateApi(_apisecret))
                    return;

                mongoManager = new MongoManager();

                if (_action == "AddRecord")
                {
                    List<BsonElement> elements = new List<BsonElement>
                    {
                        new BsonElement(Constants.Database.Fields.Humiture.Date, BsonValue.Create(DateTime.Now)),
                        new BsonElement(Constants.Database.Fields.Humiture.Temperature, BsonValue.Create(_temperature)),
                        new BsonElement(Constants.Database.Fields.Humiture.Humidity, BsonValue.Create(_humidity))
                    };

                    List<UploaderConfigItem> configItems =  mongoManager.SelectFromCollection(new CollectionItem(Constants.Database.Settings), FilterBuilder.UploaderConfig.BuildFilter(_uploaderId));
                    if (configItems.Any())
                        mongoManager.Insert(new BsonDocument(elements), new CollectionItem(configItems.FirstOrDefault().Position, Constants.Database.CollectionType.Humiture));
                    else
                        LoggingManager.LogWaring("Cant find a Position for uploader with ID " + _uploaderId + ". Pleas add this Uploader to the Config. Value recieved is lost.", System.Reflection.MethodBase.GetCurrentMethod().Name);

                    return;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}