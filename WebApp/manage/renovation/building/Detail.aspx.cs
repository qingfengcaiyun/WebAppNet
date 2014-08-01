using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.renovation.building
{
    public partial class Detail : System.Web.UI.Page
    {
        public string buildingId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.buildingId = WebPageCore.GetRequest("buildingId");

                if (!RegexDo.IsInt32(this.buildingId))
                {
                    this.buildingId = "0";
                }
            }
        }
    }
}