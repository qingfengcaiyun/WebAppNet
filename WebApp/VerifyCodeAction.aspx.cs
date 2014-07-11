using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp
{
    public partial class VerifyCodeAction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VerifyCode v = new VerifyCode();

            v.Length = 4;
            v.FontSize = 14;
            v.Chaos = true;
            v.BackgroundColor = Color.White;
            v.ChaosColor = Color.LightGray;
            v.Padding = 2;
            string code = v.CreateVerifyCode();                //取随机码
            v.CreateImageOnPage(code, this.Context);        // 输出图片

            Session.Add("realcode", code);
        }
    }
}