using HydroLogger.Code.DTO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HydroLogger.Pages
{
    public partial class Services : System.Web.UI.Page
    {

        [WebMethod]
        public static string LoadUploaderPosition()
        {
            return "hello World";

        }

        [WebMethod]
        public static void SaveUploaderPosition(string data)
        {
            List<IdPositionItem> items = new List<IdPositionItem>();

            items = JsonConvert.DeserializeObject<List<IdPositionItem>>(data);
        }
    }
}