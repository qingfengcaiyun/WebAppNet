using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using System.Collections;

namespace WebApp.manage.info.category
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                Hashtable content = new Hashtable();
                content.Add("cityId", cUser["locationId"]);

                string nameSpace = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;
                className = className.Substring(className.LastIndexOf('.') + 1).ToLower();

                Response.Write(VelocityDo.BuildStringByTemplate(className + ".vm", @"~/templates/" + nameSpace, content));
            }
        }
    }
}