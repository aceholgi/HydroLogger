namespace HydroLogger.Code
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
            public static readonly string CollectionName = "entries";

            public static readonly string Date = "Date";
            public static readonly string Humidity = "Humidity";
            public static readonly string Temperature = "Temperature";
            public static readonly string Position = "Position";
        }

        public static class Mail
        {

        }
    }
}