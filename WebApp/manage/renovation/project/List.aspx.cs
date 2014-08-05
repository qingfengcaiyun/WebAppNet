using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.Users;

namespace WebApp.manage.renovation.project
{
    public partial class List : System.Web.UI.Page
    {
        public string locationId;
        public string memberId;
        public string designerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                designerId = WebPageCore.GetRequest("designerId");

                if (RegexDo.IsInt64(designerId))
                {
                    Dictionary<string, object> d = new DesignerLogic().GetOne(Int64.Parse(designerId));
                    memberId = d["memberId"].ToString();

                    Dictionary<string, object> m = new MemberLogic().GetOne(Int64.Parse(memberId));
                    locationId = m["locationId"].ToString();
                }
                else
                {
                    designerId = "0";
                    memberId = WebPageCore.GetRequest("memberId");

                    if (RegexDo.IsInt64(memberId))
                    {
                        Dictionary<string, object> m = new MemberLogic().GetOne(Int64.Parse(memberId));
                        locationId = m["locationId"].ToString();
                    }
                    else
                    {
                        locationId = ((Dictionary<string, object>)WebPageCore.GetSession("cUser"))["locationId"].ToString();
                        memberId = "0";
                    }
                }
            }
        }
    }
}