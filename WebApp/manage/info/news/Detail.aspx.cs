﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage.info.news
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                object o = WebPageCore.GetSession("fileIds");

                if (o != null)
                {
                    foreach (string id in o.ToString().Split(','))
                    {
                        new FileInfoLogic().Delete(Int64.Parse(id));
                    }

                    WebPageCore.RemoveSession("fileIds");
                }

                string newsId = WebPageCore.GetRequest("newsId");
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("newsId", newsId);

                string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
                className = className.Substring(className.LastIndexOf('.') + 1).ToLower();

                Response.Write(VelocityDo.BuildStringByTemplate(className + ".vm", @"~/templates/" + nameSpace, content));
            }
        }
    }
}