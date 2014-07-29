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
        public string fullName;
        public string lastLogin;
        public string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (WebPageCore.GetSession("cUser") != null)
                {
                    Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                    Dictionary<string, object> admin = new AdminLogic().GetOne(Int32.Parse(cUser["userId"].ToString()));

                    this.fullName = admin["fullName"].ToString();
                    this.lastLogin = cUser["lastLogin"].ToString();
                    this.userName = cUser["userName"].ToString();
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