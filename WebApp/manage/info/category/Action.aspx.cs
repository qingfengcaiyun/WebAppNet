using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Info;

namespace WebApp.manage.info.category
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "tree": rs = Tree(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                default: rs = TreeGrid(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            return new CategoryLogic().GetTree("0");
        }

        private string TreeGrid()
        {
            return new CategoryLogic().GetTreeGrid("0");
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["cateId"].ToString()) > 0)
            {
                return JsonDo.Message(new CategoryLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new CategoryLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string cateNo = WebPageCore.GetRequest("cateNo");

            if (RegexDo.IsInt32(cateNo))
            {
                return JsonDo.Message(new CategoryLogic().Delete(cateNo) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}