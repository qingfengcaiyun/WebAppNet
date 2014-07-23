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

        public UserDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(string userName, string userPwd)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Sys_User", "u");
            s.AddTable("Sys_Location", "l");

            s.AddField("u", "userId");
            s.AddField("u", "userName");
            s.AddField("u", "userPwd");
            s.AddField("u", "userType");
            s.AddField("u", "lastLogin");
            s.AddField("u", "locationId");
            s.AddField("u", "isDeleted");
            s.AddField("u", "isLocked");

            s.AddField("l", "cnName", "location");

            s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "u", "userName", "=", "@userName");
            s.AddWhere("and", "u", "userPwd", "=", "@userPwd");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("userName", userName);
            this.param.Add("userPwd", userPwd);

            return this.db.GetDataRow(sql, param);
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Sys_User", "u");
            s.AddTable("Sys_Location", "l");

            s.AddField("u", "userId");
            s.AddField("u", "userName");
            s.AddField("u", "userPwd");
            s.AddField("u", "userType");
            s.AddField("u", "lastLogin");
            s.AddField("u", "locationId");
            s.AddField("u", "isDeleted");
            s.AddField("u", "isLocked");

            s.AddField("l", "cnName", "location");

            s.AddWhere("", "u", "locationId", "=", "l", "locationId");
            s.AddWhere("and", "u", "userId", "=", "@userId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool UpdateUserPwd(string userPwd, string md5Pwd, int userId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddField("userPwd");
            s.AddField("md5Pwd");

            s.AddTable("Sys_User");

            s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userPwd", userPwd);
            this.param.Add("md5Pwd", md5Pwd);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetLastlogin(int userId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddField("lastLogin");

            s.AddTable("Sys_User");

            s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("lastLogin", DateTime.Now);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public Boolean UpdateUserPwds()
        {
            SqlBuilder s = new SqlBuilder();

            s.AddField("userId");
            s.AddField("md5Pwd");

            s.AddTable("Sys_User");

            this.sql = s.SqlSelect();

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

                s.AddTable("Sys_User");

                s.AddField("userPwd");

                s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

                this.sql = s.SqlUpdate();

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
