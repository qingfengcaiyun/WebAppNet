using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class WebMsgLogic
    {
        private WebMsgDao dao = null;

        public WebMsgLogic()
        {
            this.dao = new WebMsgDao();
        }

        public Dictionary<string, object> GetMsgs(int locationId)
        {
            return this.dao.GetMsgs(locationId);
        }

        public string GetMsgsJson(int locationId)
        {
            return JsonDo.DictionaryToJSON(this.dao.GetMsgs(locationId));
        }

        public bool Save(Dictionary<string, object> msgs, int locationId)
        {
            return this.dao.Save(msgs, locationId);
        }
    }
}
