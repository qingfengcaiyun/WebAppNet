using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.sys.function
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "tree": rs = Tree(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)WebPageCore.GetSession("cUser");
            return new FunctionLogic().GetTree("0", Int32.Parse(cUser["userId"].ToString()));
        }
    }
}