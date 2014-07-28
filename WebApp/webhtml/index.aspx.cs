using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glibs.Util;
using WebLogic.Service.Renovation;
using WebLogic.Service.System;

namespace WebApp.webhtml
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Dictionary<string, object>> prices = new ParameterLogic().GetList("PriceLevel");

            List<Dictionary<string, object>> regions = new LocationLogic().GetList("001001001001001001");

            Dictionary<string, object> msgs = new WebMsgLogic().GetMsgs();

            Hashtable content = new Hashtable();
            content.Add("webmsg", msgs);
            content.Add("prices", prices);
            content.Add("regions", regions);

            string nameSpace = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            nameSpace = nameSpace.Substring(nameSpace.IndexOf('.') + 1).Replace('.', '/');

            string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            className = className.Substring(className.LastIndexOf('.') + 1).ToLower();

            Response.Write(VelocityDo.BuildStringByTemplate(className + ".vm", @"~/templates/" + nameSpace, content));
        }
    }
}