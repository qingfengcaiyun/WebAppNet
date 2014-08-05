using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.parameter
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
                case "treeValue": rs = TreeValue(); break;
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;
                case "list": rs = List(); break;
            }

            Response.Write(rs);
        }

        private string Tree()
        {
            return new ParameterLogic().GetParamTypeJson();
        }

        private string TreeValue()
        {
            string paramKey = WebPageCore.GetRequest("paramKey");
            return new ParameterLogic().GetParamValueJson(paramKey);
        }

        private string One()
        {
            string paramId = WebPageCore.GetRequest("paramId");

            return JsonDo.DictionaryToJSON(new ParameterLogic().GetOne(Int32.Parse(paramId)));
        }

        private string List()
        {
            string paramKey = WebPageCore.GetRequest("paramKey");
            return JsonDo.ListToJSON(new ParameterLogic().GetList(paramKey));
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();
            Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
            content.Add("locationId", cUser["locationId"]);

            if (Int32.Parse(content["paramId"].ToString()) > 0)
            {
                return JsonDo.Message(new ParameterLogic().Update(content) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message(new ParameterLogic().Insert(content) > 0 ? "1" : "0");
            }
        }

        private string Delete()
        {
            string paramId = WebPageCore.GetRequest("paramId");

            if (RegexDo.IsInt32(paramId))
            {
                return JsonDo.Message(new ParameterLogic().Delete(Int32.Parse(paramId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}