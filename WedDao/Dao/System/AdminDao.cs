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

            s.AddField("adminId");
            s.AddField("userId");
            s.AddField("locationId");
            s.AddField("fullName");
            s.AddField("phone");
            s.AddField("email");
            s.AddField("qq");
            s.AddField("insertTime");
            s.AddField("updateTime");

            s.AddTable("Sys_Admin");

            s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("@userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddField("locationId");
            s.AddField("fullName");
            s.AddField("phone");
            s.AddField("email");
            s.AddField("qq");
            s.AddField("updateTime");

            s.AddTable("Sys_Admin");

            s.AddWhere(string.Empty, string.Empty, "adminId", "=", "@adminId");

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
