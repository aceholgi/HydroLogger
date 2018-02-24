﻿namespace HydroLogger.Code
{
    public static class Constants
    {
        public static class ConnectionStrings
        {
            public static readonly string ApiSecret = "apiSecret";
            public static readonly string Mongo = "mongoDB";
            public static readonly string Emails = "emails";
        }

        public static class Database
        {
            public static readonly string CollectionNamePrefix = "entries_";
            public static readonly string SettingsCollection = "Settings";
            public static readonly string LoggingCollection = "Logging";

            public static class Fields
            {
                public static readonly string Version = "Version";

                public static class Humiture
                {
                    public static readonly string Date = "Date";
                    public static readonly string Temperature = "Temperature";
                    public static readonly string Humidity = "Humidity";
                }

                public static class UploaderConfig
                {
                    public static readonly string Id = "UploaderId";
                    public static readonly string Position = "Position";
                }

                public static class Logging
                {
                    public static readonly string Date = "Date";
                    public static readonly string Message = "Message";
                    public static readonly string Location = "Location";
                    public static readonly string Exception = "Exception";
                }
            }
        }

        public static class Mail
        {

        }
    }
}