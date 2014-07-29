using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.sys.rolefunc
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "save": rs = Save(); break;
                case "delete": rs = List(); break;
                default: rs = List(); break;
            }

            Response.Write(rs);
        }

        private string Save()
        {
            string roleId = WebPageCore.GetRequest("roleId");
            string ids = WebPageCore.GetRequest("ids");

            string[] idss = ids.Split(',');

            Int64[] funcIds = new Int64[idss.Length];

            for (int i = 0; i < idss.Length; i++)
            {
                funcIds[i] = Int64.Parse(idss[i]);
            }

            return JsonDo.Message(new RoleFuncLogic().SaveList(funcIds, Int64.Parse(roleId)) ? "1" : "0");
        }

        private string List()
        {
            string roleId = WebPageCore.GetRequest("roleId");
            return new RoleFuncLogic().GetList(Int32.Parse(roleId));
        }
    }
}