using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.sys.location
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
                case "treeList": rs = TreeList(); break;
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                default: rs = Tree(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            return new LocationLogic().GetTreeGrid("0");
        }

        private string TreeList()
        {
            string parentNo = WebPageCore.GetRequest("parentNo");
            return new LocationLogic().GetTreeGrid(parentNo);
        }

        private string One()
        {
            string locationId = WebPageCore.GetRequest("locationId");

            return JsonDo.DictionaryToJSON(new LocationLogic().GetOne(Int32.Parse(locationId)));
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();
            if (Int32.Parse(content["locationId"].ToString()) > 0)
            {
                return JsonDo.Message(new LocationLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new LocationLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string locationNo = WebPageCore.GetRequest("locationNo");

            if (RegexDo.IsInt32(locationNo))
            {
                return JsonDo.Message(new LocationLogic().Delete(locationNo) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}