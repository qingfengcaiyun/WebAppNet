using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.info.activity
{
    public partial class Detail : System.Web.UI.Page
    {
        public string actId;
        public string locationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.actId = WebPageCore.GetRequest("actId");

                if (!RegexDo.IsInt32(this.actId))
                {
                    this.actId = "0";
                }

                this.locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
            }
        }
    }
}