using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
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
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                default: rs = TreeGrid(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)WebPageCore.GetSession("cUser");
            return new FunctionLogic().GetTree("0", Int32.Parse(cUser["userId"].ToString()));
        }

        private string TreeGrid()
        {
            return new FunctionLogic().GetTreeGrid("0");
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["funcId"].ToString()) > 0)
            {
                return JsonDo.Message(new FunctionLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new FunctionLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string funcNo = WebPageCore.GetRequest("funcNo");

            if (RegexDo.IsInt32(funcNo))
            {
                return JsonDo.Message(new FunctionLogic().Delete(funcNo) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}