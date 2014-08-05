using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.manage.info.activity
{
    public partial class List : System.Web.UI.Page
    {
        public string locationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                locationId = ((Dictionary<string, object>)Session["cUser"])["locationId"].ToString();
            }
        }
    }
}