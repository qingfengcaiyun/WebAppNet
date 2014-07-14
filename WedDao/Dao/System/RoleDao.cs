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

            s.SqlFields = new SqlField();
            s.SqlFields.Add("roleId");
            s.SqlFields.Add("roleName");
            s.SqlFields.Add("itemIndex");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Roles");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("roleId");
            s.SqlFields.Add("roleName");
            s.SqlFields.Add("itemIndex");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Roles");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("itemIndex", true);

            this.param = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(msg))
            {
                s.SqlWhere = new SqlWhere();
                s.SqlWhere.Add(string.Empty, string.Empty, "roleName", "like", "'%'+@msg+'%'");

                this.param.Add("msg", msg);
            }

            this.sql = s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int roleId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_RoleFunc");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = s.SqlDelete();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Roles");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = this.sql + s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("roleName");
            s.SqlFields.Add("itemIndex");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Roles");

            this.sql = s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("roleName");
            s.SqlFields.Add("itemIndex");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Roles");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "roleId", "=", "@roleId");

            this.sql = s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("roleId", content["roleId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
