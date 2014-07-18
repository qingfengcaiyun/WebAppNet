using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.sys.webmsg
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Write(VelocityDo.BuildStringByTemplate("view.vm", @"~/templates/manage/sys/webmsg", null));
            }
        }
    }
}