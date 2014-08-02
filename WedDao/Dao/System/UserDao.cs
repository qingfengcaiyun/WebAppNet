using System;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WebDao.Dao.System
{
    public class UserDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public UserDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(string userName, string userPwd)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("u", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "userPwd");
            this.s.AddField("u", "userType");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("u", "locationId");
            this.s.AddField("u", "isDeleted");
            this.s.AddField("u", "isLocked");

            this.s.AddField("l", "cnName", "location");

            this.s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "u", "userName", "=", "@userName");
            this.s.AddWhere("and", "u", "userPwd", "=", "@userPwd");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("userName", userName);
            this.param.Add("userPwd", userPwd);

            return this.db.GetDataRow(sql, param);
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("u", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "userPwd");
            this.s.AddField("u", "userType");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("u", "locationId");
            this.s.AddField("u", "isDeleted");
            this.s.AddField("u", "isLocked");

            this.s.AddField("l", "cnName", "location");

            this.s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "u", "userId", "=", "@userId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool UpdateUserPwd(string userPwd, string md5Pwd, int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddField("userPwd");
            this.s.AddField("md5Pwd");

            this.s.AddTable("Sys_User");

            this.s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userPwd", userPwd);
            this.param.Add("md5Pwd", md5Pwd);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetLastlogin(int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddField("lastLogin");

            this.s.AddTable("Sys_User");

            this.s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("lastLogin", DateTime.Now);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public Boolean UpdateUserPwds()
        {
            this.s = new SqlBuilder();

            this.s.AddField("userId");
            this.s.AddField("md5Pwd");

            this.s.AddTable("Sys_User");

            this.sql = this.s.SqlSelect();

            List<Dictionary<string, object>> list = this.db.GetDataTable(this.sql, null);

            if (list.Count > 0)
            {
                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 0, j = list.Count; i < j; j++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("userPwd", Cryption.GetPassword(list[i]["md5Pwd"].ToString()));
                    this.param.Add("userId", Int32.Parse(list[i]["userId"].ToString()));

                    paramsList.Add(this.param);
                }

                s = new SqlBuilder();

                this.s.AddTable("Sys_User");

                this.s.AddField("userPwd");

                this.s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

                this.sql = this.s.SqlUpdate();

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("userName");
            this.s.AddField("userPwd");
            this.s.AddField("md5Pwd");
            this.s.AddField("userType");
            this.s.AddField("lastLogin");
            this.s.AddField("locationId");
            this.s.AddField("isDeleted");
            this.s.AddField("isLocked");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.s.AddTable("Sys_User");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("userName", content["userName"]);
            this.param.Add("userPwd", content["userPwd"]);
            this.param.Add("md5Pwd", content["md5Pwd"]);
            this.param.Add("userType", content["userType"]);
            this.param.Add("lastLogin", DateTime.Now);
            this.param.Add("locationId", content["qq"]);
            this.param.Add("isDeleted", 0);
            this.param.Add("isLocked", 0);
            this.param.Add("insertTime", DateTime.Now);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddField("userName");
            this.s.AddField("userPwd");
            this.s.AddField("md5Pwd");
            this.s.AddField("userType");
            this.s.AddField("locationId");
            this.s.AddField("updateTime");

            this.s.AddTable("Sys_User");

            this.s.AddWhere("", "", "userId", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userName", content["userName"]);
            this.param.Add("userPwd", content["userPwd"]);
            this.param.Add("md5Pwd", content["md5Pwd"]);
            this.param.Add("userType", content["userType"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("userId", content["userId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool Delete(int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddField("isDeleted");

            this.s.AddTable("Sys_User");

            this.s.AddWhere("", "", "userId", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isDeleted", 1);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetLocked(int userId, bool isLock)
        {
            this.s = new SqlBuilder();

            this.s.AddField("isLocked");

            this.s.AddTable("Sys_User");

            this.s.AddWhere("", "", "userId", "=", "@userId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();

            if (isLock)
            {
                this.param.Add("isLocked", 1);
            }
            else
            {
                this.param.Add("isLocked", 0);
            }

            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
