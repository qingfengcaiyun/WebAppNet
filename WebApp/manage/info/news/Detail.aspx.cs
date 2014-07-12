﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.info.article
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string newsId = WebPageCore.GetRequest("newsId");
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("newsId", newsId);

                Response.Write(VelocityDo.BuildStringByTemplate("detail.vm", @"~/templates/manage/info/news", content));
            }
        }
    }
}