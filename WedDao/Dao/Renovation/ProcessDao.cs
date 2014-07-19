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

        public ProcessDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int processId)
        {
            this.sql = @"select [processId],[processName],[processNo],[parentNo],[isLeaf] from [Renovation_Process] where [processId]=@processId";

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", processId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.sql = @"select [processId],[processName],[processNo],[parentNo],[isLeaf] from [Renovation_Process] where [parentNo] like @parentNo+'%' order by [processNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int processId)
        {
            this.sql = @"update [Renovation_Article] set [processId]=0 where [processId]=@processId;delete from [Renovation_Process] where [processId]=@processId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", processId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"update [Renovation_Process] set [isLeaf]=0 where [processNo]=@parentNo; insert into [Renovation_Process] ([processName],[processNo],[parentNo],[isLeaf])values(@processName,@processNo,@parentNo,1)";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("processName", content["processName"]);
            this.param.Add("parentNo", content["parentNo"]);

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

                    this.sql = @"update [Renovation_Process] set [processNo]=@processNo,[parentNo]=@parentNo where [processId]=@processId";
                    this.db.Batch(this.sql, paramList);
                }
            }

            this.sql = @"update [Renovation_Process] set [isLeaf]=0 where [processNo]=@parentNo;update [Renovation_Process] set [processName]=@processName,[processNo]=@processNo,[parentNo]=@parentNo where [processId]=@processId";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("processName", content["processName"]);
            this.param.Add("processNo", content["processNo"]);
            this.param.Add("processId", content["processId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
