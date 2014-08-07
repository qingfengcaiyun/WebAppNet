using System.Collections;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.System;

namespace WebHtml.html
{
    public class ActivityPage
    {
        public static bool CreateList(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            string enName = location["enName"].ToString();
            string dirPath = WebPageCore.GetMapPath(@"~/" + enName + @"/activity/list");
            Hashtable content = null;
            string htmlStr = "";

            ActivityLogic aLogic = new ActivityLogic();

            PageRecords pr = aLogic.GetPage(10, 1, locationId, "");
            int pageCount = pr.PageCount;

            if (pageCount > 1)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pr = aLogic.GetPage(10, i, locationId, "");
                    pr.BuildIndexPage("list");

                    content = new Hashtable();
                    content.Add("list", pr.PageResult);
                    content.Add("webmsg", msgs);
                    content.Add("indexPage", pr.IndexPage);

                    htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/activity", content);
                    HtmlDo.WriteHtml(htmlStr, dirPath, "list_" + i + ".html");
                }
            }
            else
            {
                content = new Hashtable();
                content.Add("list", pr.PageResult);
                content.Add("webmsg", msgs);
                content.Add("indexPage", "");

                htmlStr = VelocityDo.BuildStringByTemplate("list.vm", @"~/templates/" + enName + @"/activity", content);
                HtmlDo.WriteHtml(htmlStr, dirPath, "list_1.html");
            }

            return true;
        }

        public static bool CreateDetail(int locationId)
        {
            Dictionary<string, object> location = new LocationLogic().GetOne(locationId);
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(locationId);
            string enName = location["enName"].ToString();
            string dirPath = WebPageCore.GetMapPath(@"~/" + enName + @"/activity/detail");

            List<Dictionary<string, object>> list = new ActivityLogic().GetList("", locationId);

            if (list != null && list.Count > 0)
            {
                Hashtable content = null;
                string htmlStr = "";

                for (int i = 0; i < list.Count; i++)
                {
                    list[i]["content"] = JsonDo.UndoChar(list[i]["content"].ToString()).Replace("/attached", "http://www.zxrrt.com/attached");

                    content = new Hashtable();
                    content.Add("content", list[i]["content"]);
                    content.Add("webmsg", msgs);

                    htmlStr = VelocityDo.BuildStringByTemplate("detail.vm", @"~/templates/" + enName + @"/activity", content);
                    HtmlDo.WriteHtml(htmlStr, dirPath, "detail_" + list[i]["actId"].ToString() + ".html");
                }
            }

            return true;
        }
    }
}
