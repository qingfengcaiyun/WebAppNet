using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.System;

namespace WebApp.manage.info.news
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

            Dictionary<string, object> one = new NewsLogic().GetOne(Int32.Parse(newsId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();
            Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
            content.Add("cityId", cUser["locationId"]);

            string fileIds = WebPageCore.GetSession("fileIds") != null ? WebPageCore.GetSession("fileIds").ToString() : string.Empty;
            string picUrls = WebPageCore.GetRequest("picUrl");
            if (string.IsNullOrEmpty(picUrls))
            {
                content.Add("picUrl", string.Empty);
                content.Add("fileIds", string.Empty);

                if (!string.IsNullOrEmpty(fileIds))
                {
                    foreach (string id in fileIds.Split(','))
                    {
                        new FileInfoLogic().Delete(Int64.Parse(id));
                    }
                }
            }
            else
            {
                string[] pus = picUrls.Split(',');

                content.Add("picUrl", pus[0]);
                content.Add("fileIds", new FileInfoLogic().GetFileIds(pus));

                if (!string.IsNullOrEmpty(fileIds))
                {
                    foreach (string id in fileIds.Split(','))
                    {
                        if (!("," + content["fileIds"].ToString() + ",").Contains("," + id + ","))
                        {
                            new FileInfoLogic().Delete(Int64.Parse(id));
                        }
                    }
                }
            }

            WebPageCore.RemoveSession("fileIds");

            if (Int32.Parse(content["newsId"].ToString()) == 0)
            {
                return JsonDo.Message(new NewsLogic().Insert(content) > 0 ? "1" : "0");
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