using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using System.Collections;

namespace WebApp.manage.renovation.project
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string cateId = WebPageCore.GetRequest("cateId");

                if (!RegexDo.IsInt32(cateId))
                {
                    cateId = "1";
                }

                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                Hashtable content = new Hashtable();
                content.Add("cityId", cUser["locationId"]);
                content.Add("cateId", cateId);

                string nameSpace = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;
                className = className.Substring(className.LastIndexOf('.') + 1).ToLower();

                Response.Write(VelocityDo.BuildStringByTemplate(className + ".vm", @"~/templates/" + nameSpace, content));
            }
        }
    }
}