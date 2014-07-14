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

            s.SqlFields = new SqlField();
            s.SqlFields.Add("adminId");
            s.SqlFields.Add("userId");
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("fullName");
            s.SqlFields.Add("phone");
            s.SqlFields.Add("email");
            s.SqlFields.Add("qq");
            s.SqlFields.Add("insertTime");
            s.SqlFields.Add("updateTime");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Admin");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("@userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("fullName");
            s.SqlFields.Add("phone");
            s.SqlFields.Add("email");
            s.SqlFields.Add("qq");
            s.SqlFields.Add("updateTime");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Admin");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "adminId", "=", "@adminId");

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
