using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class RoleDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public RoleDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int roleId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("roleId"));
            s.SqlFields.Add(new SqlField("roleName"));
            s.SqlFields.Add(new SqlField("itemIndex"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Roles"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "roleId", "=", "@roleId"));

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("roleId"));
            s.SqlFields.Add(new SqlField("roleName"));
            s.SqlFields.Add(new SqlField("itemIndex"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Roles"));

            s.SqlOrderBy = new List<SqlOrderBy>();
            s.SqlOrderBy.Add(new SqlOrderBy("itemIndex", true));

            this.param = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(msg))
            {
                s.SqlWhere = new List<SqlWhere>();
                s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "roleName", "like", "'%'+@msg+'%'"));

                this.param.Add("msg", msg);
            }

            this.sql = s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int roleId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_RoleFunc"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "roleId", "=", "@roleId"));

            this.sql = s.SqlDelete();

            s = new SqlBuilder();

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Roles"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "roleId", "=", "@roleId"));

            this.sql = this.sql + s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("roleName"));
            s.SqlFields.Add(new SqlField("itemIndex"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Roles"));

            this.sql = s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("roleName"));
            s.SqlFields.Add(new SqlField("itemIndex"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Roles"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "roleId", "=", "@roleId"));

            this.sql = s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("roleId", content["roleId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
