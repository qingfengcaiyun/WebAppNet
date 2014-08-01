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
                default: rs = "嘿嘿！你怎么看到我的？？？"; break;
            }

            Response.Write(rs);
        }

        private string WebIndex()
        {
            return JsonDo.Message(IndexPage.CreateIndex() ? "1" : "0");
        }

        private string ProcessIndex()
        {
            return JsonDo.Message(ProcessPage.CreateIndex() ? "1" : "0");
        }

        private string ProcessList()
        {
            return JsonDo.Message(ProcessPage.CreateList() ? "1" : "0");
        }

        private string ProcessDetail()
        {
            return JsonDo.Message(ProcessPage.CreateDetail() ? "1" : "0");
        }
    }
}