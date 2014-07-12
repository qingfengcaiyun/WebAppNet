using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;

namespace WebApp.manage.info.article
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "page": rs = Page(); break;
                case "one": rs = One(); break;
                default: Session.Abandon(); break;
            }

            Response.Write(rs);
        }

        private string Page()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)WebPageCore.GetSession("cUser");

            string pageNo = WebPageCore.GetRequest("pageNo");
            string pageSize = WebPageCore.GetRequest("pageSize");
            string cateId = WebPageCore.GetRequest("cateId");
            string msg = WebPageCore.GetRequest("msg");
            string cityId = cUser["locationId"].ToString();

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            return new NewsLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), Int32.Parse(cateId), cityId, msg);
        }

        private string One()
        {
            string newsId = WebPageCore.GetRequest("newsId");

            return JsonDo.DictionaryToJSON(new NewsLogic().GetOne(Int32.Parse(newsId)));
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["newsId"].ToString()) == 0)
            {
                long l = new NewsLogic().Insert(content);

                return JsonDo.Message(l > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new NewsLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string newsId = WebPageCore.GetRequest("newsId");

            if (RegexDo.IsInt32(newsId))
            {
                return JsonDo.Message(new NewsLogic().Delete(Int32.Parse(newsId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}