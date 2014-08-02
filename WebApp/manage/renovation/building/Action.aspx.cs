using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.building
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
                default: rs = GetPage(); break;
            }

            Response.Write(rs);
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

            return new BuildingsLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), Int32.Parse(locationId), msg);
        }

        private string One()
        {
            string buildingsId = WebPageCore.GetRequest("buildingsId");

            Dictionary<string, object> one = new BuildingsLogic().GetOne(Int32.Parse(buildingsId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["buildingsId"].ToString()) == 0)
            {
                return JsonDo.Message(new BuildingsLogic().Insert(content) > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new BuildingsLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string buildingsId = WebPageCore.GetRequest("buildingsId");

            if (RegexDo.IsInt32(buildingsId))
            {
                return JsonDo.Message(new BuildingsLogic().Delete(Int64.Parse(buildingsId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}