using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebDao.Dao.System;
using WebLogic.Service.System;

namespace WebApp
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.ContentEncoding = Encoding.UTF8;
            //Response.ContentType = "text/html; charset=utf-8";
            //Response.Write(Cryption.GetPassword("e10adc3949ba59abbe56e057f20f883e") + "<br />");

            //Response.Write(Cryption.Encrypt("root")+"<br />");

            //Response.Write(Cryption.Encrypt(Cryption.Decrypt("root")) + "<br />");

            //Response.Write(System.AppDomain.CurrentDomain.BaseDirectory+@"templates\manage");

            //Response.Write(Server.MapPath("attached"));

            //string str = "001001003001001002";
            //int i = str.IndexOf("001");
            //Response.Write(i);

            /*
            string str = "";
            //取得当前方法命名空间
            str += "命名空间名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "\n";
            //取得当前方法类全名 包括命名空间
            str += "类名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "\n";
            //取得当前方法名
            str += "方法名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n";
            str += "\n";

            StackTrace ss = new StackTrace(true);
            MethodBase mb = ss.GetFrame(1).GetMethod();
            //取得父方法命名空间
            str += mb.DeclaringType.Namespace + "\n";
            //取得父方法类名
            str += mb.DeclaringType.Name + "\n";
            //取得父方法类全名
            str += mb.DeclaringType.FullName + "\n";
            //取得父方法名
            str += mb.Name + "\n";
            

            //Response.Write(WebPageCore.GetClassName());


            List<Dictionary<string, object>> locals = new LocationLogic().GetList("001001001001001001");

            List<string> l = new List<string>();

            if (locals != null && locals.Count > 0)
            {
                for (int i = 0; i < locals.Count; i++)
                {
                    l.Add(locals[i]["cnName"].ToString());
                }
            }

            Hashtable content = new Hashtable();
            content.Add("regions", locals);

            Response.Write(VelocityDo.BuildStringByTemplate("test.vm", @"~/templates/", content));
             * 

            string ip = Request.UserHostAddress;
            string ipfilePath = Server.MapPath(@"~/libs/qqwry.dat");
            IpUtil ipSearch = new IpUtil(ipfilePath);
            IpUtil.IPLocation loc = ipSearch.GetIPLocation(ip);

            Response.Write("你查的ip是：" + ip + " 地理位置：" + loc.country + " - " + loc.area);
             * 
             * */

            string lType = "city";
            LocationType localType;

            foreach (string s in Enum.GetNames(typeof(LocationType)))
            {
                if (string.CompareOrdinal(s.ToLower(), lType.ToLower()) == 0)
                {
                    localType = (LocationType)Enum.Parse(typeof(LocationType), s);

                    Response.Write(localType.ToString());
                }
            }


            
        }
    }
}