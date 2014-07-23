using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.process
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
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                default: rs = Tree(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            return new ProcessLogic().GetTree("0");
        }

        private string One()
        {
            string processId = WebPageCore.GetRequest("processId");

            return JsonDo.DictionaryToJSON(new ProcessLogic().GetOne(Int32.Parse(processId)));
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();
            if (Int32.Parse(content["processId"].ToString()) > 0)
            {
                return JsonDo.Message(new ProcessLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new ProcessLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string processNo = WebPageCore.GetRequest("processNo");

            if (RegexDo.IsInt32(processNo))
            {
                return JsonDo.Message(new ProcessLogic().Delete("00" + processNo) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}