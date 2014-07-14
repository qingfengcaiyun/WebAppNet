using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class AdminDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public AdminDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("adminId"));
            s.SqlFields.Add(new SqlField("userId"));
            s.SqlFields.Add(new SqlField("locationId"));
            s.SqlFields.Add(new SqlField("fullName"));
            s.SqlFields.Add(new SqlField("phone"));
            s.SqlFields.Add(new SqlField("email"));
            s.SqlFields.Add(new SqlField("qq"));
            s.SqlFields.Add(new SqlField("insertTime"));
            s.SqlFields.Add(new SqlField("updateTime"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Admin"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "userId", "=", "@userId"));

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("@userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("locationId"));
            s.SqlFields.Add(new SqlField("fullName"));
            s.SqlFields.Add(new SqlField("phone"));
            s.SqlFields.Add(new SqlField("email"));
            s.SqlFields.Add(new SqlField("qq"));
            s.SqlFields.Add(new SqlField("updateTime"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Admin"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "adminId", "=", "@adminId"));

            this.sql = s.SqlUpdate();

            this.param = new Dictionary<string, object>();

            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("email", content["email"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("adminId", content["adminId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
