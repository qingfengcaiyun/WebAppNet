using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.renovation.article
{
    public partial class Detail : System.Web.UI.Page
    {
        public string raId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.raId = WebPageCore.GetRequest("raId");

                if (!RegexDo.IsInt32(this.raId))
                {
                    this.raId = "0";
                }
            }
        }
    }
}