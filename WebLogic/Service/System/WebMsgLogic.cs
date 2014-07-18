﻿using System.Collections.Generic;
using WedDao.Dao.System;
using Glibs.Sql;

namespace WebLogic.Service.System
{
    public class WebMsgLogic
    {
        private WebMsgDao dao = null;

        public WebMsgLogic()
        {
            this.dao = new WebMsgDao();
        }

        public Dictionary<string, object> GetMsgs()
        {
            return this.dao.GetMsgs();
        }

        public string GetMsgsJson()
        {
            return JsonDo.DictionaryToJSON(this.dao.GetMsgs());
        }

        public bool Save(Dictionary<string, object> msgs)
        {
            return this.dao.Save(msgs);
        }
    }
}
