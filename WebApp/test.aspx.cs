using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

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

            string str = "001001003001001002";
            int i = str.IndexOf("001");
            Response.Write(i);
        }
    }
}