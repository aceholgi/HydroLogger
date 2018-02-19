using System.Configuration;

namespace HydroLogger.Code.Manager
{
    public static class AuthentificationManager
    {
        public static bool Authenticate(string apiSecretParam)
        {
            if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret] != null)
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret].ToString() + "" == apiSecretParam)
                    return true;
            return false;
        }
    }
}