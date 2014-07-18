using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class WebMsgDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public WebMsgDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetMsgs()
        {
            this.sql = @"select [msgId],[msgKey],[msgValue] from [Sys_WebMsg]";

            List<Dictionary<string, object>> list = this.db.GetDataTable(this.sql, null);

            if (list != null && list.Count > 0)
            {
                Dictionary<string, object> msgs = new Dictionary<string, object>();

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    msgs.Add(list[i]["msgKey"].ToString(), list[i]["msgValue"].ToString());
                }

                return msgs;
            }
            else
            {
                return null;
            }
        }

        public bool Save(Dictionary<string, object> msgs)
        {
            if (msgs != null && msgs.Count > 0)
            {
                this.sql = @"delete from [Sys_WebMsg]";

                this.db.Update(this.sql, null);

                this.sql = @"insert into [Sys_WebMsg] ([msgKey],[msgValue])values(@msgKey,@msgValue)";

                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();
                Dictionary<string, object> param = null;

                foreach (KeyValuePair<string, object> kv in msgs)
                {
                    param = new Dictionary<string, object>();

                    param.Add("msgKey", kv.Key);
                    param.Add("msgValue", kv.Value);

                    paramList.Add(param);
                }

                this.db.Batch(this.sql, paramList);
            }

            return true;
        }
    }
}
