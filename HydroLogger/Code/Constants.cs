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

            public static class Fields
            {
                public static readonly string Position = "Position";
                public static readonly string Date = "Date";
                public static readonly string Temperature = "Temperature";
                public static readonly string Humidity = "Humidity";
            }
        }

        public static class Mail
        {

        }
    }
}