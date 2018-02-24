using System;
using System.Configuration;

namespace HydroLogger.Code.Manager
{
    public static class AuthentificationManager
    {
        public static bool Authenticate(string apiSecretParam)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret] != null)
                    if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.ApiSecret].ToString() + "" == apiSecretParam)
                        return true;
            }
            catch (Exception ex)
            {
                LoggingManager.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return false;
        }
    }
}