using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Users
{
    public class ClientDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ClientDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int clientId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Client", "c");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("c", "clientId");
            this.s.AddField("c", "userId");
            this.s.AddField("c", "locationId");
            this.s.AddField("c", "fullName");
            this.s.AddField("c", "sex");
            this.s.AddField("c", "address");
            this.s.AddField("c", "phone");
            this.s.AddField("c", "qq");
            this.s.AddField("c", "email");
            this.s.AddField("c", "isDeleted");
            this.s.AddField("c", "insertTime");
            this.s.AddField("c", "updateTime");

            this.s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "u", "userId", "=", "c", "userId");
            this.s.AddWhere("and", "c", "clientId", "=", "@clientId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("clientId", clientId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Delete(int userId, int clientId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Users");

            this.s.AddField("isDeleted");

            this.s.AddWhere("", "", "userid", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();

            this.s.AddTable("User_Client");

            this.s.AddField("isDeleted");

            this.s.AddWhere("", "", "clientId", "=", "@clientId");

            this.sql = this.sql + ";" + this.s.SqlUpdate() + ";";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("clientId", clientId);
            this.param.Add("isDeleted", 1);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Client");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("sex");
            this.s.AddField("address");
            this.s.AddField("phone");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("isDeleted");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("isDeleted", 0);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Client");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("sex");
            this.s.AddField("address");
            this.s.AddField("phone");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("updateTime");

            this.s.AddWhere("", "", "clientId", "=", "@clientId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("clientId", content["clientId"]);

            return this.db.Update(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(String msg, int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("User_Client", "c");
            this.s.AddTable("Sys_Locations", "l");

            this.s.AddField("u", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("c", "clientId");
            this.s.AddField("c", "locationId");
            this.s.AddField("c", "fullName");
            this.s.AddField("c", "phone");
            this.s.AddField("l", "cnName", "location");

            this.s.AddOrderBy("c", "fullName", true);

            this.s.AddWhere("", "u", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "c", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "c", "isDeleted", "=", "0");
            this.s.AddWhere("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();
            if (locationId > 0)
            {
                this.s.AddWhere("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            this.s.AddWhere("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.sql = this.s.SqlSelect();

            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, String msg, int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("User_Client", "c");
            this.s.AddTable("Sys_Locations", "l");

            this.s.AddField("u", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("c", "clientId");
            this.s.AddField("c", "locationId");
            this.s.AddField("c", "fullName");
            this.s.AddField("c", "phone");
            this.s.AddField("l", "cnName", "location");

            this.s.AddOrderBy("c", "fullName", true);

            this.s.SetTagField("c", "clientId");

            this.s.AddWhere("", "u", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "c", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "c", "isDeleted", "=", "0");
            this.s.AddWhere("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();

            if (locationId > 0)
            {
                this.s.AddWhere("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            this.s.AddWhere("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.param.Add("msg", msg);

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
