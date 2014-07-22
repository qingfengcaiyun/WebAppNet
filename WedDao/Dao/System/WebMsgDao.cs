using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class WebMsgDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public WebMsgDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetMsgs()
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_WebMsg");

            this.s.AddField("msgId");
            this.s.AddField("msgKey");
            this.s.AddField("msgValue");

            this.sql = this.s.SqlSelect();

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
                this.s = new SqlBuilder();

                this.s.AddTable("Sys_WebMsg");

                this.sql = this.s.SqlDelete();

                this.db.Update(this.sql, null);

                this.s = new SqlBuilder();

                this.s.AddTable("Sys_WebMsg");

                this.s.AddField("msgKey");
                this.s.AddField("msgValue");

                this.sql = this.s.SqlInsert();

                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();

                foreach (KeyValuePair<string, object> kv in msgs)
                {
                    this.param = new Dictionary<string, object>();

                    this.param.Add("msgKey", kv.Key);
                    this.param.Add("msgValue", kv.Value);

                    paramList.Add(this.param);
                }

                this.db.Batch(this.sql, paramList);
            }

            return true;
        }
    }
}
