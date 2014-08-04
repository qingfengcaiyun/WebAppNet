using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.user.designer
{
    public partial class Detail : System.Web.UI.Page
    {
        public string designerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            designerId = WebPageCore.GetRequest("designerId");

            if (!RegexDo.IsInt64(designerId))
            {
                designerId = "0";
            }
        }
    }
}