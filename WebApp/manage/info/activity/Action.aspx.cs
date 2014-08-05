using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using Glibs.Sql;
using WebLogic.Service.Info;

namespace WebApp.manage.info.activity
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "onIndex": rs = OnIndex(); break;
                case "page": rs = GetPage(); break;
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                default: rs = GetPage(); break;
            }

            Response.Write(rs);
        }

        private string GetPage()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)WebPageCore.GetSession("cUser");

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

            return new ActivityLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), Int32.Parse(locationId), msg);
        }

        private string One()
        {
            string actId = WebPageCore.GetRequest("actId");

            Dictionary<string, object> one = new ActivityLogic().GetOne(Int32.Parse(actId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string OnIndex()
        {
            string actId = WebPageCore.GetRequest("actId");

            return JsonDo.Message(new ActivityLogic().SetOnIndex(Int32.Parse(actId)) ? "1" : "0");
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["actId"].ToString()) == 0)
            {
                return JsonDo.Message(new ActivityLogic().Insert(content) > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new ActivityLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string actId = WebPageCore.GetRequest("actId");

            if (RegexDo.IsInt32(actId))
            {
                return JsonDo.Message(new ActivityLogic().Delete(Int32.Parse(actId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}