using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.project
{
    public partial class Pictures : System.Web.UI.Page
    {
        public string projectId;
        public string projectName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                projectId = WebPageCore.GetRequest("projectId");

                if (RegexDo.IsInt64(projectId))
                {
                    Dictionary<string, object> item = new ProjectLogic().GetOne(Int64.Parse(projectId));
                    projectName = item["projectName"].ToString();
                }
                else
                {
                    projectId = "0";
                    projectName = "";
                }
            }
        }
    }
}