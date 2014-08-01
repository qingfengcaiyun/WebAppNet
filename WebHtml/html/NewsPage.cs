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
        public static bool CreateIndex()
        {
            string dirPath = WebPageCore.GetMapPath(@"~/webhtml/news");
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(6);
            List<Dictionary<string, object>> cates = new CategoryLogic().GetList("0");
            string cityId = new LocationLogic().GetParentIdString(6) + "," + new LocationLogic().GetSubIdArray(6);
            string htmlStr = "";

            int pageSize = 20;

            NewsLogic nLogic = new NewsLogic();
            Hashtable content = null;
            PageRecords pr = nLogic.GetPage(pageSize, 1, 0, cityId, "");
            int pageCount = pr.PageCount;

            if (pageCount > 1)
            {
                for (int i = 1; i < pageCount; i++)
                {
                    pr = nLogic.GetPage(pageSize, i, 0, cityId, "");
                    pr.BuildIndexPage("index");

                    content = new Hashtable();
                    content.Add("webmsg", msgs);
                    content.Add("cates", cates);
                    content.Add("news", pr.PageResult);
                    content.Add("indexPage", pr.IndexPage);

                    htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/news", content);

                    HtmlDo.WriteHtml(htmlStr, dirPath, @"index_" + pr.CurrentPage + @".html");
                }
            }
            else
            {
                content = new Hashtable();
                content.Add("webmsg", msgs);
                content.Add("cates", cates);
                content.Add("news", pr.PageResult);
                content.Add("indexPage", "");

                htmlStr = VelocityDo.BuildStringByTemplate("index.vm", @"~/templates/news", content);

                HtmlDo.WriteHtml(htmlStr, dirPath, @"index_1.html");
            }

            return true;
        }

        public static bool CreateList()
        {
            return true;
        }

        public static bool CreateDetail()
        {
            return true;
        }
    }
}
