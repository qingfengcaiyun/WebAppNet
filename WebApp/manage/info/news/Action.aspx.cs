using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.System;

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
                case "page": rs = GetPage(); break;
                case "one": rs = One(); break;
                default: Session.Abandon(); break;
            }

            Response.Write(rs);
        }

        private string GetPage()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)WebPageCore.GetSession("cUser");

            string pageNo = WebPageCore.GetRequest("page");
            string pageSize = WebPageCore.GetRequest("rows");
            string cateId = WebPageCore.GetRequest("cateId");
            string msg = WebPageCore.GetRequest("msg");
            string cityId = WebPageCore.GetRequest("cityId");

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            cityId = new LocationLogic().GetSubIdArray(Int32.Parse(cityId));

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
                Int64 l = new NewsLogic().Insert(content);

                if (l > 0)
                {
                    string[] cs = content["cateList"].ToString().Split(',');

                    Int64[] cl = new Int64[cs.Length];

                    for (int i = 0; i < cs.Length; i++)
                    {
                        cl[i] = Int64.Parse(cs[i]);
                    }

                    new NewsLogic().SetRelationship(cl, l);
                }

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