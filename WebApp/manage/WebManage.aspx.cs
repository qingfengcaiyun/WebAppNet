using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage
{
    public partial class WebManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (WebPageCore.GetSession("cUser") != null)
                {
                    Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                    Dictionary<string, object> admin = new AdminLogic().GetOne(Int32.Parse(cUser["userId"].ToString()));
                    Dictionary<string, object> content = new Dictionary<string, object>();
                    content.Add("fullName", admin["fullName"]);
                    content.Add("lastLogin", cUser["lastLogin"]);
                    content.Add("userName", cUser["userName"]);

                    string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                    nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                    Response.Write(VelocityDo.BuildStringByTemplate("manage.vm", @"~/templates/" + nameSpace, content));
                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}