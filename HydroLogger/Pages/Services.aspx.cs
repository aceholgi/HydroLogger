using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HydroLogger.Pages
{
    public partial class Services : System.Web.UI.Page
    {

        [WebMethod]
        public static string LoadIdPositionData()
        {
            return "hello World";

        }

        [WebMethod]
        public static void SaveIdPositionData(string data)
        {
            var i = data;

        }
    }
}