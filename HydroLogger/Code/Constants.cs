namespace HydroLogger.Code
{
    public static class Constants
    {
        public static class ConnectionStrings
        {
            public static readonly string ApiSecret = "apiSecret";
            public static readonly string Mongo = "mongoDB";
        }

        public static class Database
        {
            public static readonly string CollectionName = "entries";

            public static readonly string Date = "date";
            public static readonly string Humidity = "humidity";
            public static readonly string Temperature = "temperature";
            public static readonly string Position = "position";
        }
    }
}