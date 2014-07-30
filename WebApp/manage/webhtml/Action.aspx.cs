using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using Glibs.Sql;
using System.IO;
using System.Collections;
using System.Text;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;
using WebLogic.Service.Info;

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
                case "index": rs = IndexPage(); break;
                case "process": rs = ProcessPage(); break;

                default: rs = "嘿嘿！你怎么看到我的？？？"; break;
            }

            Response.Write(rs);
        }

        private bool WriteHtml(string htmlStr, string dirPath, string fileName)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = dirPath + "/" + fileName;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, htmlStr, Encoding.UTF8);

            return File.Exists(filePath);
        }

        private string IndexPage()
        {
            List<Dictionary<string, object>> prices = new ParameterLogic().GetList("PriceLevel");
            List<Dictionary<string, object>> regions = new LocationLogic().GetList("001001001001001001");
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();

            Hashtable content = new Hashtable();
            content.Add("webmsg", msgs);
            content.Add("prices", prices);
            content.Add("regions", regions);

            string htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates", content);
            string dirPath = Server.MapPath(@"~/webhtml");
            string fileName = @"index.html";

            return JsonDo.Message(WriteHtml(htmlStr, dirPath, fileName) ? "1" : "0");
        }

        private string ProcessPage()
        {
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();

            Hashtable content = new Hashtable();
            content.Add("webmsg", msgs);
            content.Add("list", list);

            string htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/process", content);
            string dirPath = Server.MapPath(@"~/webhtml/process");
            string fileName = @"index.html";

            return JsonDo.Message(WriteHtml(htmlStr, dirPath, fileName) ? "1" : "0");
        }
    }
}