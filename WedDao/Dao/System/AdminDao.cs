using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class AdminDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public AdminDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Admin", "a");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "adminId");
            this.s.AddField("a", "userId");
            this.s.AddField("a", "locationId");
            this.s.AddField("a", "fullName");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "email");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "insertTime");
            this.s.AddField("a", "updateTime");

            this.s.AddWhere(string.Empty, "u", "userId", "=", "a", "userId");
            this.s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "a", "userId", "=", "@userId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("@userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("phone");
            this.s.AddField("email");
            this.s.AddField("qq");
            this.s.AddField("updateTime");

            this.s.AddTable("Sys_Admin");

            this.s.AddWhere(string.Empty, string.Empty, "adminId", "=", "@adminId");

            this.sql = this.s.SqlUpdate();

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

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("phone");
            this.s.AddField("email");
            this.s.AddField("qq");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.s.AddTable("Sys_Admin");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("email", content["email"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("insertTime", DateTime.Now);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Insert(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string cityId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Admin", "a");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("Sys_Location", "l");

            this.s.SetTagField("a", "adminId");

            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "adminId");
            this.s.AddField("a", "userId");
            this.s.AddField("a", "locationId");
            this.s.AddField("a", "fullName");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "email");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "insertTime");
            this.s.AddField("a", "updateTime");

            this.s.AddWhere(string.Empty, "u", "userId", "=", "a", "userId");
            this.s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "a", "userId", "=", "@userId");

            this.s.AddOrderBy("a", "adminId", true);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = this.s.SqlCount();
            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();

            this.sql = this.s.SqlPage(pr.PageSize, pr.StartIndex);

            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }
    }
}
