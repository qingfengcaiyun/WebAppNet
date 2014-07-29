using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;

namespace WebApp.manage.renovation.article
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
            string processId = WebPageCore.GetRequest("processId");
            string msg = WebPageCore.GetRequest("msg");

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            return new ArticleLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), Int32.Parse(processId), msg);
        }

        private string One()
        {
            string raId = WebPageCore.GetRequest("raId");

            Dictionary<string, object> one = new ArticleLogic().GetOne(Int32.Parse(raId));

            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

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

            if (Int32.Parse(content["raId"].ToString()) == 0)
            {
                return JsonDo.Message(new ArticleLogic().Insert(content) > 0 ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new ArticleLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string raId = WebPageCore.GetRequest("raId");

            if (RegexDo.IsInt32(raId))
            {
                return JsonDo.Message(new ArticleLogic().Delete(Int32.Parse(raId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}