using System;
using System.Collections;
using System.Collections.Generic;
using Glibs.Util;
using WebLogic.Service.Info;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;

namespace WebHtml.html
{
    public class IndexPage
    {
        public static bool CreateIndex()
        {
            string cityId = new LocationLogic().GetParentIdString(6) + "," + new LocationLogic().GetSubIdArray(6);

            List<Dictionary<string, object>> newsList = new NewsLogic().GetPage(10, 1, 0, cityId, "").PageResult;
            List<Dictionary<string, object>> prices = new ParameterLogic().GetList("PriceLevel");
            List<Dictionary<string, object>> regions = new LocationLogic().GetList("001001001001001001");
            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs(0);

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
            string dirPath = WebPageCore.GetMapPath(@"~/webhtml");
            string fileName = @"index.html";

            return HtmlDo.WriteHtml(htmlStr, dirPath, fileName);
        }
    }
}
