using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebHtml.html;

namespace WebApp.manage.webhtml
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "index": rs = WebIndex(); break;
                case "processIndex": rs = ProcessIndex(); break;
                case "processList": rs = ProcessList(); break;
                case "processDetail": rs = ProcessDetail(); break;
                case "newsIndex": rs = NewsIndex(); break;
                case "newsList": rs = NewsList(); break;
                case "newsDetail": rs = NewsDetail(); break;
                case "activityList": rs = ActivityList(); break;
                case "activityDetail": rs = ActivityDetail(); break;
                default: rs = "嘿嘿！你怎么看到我的？？？"; break;
            }

            Response.Write(rs);
        }

        private string WebIndex()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(IndexPage.CreateIndex(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string ProcessIndex()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(ProcessPage.CreateIndex(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string ProcessList()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(ProcessPage.CreateList(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string ProcessDetail()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(ProcessPage.CreateDetail(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string NewsIndex()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(NewsPage.CreateIndex(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string NewsList()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(NewsPage.CreateList(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string NewsDetail()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(NewsPage.CreateDetail(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string ActivityList()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(ActivityPage.CreateList(Int32.Parse(locationId)) ? "1" : "0");
        }

        private string ActivityDetail()
        {
            string locationId = WebPageCore.GetRequest("locationId");
            return JsonDo.Message(ActivityPage.CreateDetail(Int32.Parse(locationId)) ? "1" : "0");
        }

    }
}