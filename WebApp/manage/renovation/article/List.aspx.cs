using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.renovation.article
{
    public partial class List : System.Web.UI.Page
    {
        public string processId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.processId = WebPageCore.GetRequest("processId");

                if (!RegexDo.IsInt32(this.processId))
                {
                    this.processId = "1";
                }
            }
        }
    }
}