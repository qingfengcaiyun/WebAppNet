using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.user.member
{
    public partial class Detail : System.Web.UI.Page
    {
        public string memberId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.memberId = WebPageCore.GetRequest("memberId");

                if (!RegexDo.IsInt32(this.memberId))
                {
                    this.memberId = "0";
                }
            }
        }
    }
}