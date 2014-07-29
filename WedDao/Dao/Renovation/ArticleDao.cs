using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class ArticleDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ArticleDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int raId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article", "a");
            this.s.AddTable("Renovation_Process", "p");

            this.s.AddField("p", "processName");

            this.s.AddField("a", "raId");
            this.s.AddField("a", "processId");
            this.s.AddField("a", "longTitle");
            this.s.AddField("a", "shortTitle");
            this.s.AddField("a", "content");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "picUrl");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "outLink");
            this.s.AddField("a", "isTop");
            this.s.AddField("a", "topTime");
            this.s.AddField("a", "insertTime");
            this.s.AddField("a", "updateTime");

            this.s.AddWhere("", "a", "raId", "=", "@raId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int processId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processNo");

            this.s.AddWhere("", "", "processId", "=", "@processId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processId");

            this.s.AddWhere("", "", "processNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article", "a");
            this.s.AddTable("Renovation_Process", "p");

            this.s.AddField("p", "processName");

            this.s.AddField("a", "raId");
            this.s.AddField("a", "processId");
            this.s.AddField("a", "longTitle");
            this.s.AddField("a", "shortTitle");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "picUrl");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "outLink");
            this.s.AddField("a", "isTop");
            this.s.AddField("a", "topTime");
            this.s.AddField("a", "insertTime");
            this.s.AddField("a", "updateTime");

            this.s.AddWhere("", "(a", "longTitle", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "a", "shortTitle", "like", "'%'+@msg+'%')");
            this.s.AddWhere("and", "a", "processId", "in", "(" + this.sql + ")");

            this.s.AddOrderBy("a", "itemIndex", false);
            this.s.AddOrderBy("a", "insertTime", false);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("msg", msg);
            this.param.Add("processId", processId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int processId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processNo");

            this.s.AddWhere("", "", "processId", "=", "@processId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Process");

            this.s.AddField("processId");

            this.s.AddWhere("", "", "processNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article", "a");
            this.s.AddTable("Renovation_Process", "p");

            this.s.SetTagField("a", "raId");

            this.s.AddField("p", "processName");

            this.s.AddField("a", "raId");
            this.s.AddField("a", "processId");
            this.s.AddField("a", "longTitle");
            this.s.AddField("a", "shortTitle");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "picUrl");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "outLink");
            this.s.AddField("a", "isTop");
            this.s.AddField("a", "topTime");
            this.s.AddField("a", "insertTime");
            this.s.AddField("a", "updateTime");

            this.s.AddWhere("", "a", "processId", "=", "p", "processId");
            this.s.AddWhere("and", "(a", "longTitle", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "a", "shortTitle", "like", "'%'+@msg+'%')");
            this.s.AddWhere("and", "a", "processId", "in", "(" + this.sql + ")");

            this.s.AddOrderBy("a", "itemIndex", false);
            this.s.AddOrderBy("a", "insertTime", false);

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", processId);
            this.param.Add("msg", msg);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.s.SqlCount(), this.param).ToString());
            pr.SetBaseParam();

            pr.PageResult = this.db.GetDataTable(this.s.SqlPage(pr.PageSize, pr.StartIndex), this.param);

            return pr;
        }

        public bool Delete(int raId)
        {
            this.s = new SqlBuilder();
            this.s.AddTable("Renovation_Article");
            this.s.AddWhere("", "", "raId", "=", "@raId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article");

            this.s.AddField("processId");
            this.s.AddField("longTitle");
            this.s.AddField("titleColor");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("readCount");
            this.s.AddField("fileIds");
            this.s.AddField("itemIndex");
            this.s.AddField("outLink");
            this.s.AddField("isTop");
            this.s.AddField("topTime");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", content["processId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("readCount", 0);
            this.param.Add("fileIds", content["fileIds"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article");

            this.s.AddField("processId");
            this.s.AddField("longTitle");
            this.s.AddField("titleColor");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("fileIds");
            this.s.AddField("itemIndex");
            this.s.AddField("outLink");
            this.s.AddField("isTop");
            this.s.AddField("topTime");
            this.s.AddField("updateTime");

            this.s.AddWhere("", "", "raId", "=", "@raId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", content["processId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("fileIds", content["fileIds"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("raId", content["raId"]);

            return this.db.Update(this.sql, this.param);
        }

        public int SetReadCount(int raId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Article");

            this.s.AddField("readCount");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "raId", "=", "@raId");

            this.sql = this.s.SqlIncrease() + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return (int)this.db.GetDataValue(this.sql, this.param);
        }
    }
}
