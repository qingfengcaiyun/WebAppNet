using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.manage.info.category
{
    public partial class View : System.Web.UI.Page
    {
        public string cityId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                this.cityId = cUser["locationId"].ToString();
            }
        }
    }
}