using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.info.article
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("cityId", cUser["locationId"]);

                Response.Write(VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/manage/info/article", content));
            }
        }
    }
}