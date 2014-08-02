using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class CustomAreaDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public CustomAreaDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int areaId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_CustomArea");

            this.s.AddField("areaId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("itemIndex");

            this.s.AddWhere(string.Empty, string.Empty, "areaId", "=", "@areaId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList()
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_CustomArea");

            this.s.AddField("areaId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("itemIndex");

            this.s.AddOrderBy("itemIndex", true);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public Boolean Delete(int areaId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_AreaRelation");

            this.s.AddWhere(string.Empty, string.Empty, "areaId", "=", "@areaId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_CustomArea");

            this.s.AddWhere(string.Empty, string.Empty, "areaId", "=", "@areaId");

            this.sql = this.sql + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_CustomArea");

            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("itemIndex");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_CustomArea");

            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("itemIndex");

            this.s.AddWhere(string.Empty, string.Empty, "areaId", "=", "@areaId");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("areaId", content["areaId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
