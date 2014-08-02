using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class RoleDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public RoleDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int roleId)
        {
            this.s = new SqlBuilder();

            this.s.AddField("roleId");
            this.s.AddField("roleName");
            this.s.AddField("itemIndex");

            this.s.AddTable("Sys_Role");

            this.s.AddWhere(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddField("roleId");
            this.s.AddField("roleName");
            this.s.AddField("itemIndex");

            this.s.AddTable("Sys_Role");

            this.s.AddOrderBy("itemIndex", true);

            this.param = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere(string.Empty, string.Empty, "roleName", "like", "'%'+@msg+'%'");

                this.param.Add("msg", msg);
            }

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int roleId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_RoleFunc");

            this.s.AddWhere(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = this.s.SqlDelete();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Role");

            this.s.AddWhere(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = this.sql + ";" + this.s.SqlDelete() + ";";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("roleName");
            this.s.AddField("itemIndex");

            this.s.AddTable("Sys_Role");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("roleName");
            this.s.AddField("itemIndex");

            this.s.AddTable("Sys_Role");

            this.s.AddWhere(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("roleId", content["roleId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
