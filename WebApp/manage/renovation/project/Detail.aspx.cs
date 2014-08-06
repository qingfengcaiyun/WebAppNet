using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.project
{
    public partial class Detail : System.Web.UI.Page
    {
        public string projectId;
        public string locationId;
        public string blocationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                projectId = WebPageCore.GetRequest("projectId");

                if (RegexDo.IsInt64(projectId) && Int64.Parse(projectId) > 0)
                {
                    Dictionary<string, object> item = new ProjectLogic().GetOne(Int64.Parse(projectId));
                    blocationId = item["locationId"].ToString();
                }
                else
                {
                    projectId = "0";
                    blocationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
                    locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
                }
            }
        }
    }
}