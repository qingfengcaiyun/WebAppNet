using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.info.category
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();

                Response.Write(VelocityDo.BuildStringByTemplate("view.vm", @"~/templates/manage/info/category", content));
            }
        }
    }
}