using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using Glibs.Sql;
using WebLogic.Service.Users;

namespace WebApp.manage.user.designer
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "page": rs = GetPage(); break;
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                case "tree": rs = Tree(); break;
                default: rs = GetPage(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            string locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
            string memberId = WebPageCore.GetRequest("memberId");

            return new DesignerLogic().GetTree("", Int64.Parse(memberId), Int32.Parse(locationId));
        }

        private string GetPage()
        {
            string pageNo = WebPageCore.GetRequest("page");
            string pageSize = WebPageCore.GetRequest("rows");
            string locationId = WebPageCore.GetRequest("locationId");
            string memberId = WebPageCore.GetRequest("memberId");
            string msg = WebPageCore.GetRequest("msg");

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            return new DesignerLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), msg, Int64.Parse(memberId), Int32.Parse(locationId));
        }

        private string One()
        {
            string designerId = WebPageCore.GetRequest("designerId");

            Dictionary<string, object> one = new DesignerLogic().GetOne(Int64.Parse(designerId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int64.Parse(content["designerId"].ToString()) == 0)
            {
                return JsonDo.Message(new DesignerLogic().Insert(content) > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new DesignerLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string designerId = WebPageCore.GetRequest("designerId");

            if (RegexDo.IsInt32(designerId))
            {
                return JsonDo.Message(new DesignerLogic().Delete(Int64.Parse(designerId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}