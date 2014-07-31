using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;

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
                case "processList": rs = ProcessList(); break;
                case "processDetail": rs = ProcessDetail(); break;
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
            string cityId = new LocationLogic().GetParentIdString(6) + "," + new LocationLogic().GetSubIdArray(6);

            List<Dictionary<string, object>> newsList = new NewsLogic().GetPage(10, 1, 0, cityId, "").PageResult;
            List<Dictionary<string, object>> prices = new ParameterLogic().GetList("PriceLevel");
            List<Dictionary<string, object>> regions = new LocationLogic().GetList("001001001001001001");
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();

            if (newsList != null && newsList.Count > 0)
            {
                for (int i = 0, j = newsList.Count; i < j; i++)
                {
                    if (i % 2 == 0)
                    {
                        newsList[i].Add("cls", "bg2");
                    }
                    else
                    {
                        newsList[i].Add("cls", "bg1");
                    }

                    newsList[i].Add("timeStr", DateTime.Parse(newsList[i]["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                }
            }

            Hashtable content = new Hashtable();
            content.Add("webmsg", msgs);
            content.Add("prices", prices);
            content.Add("regions", regions);
            content.Add("newsList", newsList);

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

        private string ProcessList()
        {
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();
            string htmlStr = string.Empty;
            string dirPath = Server.MapPath(@"~/webhtml/process/list");
            List<Dictionary<string, object>> temp = null;

            PageRecords pr = null;
            Hashtable content = new Hashtable();

            if (list != null && list.Count > 0)
            {
                ArticleLogic logic = new ArticleLogic();

                foreach (Dictionary<string, object> itemList in list)
                {
                    foreach (Dictionary<string, object> item in (List<Dictionary<string, object>>)itemList["itemList"])
                    {
                        item["styleClass"] = "ProcessItemOn";

                        pr = logic.GetPage(20, 1, Int32.Parse(item["processId"].ToString()), "");

                        int processId = Int32.Parse(item["processId"].ToString());
                        int pageCnt = pr.PageCount;

                        if (pageCnt > 1)
                        {
                            for (int cnt = 1; cnt <= pageCnt; cnt++)
                            {
                                pr = logic.GetPage(20, cnt, processId, "");

                                temp = pr.PageResult;

                                if (temp != null && temp.Count > 0)
                                {
                                    foreach (Dictionary<string, object> t in temp)
                                    {
                                        string html = WebPageCore.ClearHTML(t["content"].ToString());
                                        t.Add("summary", (html.Length > 200 ? html.Substring(0, 200) + "……" : html));
                                    }
                                }

                                pr.BuildIndexPage("list/list_" + item["processId"].ToString());

                                content = new Hashtable();
                                content.Add("webmsg", msgs);
                                content.Add("processName", item["processName"].ToString());
                                content.Add("processes", list);
                                content.Add("articles", pr.PageResult);
                                content.Add("indexPage", pr.IndexPage);

                                htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/process", content);
                                WriteHtml(htmlStr, dirPath, "list_" + item["processId"].ToString() + "_" + cnt + ".html");
                            }
                        }
                        else
                        {
                            temp = pr.PageResult;

                            if (temp != null && temp.Count > 0)
                            {
                                foreach (Dictionary<string, object> t in temp)
                                {
                                    string html = WebPageCore.ClearHTML(t["content"].ToString());
                                    t.Add("summary", (html.Length > 200 ? html.Substring(0, 200) + "……" : html));
                                }
                            }

                            content = new Hashtable();
                            content.Add("webmsg", msgs);
                            content.Add("processes", list);
                            content.Add("processName", item["processName"].ToString());
                            content.Add("articles", pr.PageResult);
                            content.Add("indexPage", "");

                            htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/process", content);
                            WriteHtml(htmlStr, dirPath, "list_" + item["processId"].ToString() + "_1.html");
                        }

                        item["styleClass"] = "ProcessItemOff";
                    }
                }
            }

            return JsonDo.Message("1");
        }

        private string ProcessDetail()
        {
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();
            List<Dictionary<string, object>> aList = new ArticleLogic().GetList();
            string htmlStr = string.Empty;
            string dirPath = Server.MapPath(@"~/webhtml/process/detail");

            Hashtable content = new Hashtable();

            if (aList != null && aList.Count > 0)
            {
                foreach (Dictionary<string, object> article in aList)
                {
                    article.Add("timeStr", DateTime.Parse(article["insertTime"].ToString()).ToString("yyyy-MM-dd"));

                    if (list != null && list.Count > 0)
                    {
                        foreach (Dictionary<string, object> itemList in list)
                        {
                            foreach (Dictionary<string, object> item in (List<Dictionary<string, object>>)itemList["itemList"])
                            {
                                if (string.CompareOrdinal(item["processId"].ToString().Trim(), article["processId"].ToString().Trim()) == 0)
                                {
                                    item["styleClass"] = "ProcessItemOn";
                                }
                                else
                                {
                                    item["styleClass"] = "ProcessItemOff";
                                }
                            }
                        }
                    }

                    content.Clear();
                    content.Add("webmsg", msgs);
                    content.Add("article", article);
                    content.Add("processes", list);

                    htmlStr = VelocityDo.BuildStringByTemplate("detail.vm", @"~/templates/process", content);
                    WriteHtml(htmlStr, dirPath, "detail_" + article["raId"].ToString() + ".html");
                }
            }

            return JsonDo.Message("1");
        }
    }
}