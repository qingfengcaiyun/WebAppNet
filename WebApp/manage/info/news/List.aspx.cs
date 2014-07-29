using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.info.news
{
    public partial class List : System.Web.UI.Page
    {
        public string cityId;
        public string cateId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.cateId = WebPageCore.GetRequest("cateId");

                if (!RegexDo.IsInt32(this.cateId))
                {
                    this.cateId = "1";
                }

                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                cityId = cUser["locationId"].ToString();
            }
        }
    }
}