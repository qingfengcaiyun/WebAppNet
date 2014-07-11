using System;
using System.Collections.Generic;
using Glibs.Sql;
using GLibs.Util;

namespace WedDao.Dao.System
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
            this.sql = @"select [userId],[userName],[userPwd],[userType],[lastLogin],[locationId],[isDeleted],[isLocked] from [Sys_User] where [userName]=@userName and [userPwd]=@userPwd";
            this.param = new Dictionary<string, object>();
            this.param.Add("userName", userName);
            this.param.Add("userPwd", userPwd);
            return this.db.GetDataRow(sql, param);
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            this.sql = @"select [userId],[userName],[userPwd],[userType],[lastLogin],[locationId],[isDeleted],[isLocked] from [Sys_User] where [userId]=@userId";
            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool UpdateUserPwd(string userPwd, string md5Pwd, int userId)
        {
            this.sql = @"update [Sys_User] set [userPwd]=@userPwd,[md5Pwd]=@md5Pwd where [userId]=@userId";

            this.param = new Dictionary<string, object>();
            this.param.Add("userPwd", userPwd);
            this.param.Add("md5Pwd", md5Pwd);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetLastlogin(int userId)
        {
            this.sql = @"update [Sys_User] set [lastLogin]=@lastLogin where [userId]=@userId";

            this.param = new Dictionary<string, object>();
            this.param.Add("lastLogin", DateTime.Now);
            this.param.Add("userId", userId);

            return this.db.Update(this.sql, this.param);
        }

        public Boolean UpdateUserPwds()
        {
            this.sql = @"select [userId],[md5Pwd] from [Sys_User]";
            List<Dictionary<string, object>> list = this.db.GetDataTable(this.sql, null);

            this.sql = @"update [Sys_User] set [userPwd]=@userPwd where [userId]=@userId";
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

                return this.db.Import(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
