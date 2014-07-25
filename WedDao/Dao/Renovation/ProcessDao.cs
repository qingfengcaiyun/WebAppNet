using System;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WebDao.Dao.Renovation
{
    public class ProcessDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ProcessDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int processId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processId");
            this.s.AddField("processName");
            this.s.AddField("processNo");
            this.s.AddField("parentNo");
            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "processId", "=", "@processId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", processId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processId");
            this.s.AddField("processName");
            this.s.AddField("processNo");
            this.s.AddField("parentNo");
            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "parentNo", "like", "@parentNo+'%'");

            this.s.AddOrderBy("processNo", true);

            this.sql = this.s.SqlSelect();

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(string processNo)
        {
            this.s = new SqlBuilder();
            this.s.AddTable("Renovation_Process");
            this.s.AddField("processId");
            this.s.AddWhere("", "", "processNo", "like", "@processNo+'%'");
            string processIds = this.s.SqlSelect();

            this.s = new SqlBuilder();
            this.s.AddTable("Renovation_Article");
            this.s.AddField("processId");
            this.s.AddWhere("", "", "processId", "in", "(" + processIds + ")");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();
            this.s.AddTable("Renovation_Process");
            this.s.AddWhere("", "", "processNo", "like", "@processNo+'%'");

            this.sql = this.sql + ";" + this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("processNo", processNo);
            this.param.Add("processId", 0);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "processNo", "=", "@processNo");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("processNo", content["processNo"]);
            this.param.Add("isLeaf", 0);

            this.db.Update(this.sql, this.param);

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processName");
            this.s.AddField("processNo");
            this.s.AddField("parentNo");
            this.s.AddField("isLeaf");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("processNo", content["processNo"]);
            this.param.Add("processName", content["processName"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("isLeaf", 1);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            Dictionary<string, object> proc = this.GetOne(Int32.Parse(content["processId"].ToString()));

            if (!proc["processNo"].ToString().StartsWith(content["parentNo"].ToString()))
            {
                List<Dictionary<string, object>> list = this.GetList(proc["processNo"].ToString());

                if (list != null && list.Count > 0)
                {
                    List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();
                    Dictionary<string, object> item = null;

                    string parentNo = content["processNo"].ToString();

                    for (int i = 0, j = list.Count; i < j; i++)
                    {
                        item = list[i];
                        this.param = new Dictionary<string, object>();

                        this.param.Add("processNo", parentNo + item["processNo"].ToString().Substring(proc["processNo"].ToString().Length));
                        this.param.Add("parentNo", parentNo + item["parentNo"].ToString().Substring(proc["processNo"].ToString().Length));
                        this.param.Add("processId", Int32.Parse(item["processId"].ToString()));

                        paramList.Add(this.param);
                    }

                    this.s = new SqlBuilder();

                    this.s.AddTable("Renovation_Process");

                    this.s.AddField("processName");
                    this.s.AddField("processNo");
                    this.s.AddField("parentNo");

                    this.s.AddWhere("", "", "processId", "=", "@processId");

                    this.sql = this.s.SqlUpdate();
                    this.db.Batch(this.sql, paramList);
                }
            }

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "processNo", "=", "@processNo");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processName");
            this.s.AddField("processNo");
            this.s.AddField("parentNo");

            this.s.AddWhere("", "", "processId", "=", "@processId");

            this.sql = this.sql + ";" + this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("processName", content["processName"]);
            this.param.Add("processNo", content["processNo"]);
            this.param.Add("processId", content["processId"]);
            this.param.Add("isLeaf", 0);

            return this.db.Update(this.sql, this.param);
        }
    }
}
