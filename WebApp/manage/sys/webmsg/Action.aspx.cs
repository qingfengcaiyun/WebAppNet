using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.sys.webmsg
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
            }

            Response.Write(rs);
        }

        private string One()
        {
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();

            if (msgs != null && msgs.Count > 0)
            {
                return JsonDo.DictionaryToJSON(new WebMsgLogic().GetMsgs());
            }
            else
            {
                return "";
            }
        }

        private string Save()
        {
            Dictionary<string, object> msgs = WebPageCore.GetParameters();
            return JsonDo.Message(new WebMsgLogic().Save(msgs) ? "1" : "0");
        }
    }
}