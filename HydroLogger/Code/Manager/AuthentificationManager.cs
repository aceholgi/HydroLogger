using System;
using System.Configuration;

namespace HydroLogger.Code.Manager
{
    public static class AuthentificationManager
    {
        public static bool AuthenticateApi(string apiSecretParam)
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

        public static bool AuthenticateUser(string settingsSecret)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.SettingsSecret] != null)
                    if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.SettingsSecret].ToString() + "" == settingsSecret)
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