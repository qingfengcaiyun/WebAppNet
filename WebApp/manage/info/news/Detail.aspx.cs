using System;
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
        public string newsId;

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

                this.newsId = WebPageCore.GetRequest("newsId");
            }
        }
    }
}