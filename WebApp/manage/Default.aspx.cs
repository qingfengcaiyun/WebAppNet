using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                Response.Write(VelocityDo.BuildStringByTemplate("login.vm", @"~/templates/" + nameSpace, null));
            }
        }
    }
}