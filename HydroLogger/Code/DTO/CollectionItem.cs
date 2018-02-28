using HydroLogger.Code.Manager;
using System.Web;
using static HydroLogger.Code.Constants.Database;

namespace HydroLogger.Code.DTO
{
    public class CollectionItem
    {
        public string Name { get; set; }
        public string FullEncodedName { get; set; }
        public CollectionType Type { get; set; }

        public CollectionItem(string name)
        {
            Name = name;

            if (Name == Constants.Database.Logging)
            {
                Type = CollectionType.Logging;
                FullEncodedName = HttpUtility.HtmlEncode(Name);
            }
            else if (Name == Constants.Database.Settings)
            {
                Type = CollectionType.Settings;
                FullEncodedName = HttpUtility.HtmlEncode(Name);
            }
            else if (Name.Contains(Constants.Database.HumiturePrefix))
            {
                Type = CollectionType.Humiture;
                Name = Name.Replace(Constants.Database.HumiturePrefix, "");
                FullEncodedName = HttpUtility.HtmlEncode(Constants.Database.HumiturePrefix + Name);
            }
            else
            {
                Type = CollectionType.System;
                FullEncodedName = HttpUtility.HtmlEncode(Name);
            }
        }

        public CollectionItem(string fullEncodedName, CollectionType type)
        {
            switch (type)
            {
                case CollectionType.Humiture:
                    Type = type;
                     
                    FullEncodedName = HttpUtility.HtmlEncode(Constants.Database.HumiturePrefix + fullEncodedName);  //#DoppeltHaletBesser                  
                    break;
                default:
                    LoggingManager.LogWaring("Collection Item Type not Implemented!", System.Reflection.MethodBase.GetCurrentMethod().Name);
                    break;
            }
        }
    }
}