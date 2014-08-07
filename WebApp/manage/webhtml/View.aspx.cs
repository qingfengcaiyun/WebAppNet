using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.webhtml
{
    public partial class View : System.Web.UI.Page
    {
        public string locationId;
        protected void Page_Load(object sender, EventArgs e)
        {
            locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
        }
    }
}