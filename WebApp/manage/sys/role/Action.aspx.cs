using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.sys.role
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
                default: rs = DataGrid(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            return new RoleLogic().GetTree();
        }

        private string DataGrid()
        {
            return JsonDo.ListToJSON(new RoleLogic().GetList(""));
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["roleId"].ToString()) > 0)
            {
                return JsonDo.Message(new RoleLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new RoleLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string roleId = WebPageCore.GetRequest("roleId");

            if (RegexDo.IsInt32(roleId))
            {
                return JsonDo.Message(new RoleLogic().Delete(Int32.Parse(roleId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}