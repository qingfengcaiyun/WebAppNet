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

        public ClientDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int clientId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("User_Client", "c");
            s.AddTable("Sys_User", "u");
            s.AddTable("Sys_Location", "l");

            s.AddField("l", "cnName", "location");
            s.AddField("u", "userName");
            s.AddField("u", "lastLogin");
            s.AddField("c", "clientId");
            s.AddField("c", "userId");
            s.AddField("c", "locationId");
            s.AddField("c", "fullName");
            s.AddField("c", "sex");
            s.AddField("c", "address");
            s.AddField("c", "phone");
            s.AddField("c", "qq");
            s.AddField("c", "email");
            s.AddField("c", "isDeleted");
            s.AddField("c", "insertTime");
            s.AddField("c", "updateTime");

            s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "u", "userId", "=", "c", "userId");
            s.AddWhere("and", "c", "clientId", "=", "@clientId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("clientId", clientId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Delete(int userId, int clientId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Sys_Users");

            s.AddField("isDeleted");

            s.AddWhere("", "", "userid", "=", "@userId");

            this.sql = s.SqlUpdate();

            s = new SqlBuilder();

            s.AddTable("User_Client");

            s.AddField("isDeleted");

            s.AddWhere("", "", "clientId", "=", "@clientId");

            this.sql = this.sql + ";" + s.SqlUpdate() + ";";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("clientId", clientId);
            this.param.Add("isDeleted", 1);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("User_Client");

            s.AddField("userId");
            s.AddField("locationId");
            s.AddField("fullName");
            s.AddField("sex");
            s.AddField("address");
            s.AddField("phone");
            s.AddField("qq");
            s.AddField("email");
            s.AddField("isDeleted");
            s.AddField("insertTime");
            s.AddField("updateTime");

            this.sql = s.SqlInsert();

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
            SqlBuilder s = new SqlBuilder();

            s.AddTable("User_Client");

            s.AddField("userId");
            s.AddField("locationId");
            s.AddField("fullName");
            s.AddField("sex");
            s.AddField("address");
            s.AddField("phone");
            s.AddField("qq");
            s.AddField("email");
            s.AddField("updateTime");

            s.AddWhere("", "", "clientId", "=", "@clientId");

            this.sql = s.SqlUpdate();

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
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Sys_User", "u");
            s.AddTable("User_Client", "c");
            s.AddTable("Sys_Locations", "l");

            s.AddField("u", "userId");
            s.AddField("u", "userName");
            s.AddField("u", "lastLogin");
            s.AddField("c", "clientId");
            s.AddField("c", "locationId");
            s.AddField("c", "fullName");
            s.AddField("c", "phone");
            s.AddField("l", "cnName", "location");

            s.AddOrderBy("c", "fullName", true);

            s.AddWhere("", "u", "userId", "=", "u", "userId");
            s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "c", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "c", "isDeleted", "=", "0");
            s.AddWhere("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();
            if (locationId > 0)
            {
                s.AddWhere("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            s.AddWhere("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            s.AddWhere("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.sql = s.SqlSelect();

            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, String msg, int locationId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Sys_User", "u");
            s.AddTable("User_Client", "c");
            s.AddTable("Sys_Locations", "l");

            s.AddField("u", "userId");
            s.AddField("u", "userName");
            s.AddField("u", "lastLogin");
            s.AddField("c", "clientId");
            s.AddField("c", "locationId");
            s.AddField("c", "fullName");
            s.AddField("c", "phone");
            s.AddField("l", "cnName", "location");

            s.AddOrderBy("c", "fullName", true);

            s.SetTagField("c", "clientId");

            s.AddWhere("", "u", "userId", "=", "u", "userId");
            s.AddWhere("and", "u", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "c", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "c", "isDeleted", "=", "0");
            s.AddWhere("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();

            if (locationId > 0)
            {
                s.AddWhere("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            s.AddWhere("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            s.AddWhere("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.param.Add("msg", msg);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = s.SqlCount();

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();

            this.sql = s.SqlPage(pr.PageSize, pr.StartIndex);
            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }
    }
}
