using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.System;

namespace WebHtml.html
{
    public class NewsPage
    {
        public static bool CreateIndex(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            string enName = location["enName"].ToString();
            string cityId = new LocationLogic().GetParentIdString(locationId) + "," + new LocationLogic().GetSubIdArray(locationId);

            string dirPath = WebPageCore.GetMapPath(@"~/webhtml/" + enName + @"/news");

            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            List<Dictionary<string, object>> cates = new CategoryLogic().GetList("001");

            NewsLogic nLogic = new NewsLogic();
            PageRecords pr = nLogic.GetPage(20, 1, 1, cityId, "");
            List<Dictionary<string, object>> temp = null;
            Hashtable content = new Hashtable();
            string htmlStr = "";

            int pageCnt = pr.PageCount;
            if (pageCnt > 1)
            {
                for (int i = 1; i <= pageCnt; i++)
                {
                    pr = nLogic.GetPage(20, i, 1, cityId, "");
                    temp = pr.PageResult;

                    if (temp != null && temp.Count > 0)
                    {
                        foreach (Dictionary<string, object> t in temp)
                        {
                            string html = WebPageCore.ClearHTML(t["content"].ToString());
                            t.Add("summary", (html.Length > 200 ? html.Substring(0, 200) + "……" : html));
                            t.Add("timeStr", DateTime.Parse(t["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                        }
                    }

                    pr.BuildIndexPage("index");

                    content = new Hashtable();

                    content.Add("webmsg", msgs);
                    content.Add("cates", cates);
                    content.Add("news", pr.PageResult);
                    content.Add("indexPage", pr.IndexPage);

                    htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/" + enName + @"/news", content);
                    HtmlDo.WriteHtml(htmlStr, dirPath, "index_" + pr.CurrentPage + ".html");
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
                        t.Add("timeStr", DateTime.Parse(t["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                    }
                }

                content.Clear();
                content.Add("webmsg", msgs);
                content.Add("cates", cates);
                content.Add("news", pr.PageResult);
                content.Add("indexPage", "");

                htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/" + enName + @"/news", content);
                HtmlDo.WriteHtml(htmlStr, dirPath, "index_1.html");
            }

            return true;
        }

        public static bool CreateList(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            string enName = location["enName"].ToString();
            string cityId = new LocationLogic().GetParentIdString(locationId) + "," + new LocationLogic().GetSubIdArray(locationId);
            string dirPath = WebPageCore.GetMapPath(@"~/webhtml/" + enName + @"/news/list");
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            List<Dictionary<string, object>> cates = new CategoryLogic().GetList("001");
            for (int i = 0; i < cates.Count; i++)
            {
                cates[i].Add("cls", "CateOff");
            }

            if (cates != null && cates.Count > 0)
            {
                PageRecords pr = null;
                NewsLogic nLogic = new NewsLogic();
                Hashtable content = null;
                List<Dictionary<string, object>> temp = null;
                string htmlStr = "";
                int pageCount = 0;

                foreach (Dictionary<string, object> cate in cates)
                {
                    cate["cls"] = "CateOn";

                    pr = nLogic.GetPage(20, 1, Int32.Parse(cate["cateId"].ToString()), cityId, "");
                    pageCount = pr.PageCount;

                    if (pageCount > 1)
                    {
                        for (int i = 1; i <= pageCount; i++)
                        {
                            pr = nLogic.GetPage(20, i, Int32.Parse(cate["cateId"].ToString()), cityId, "");

                            temp = pr.PageResult;

                            if (temp != null && temp.Count > 0)
                            {
                                foreach (Dictionary<string, object> t in temp)
                                {
                                    string html = WebPageCore.ClearHTML(t["content"].ToString());
                                    t.Add("summary", (html.Length > 200 ? html.Substring(0, 200) + "……" : html));
                                    t.Add("timeStr", DateTime.Parse(t["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                                }
                            }

                            pr.BuildIndexPage("list_" + cate["cateId"].ToString());

                            content = new Hashtable();

                            content.Add("webmsg", msgs);
                            content.Add("cates", cates);
                            content.Add("news", pr.PageResult);
                            content.Add("indexPage", pr.IndexPage);

                            htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/news", content);
                            HtmlDo.WriteHtml(htmlStr, dirPath, "list_" + cate["cateId"].ToString() + "_" + i + ".html");
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
                                t.Add("timeStr", DateTime.Parse(t["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                            }
                        }

                        content = new Hashtable();
                        content.Add("webmsg", msgs);
                        content.Add("cates", cates);
                        content.Add("news", pr.PageResult);
                        content.Add("indexPage", "");

                        htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/news", content);
                        HtmlDo.WriteHtml(htmlStr, dirPath, "list_" + cate["cateId"].ToString() + "_1.html");
                    }

                    cate["cls"] = "CateOff";
                }
            }

            return true;
        }

        public static bool CreateDetail(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            string enName = location["enName"].ToString();
            string cityId = new LocationLogic().GetParentIdString(locationId) + "," + new LocationLogic().GetSubIdArray(locationId);
            string dirPath = WebPageCore.GetMapPath(@"~/webhtml/" + enName + @"/news/detail");

            List<Dictionary<string, object>> cates = new CategoryLogic().GetList("001");
            for (int i = 0; i < cates.Count; i++)
            {
                cates[i].Add("cls", "CateOff");
            }

            NewsLogic nLogic = new NewsLogic();
            List<Dictionary<string, object>> hotlist = nLogic.GetPage(15, 1, 1, cityId, "").PageResult;
            List<Dictionary<string, object>> newsList = nLogic.GetList(1, cityId, "");
            Dictionary<string, object> temp = null;

            if (newsList != null && newsList.Count > 0)
            {
                Hashtable content = null;
                string htmlStr = "";

                for (int i = 0; i < newsList.Count; i++)
                {
                    temp = newsList[i];

                    temp.Add("timeStr", DateTime.Parse(temp["insertTime"].ToString()).ToString("yyyy-MM-dd"));
                    temp["content"] = JsonDo.UndoChar(temp["content"].ToString());

                    if (cates != null && cates.Count > 0)
                    {
                        foreach (Dictionary<string, object> t in cates)
                        {
                            t["cls"] = "CateOff";

                            if (string.CompareOrdinal(t["cateId"].ToString(), temp["cateId"].ToString()) == 0)
                            {
                                t["cls"] = "CateOn";
                            }
                        }
                    }

                    content = new Hashtable();
                    content.Add("webmsg", msgs);
                    content.Add("cates", cates);
                    content.Add("news", temp);
                    content.Add("hotList", hotlist);

                    htmlStr = VelocityDo.BuildStringByTemplate("detail.vm", @"~/templates/" + enName + @"/news", content);
                    HtmlDo.WriteHtml(htmlStr, dirPath, "detail_" + temp["newsId"].ToString() + ".html");
                }
            }

            return true;
        }
    }
}
