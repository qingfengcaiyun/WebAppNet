using System;
using System.Collections;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;

namespace WebHtml.html
{
    public class ProcessPage
    {
        public static bool CreateIndex(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            string enName = location["enName"].ToString();

            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();

            Hashtable content = new Hashtable();
            content.Add("webmsg", msgs);
            content.Add("list", list);

            string htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/" + enName + @"/process", content);
            string dirPath = WebPageCore.GetMapPath(@"~/" + enName + @"/process");
            string fileName = @"index.html";

            return HtmlDo.WriteHtml(htmlStr, dirPath, fileName);
        }

        public static bool CreateList(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            string enName = location["enName"].ToString();

            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();
            string htmlStr = string.Empty;
            string dirPath = WebPageCore.GetMapPath(@"~/" + enName + @"/process/list");
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

                                pr.BuildIndexPage("list_" + item["processId"].ToString());

                                content = new Hashtable();
                                content.Add("webmsg", msgs);
                                content.Add("processName", item["processName"].ToString());
                                content.Add("processes", list);
                                content.Add("articles", pr.PageResult);
                                content.Add("indexPage", pr.IndexPage);

                                htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/process", content);
                                HtmlDo.WriteHtml(htmlStr, dirPath, "list_" + item["processId"].ToString() + "_" + cnt + ".html");
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

                            htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/process", content);
                            HtmlDo.WriteHtml(htmlStr, dirPath, "list_" + item["processId"].ToString() + "_1.html");
                        }

                        item["styleClass"] = "ProcessItemOff";
                    }
                }
            }

            return true;
        }

        public static bool CreateDetail(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            string enName = location["enName"].ToString();

            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            List<Dictionary<string, object>> list = new ProcessLogic().GetListWebHtml();
            List<Dictionary<string, object>> aList = new ArticleLogic().GetList();
            string htmlStr = string.Empty;
            string dirPath = WebPageCore.GetMapPath(@"~/" + enName + @"/process/detail");

            Hashtable content = new Hashtable();

            if (aList != null && aList.Count > 0)
            {
                foreach (Dictionary<string, object> article in aList)
                {
                    article.Add("timeStr", DateTime.Parse(article["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                    article["content"] = JsonDo.UndoChar(article["content"].ToString());
                    article["content"] = article["content"].ToString().Replace("/attached", "http://www.zxrrt.com/attached");

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

                    htmlStr = VelocityDo.BuildStringByTemplate("detail.vm", @"~/templates/" + enName + @"/process", content);
                    HtmlDo.WriteHtml(htmlStr, dirPath, "detail_" + article["raId"].ToString() + ".html");
                }
            }

            return true;
        }
    }
}
