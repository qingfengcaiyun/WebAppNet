using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.Users;

namespace WebApp.manage.user.designer
{
    public partial class List : System.Web.UI.Page
    {
        public string locationId;
        public string memberId;

        protected void Page_Load(object sender, EventArgs e)
        {
            memberId = WebPageCore.GetRequest("memberId");

            if (RegexDo.IsInt64(memberId))
            {
                Dictionary<string, object> member = new MemberLogic().GetOne(Int64.Parse(memberId));
                locationId = member["locationId"].ToString();
            }
            else
            {
                memberId = "0";
                locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
            }
        }
    }
}