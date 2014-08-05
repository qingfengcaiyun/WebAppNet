using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.Renovation;

namespace WebApp.manage.renovation.project
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "page": rs = GetPage(); break;
                case "one": rs = One(); break;
                case "save": rs = Save(); break;
                case "delete": rs = Delete(); break;

                case "saveParam": rs = SaveParam(); break;
                case "delParam": rs = DelParam(); break;
                case "savePic": rs = SavePicture(); break;
                case "delPic": rs = DelPicture(); break;
                case "getParams": rs = GetParams(); break;
                case "getPics": rs = GetPictures(); break;

                default: rs = GetPage(); break;
            }

            Response.Write(rs);
        }

        public string SavePicture()
        {
            string projectId = WebPageCore.GetRequest("projectId");
            string picPath = WebPageCore.GetRequest("picPath");

            return JsonDo.Message(new ProjectLogic().SavePicture(Int64.Parse(projectId), picPath).ToString());
        }

        public string DelPicture()
        {
            string ppcIds = WebPageCore.GetRequest("ppcIds");
            return JsonDo.Message(new ProjectLogic().DelPicture(ppcIds) ? "1" : "0");
        }

        public string SaveParam()
        {
            string paramId = WebPageCore.GetRequest("paramId");
            string projectId = WebPageCore.GetRequest("projectId");
            return JsonDo.Message(new ProjectLogic().SaveParam(Int64.Parse(projectId), Int32.Parse(paramId)).ToString());
        }

        public string DelParam()
        {
            string pptId = WebPageCore.GetRequest("pptId");
            return JsonDo.Message(new ProjectLogic().DelParam(Int64.Parse(pptId)) ? "1" : "0");
        }

        public string GetParams()
        {
            string projectId = WebPageCore.GetRequest("projectId");
            return JsonDo.ListToJSON(new ProjectLogic().GetParams(Int64.Parse(projectId)));
        }

        public string GetPictures()
        {
            string projectId = WebPageCore.GetRequest("projectId");
            return JsonDo.ListToJSON(new ProjectLogic().GetPictures(Int64.Parse(projectId)));
        }

        private string GetPage()
        {
            string pageNo = WebPageCore.GetRequest("page");
            string pageSize = WebPageCore.GetRequest("rows");
            string locationId = WebPageCore.GetRequest("locationId");
            string memberId = WebPageCore.GetRequest("memberId");
            string designerId = WebPageCore.GetRequest("designerId");
            string msg = WebPageCore.GetRequest("msg");

            if (!RegexDo.IsInt32(pageNo))
            {
                pageNo = "1";
            }

            if (!RegexDo.IsInt32(pageSize))
            {
                pageSize = "15";
            }

            return new ProjectLogic().GetPageJson(Int32.Parse(pageSize), Int32.Parse(pageNo), Int32.Parse(locationId), Int64.Parse(memberId), Int64.Parse(designerId), msg);
        }

        private string One()
        {
            string projectId = WebPageCore.GetRequest("projectId");
            Dictionary<string, object> one = new ProjectLogic().GetOne(Int32.Parse(projectId));
            return JsonDo.DictionaryToJSON(one);
        }

        private string Save()
        {
            Dictionary<string, object> content = WebPageCore.GetParameters();

            if (Int32.Parse(content["projectId"].ToString()) == 0)
            {
                if (!content.ContainsKey("clientId"))
                {
                    content.Add("clientId", 1);
                }
                return JsonDo.Message(new ProjectLogic().Insert(content).ToString());
            }
            else
            {
                return JsonDo.Message(new ProjectLogic().Update(content) ? "1" : "0");
            }
        }

        private string Delete()
        {
            string projectId = WebPageCore.GetRequest("projectId");

            if (RegexDo.IsInt32(projectId))
            {
                return JsonDo.Message(new ProjectLogic().Delete(Int64.Parse(projectId)) ? "1" : "0");
            }
            else
            {
                return JsonDo.Message("0");
            }
        }
    }
}