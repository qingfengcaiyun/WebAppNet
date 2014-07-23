﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;

namespace WebApp.manage.renovation.project
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string cateId = WebPageCore.GetRequest("cateId");

                if (!RegexDo.IsInt32(cateId))
                {
                    cateId = "1";
                }

                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("cityId", cUser["locationId"]);
                content.Add("cateId", cateId);



                string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
                className = className.Substring(className.LastIndexOf('.') + 1).ToLower();

                Response.Write(VelocityDo.BuildStringByTemplate(className + ".vm", @"~/templates/" + nameSpace, content));
            }
        }
    }
}