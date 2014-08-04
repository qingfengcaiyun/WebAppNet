using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Users;

namespace WebApp.manage.user.member
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
            string locationId = WebPageCore.GetRequest("locationId");
            return new MemberLogic().GetTree("", Int32.Parse(locationId));
        }

        private string GetPage()
        {
            string pageNo = WebPageCore.GetRequest("page");
            string pageSize = WebPageCore.GetRequest("rows");
            string locationId = WebPageCore.GetRequest("locationId");
            string msg = WebPageCore.GetRequest("msg");

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            return new MemberLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), msg, Int32.Parse(locationId));
        }

        private string One()
        {
            string memberId = WebPageCore.GetRequest("memberId");

            Dictionary<string, object> one = new MemberLogic().GetOne(Int32.Parse(memberId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["memberId"].ToString()) == 0)
            {
                return JsonDo.Message(new MemberLogic().Insert(content) > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new MemberLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string memberId = WebPageCore.GetRequest("memberId");

            if (RegexDo.IsInt32(memberId))
            {
                return JsonDo.Message(new MemberLogic().Delete(Int64.Parse(memberId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}