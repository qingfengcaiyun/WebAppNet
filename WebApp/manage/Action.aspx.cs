using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Sql;
using Glibs.Util;
using WebLogic.Service.System;

namespace WebApp.manage
{
    public partial class Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WebPageCore.GetRequest("action");
            string rs = string.Empty;

            switch (action)
            {
                case "login": rs = Login(); break;
                case "getAdmin": rs = GetAdmin(); break;
                case "saveAdmin": rs = SaveAdmin(); break;
                case "modPwd": rs = ModPwd(); break;
                case "logout": rs = Logout(); break;
            }

            Response.Write(rs);
        }

        private string Login()
        {
            string msg = string.Empty;
            string userName = WebPageCore.GetRequest("userName");
            string userPwd = WebPageCore.GetRequest("userPwd");
            string xcode = WebPageCore.GetRequest("xcode");
            string realCode = WebPageCore.GetSession("realcode").ToString();
            if (string.CompareOrdinal(realCode.ToLower(), xcode.ToLower()) == 0)
            {
                if (new UserLogic().Login(userName, userPwd))
                {
                    msg = "1";
                }
                else
                {
                    msg = "2";
                }
            }
            else
            {
                msg = "0";
            }

            return JsonDo.Message(msg);
        }

        private string Logout()
        {
            Session.Abandon();
            return JsonDo.Message("1");
        }

        private string GetAdmin()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
            Dictionary<string, object> admin = new UserLogic().GetAdminByUserId(Int32.Parse(cUser["userId"].ToString()));

            return JsonDo.DictionaryToJSON(admin);
        }

        private string SaveAdmin()
        {
            Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];
            Dictionary<string, object> admin = new UserLogic().GetAdminByUserId(Int32.Parse(cUser["userId"].ToString()));
            Dictionary<string, object> content = WebPageCore.GetParameters();

            content.Add("adminId", admin["adminId"]);
            content.Add("locationId", admin["locationId"]);

            return JsonDo.Message(new UserLogic().SaveAdmin(content) ? "1" : "0");
        }

        public string ModPwd()
        {
            string msg = string.Empty;
            string oldPwd = WebPageCore.GetRequest("oldPwd");
            string newPwd = WebPageCore.GetRequest("newPwd");
            string xcode = WebPageCore.GetRequest("xcode");
            string realCode = Session["realcode"].ToString();
            if (string.CompareOrdinal(realCode.ToLower(), xcode.ToLower()) == 0)
            {
                Dictionary<string, object> cUser = (Dictionary<string, object>)Session["cUser"];

                if (string.CompareOrdinal(Cryption.GetPassword(oldPwd).ToLower(), cUser["userPwd"].ToString().ToLower()) == 0)
                {
                    bool b = new UserLogic().UpdatePwd(newPwd);
                    msg = b ? "3" : "2";
                }
                else
                {
                    msg = "1";
                }
            }
            else
            {
                msg = "0";
            }

            return JsonDo.Message(msg);
        }
    }
}