﻿using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Users
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

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("User_Client");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("clientId");
            s.SqlFields.Add("userId");
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("fullName");
            s.SqlFields.Add("sex");
            s.SqlFields.Add("address");
            s.SqlFields.Add("phone");
            s.SqlFields.Add("qq");
            s.SqlFields.Add("email");
            s.SqlFields.Add("isDeleted");
            s.SqlFields.Add("insertTime");
            s.SqlFields.Add("updateTime");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "clientId", "=", "@clientId");

            this.sql = s.SqlSelect();

            //this.sql = @"select [clientId],[userId],[locationId],[trueName],[sex],[address],[phone],[qq],[email],[isDeleted],[insertTime],[updateTime] from [User_Client] where [clientId]=@clientId";

            this.param = new Dictionary<string, object>();
            this.param.Add("clientId", clientId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Delete(int userId, int clientId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Users");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("isDeleted");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "userid", "=", "@userId");

            this.sql = s.SqlDelete();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("User_Client");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("isDeleted");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "clientId", "=", "@clientId");

            this.sql = this.sql + s.SqlDelete();

            //this.sql = @"update [Sys_Users] set [isDeleted]=@isDeleted where [userId]=@userId;update [User_Client] set [isDeleted]=@isDeleted where [clientId]=@clientId ";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("clientId", clientId);
            this.param.Add("isDeleted", 1);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("User_Client");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("userId");
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("fullName");
            s.SqlFields.Add("sex");
            s.SqlFields.Add("address");
            s.SqlFields.Add("phone");
            s.SqlFields.Add("qq");
            s.SqlFields.Add("email");
            s.SqlFields.Add("isDeleted");
            s.SqlFields.Add("insertTime");
            s.SqlFields.Add("updateTime");

            this.sql = s.SqlInsert();

            //this.sql = @"insert into [User_Client] ([userId],[locationId],[fullName],[sex],[address],[phone],[qq],[email],[isDeleted],[insertTime],[updateTime])values(@userId,@locationId,@fullName,@sex,@address,@phone,@qq,@email,@isDeleted,@insertTime,@updateTime)";

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

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("User_Client");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("userId");
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("fullName");
            s.SqlFields.Add("sex");
            s.SqlFields.Add("address");
            s.SqlFields.Add("phone");
            s.SqlFields.Add("qq");
            s.SqlFields.Add("email");
            s.SqlFields.Add("updateTime");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "clientId", "=", "@clientId");

            this.sql = s.SqlUpdate();

            //this.sql = @"update [User_Client] set [userId]=@userId,[locationId]=@locationId,[fullName]=@fullName,[sex]=@sex,[address]=@address,[phone]=@phone,[qq]=@qq,[email]=@email,[updateTime]=@updateTime where [clientId]=@clientId";

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

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_User", "u");
            s.SqlTable.Add("User_Client", "c");
            s.SqlTable.Add("Sys_Locations", "l");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("u", "userId");
            s.SqlFields.Add("u", "userName");
            s.SqlFields.Add("u", "lastLogin");
            s.SqlFields.Add("c", "clientId");
            s.SqlFields.Add("c", "locationId");
            s.SqlFields.Add("c", "fullName");
            s.SqlFields.Add("c", "phone");
            s.SqlFields.Add("l", "cnName");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("c", "fullName", true);

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "u", "userId", "=", "u", "userId");
            s.SqlWhere.Add("and", "u", "locationId", "=", "l", "locationId");
            s.SqlWhere.Add("and", "c", "locationId", "=", "l", "locationId");
            s.SqlWhere.Add("and", "c", "isDeleted", "=", "0");
            s.SqlWhere.Add("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();
            if (locationId > 0)
            {
                s.SqlWhere.Add("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            s.SqlWhere.Add("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            s.SqlWhere.Add("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.sql = s.SqlSelect();

            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, String msg, int locationId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_User", "u");
            s.SqlTable.Add("User_Client", "c");
            s.SqlTable.Add("Sys_Locations", "l");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("u", "userId");
            s.SqlFields.Add("u", "userName");
            s.SqlFields.Add("u", "lastLogin");
            s.SqlFields.Add("c", "clientId");
            s.SqlFields.Add("c", "locationId");
            s.SqlFields.Add("c", "fullName");
            s.SqlFields.Add("c", "phone");
            s.SqlFields.Add("l", "cnName");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("c", "fullName", true);

            s.SqlTagField = new SqlField();
            s.SqlTagField.Add("c", "clientId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "u", "userId", "=", "u", "userId");
            s.SqlWhere.Add("and", "u", "locationId", "=", "l", "locationId");
            s.SqlWhere.Add("and", "c", "locationId", "=", "l", "locationId");
            s.SqlWhere.Add("and", "c", "isDeleted", "=", "0");
            s.SqlWhere.Add("and", "u", "userType", "=", "'C'");

            this.param = new Dictionary<string, object>();

            if (locationId > 0)
            {
                s.SqlWhere.Add("and", "c", "locationId", "=", "@locationId");
                this.param.Add("locationId", locationId);
            }

            s.SqlWhere.Add("and", "(c", "fullName", "like", "'%'+@msg+'%'");
            s.SqlWhere.Add("or", "c", "phone", "like", "'%'+@msg+'%')");

            this.param.Add("msg", msg);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = s.SqlCount();

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();

            this.sql = s.SqlPage();
            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }
    }
}
