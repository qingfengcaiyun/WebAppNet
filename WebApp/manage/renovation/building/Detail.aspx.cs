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
        public string buildingsId;
        public string locationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.buildingsId = WebPageCore.GetRequest("buildingsId");

                if (!RegexDo.IsInt32(this.buildingsId))
                {
                    this.buildingsId = "0";
                }

                this.locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
            }
        }
    }
}