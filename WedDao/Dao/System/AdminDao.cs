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
            this.sql = @"select [adminId],[userId],[locationId],[fullName],[phone],[email],[qq],[insertTime],[updateTime] from [Sys_Admin] where [userId]=@userId";

            this.param = new Dictionary<string, object>();
            this.param.Add("@userId", userId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Sys_Admin] set [locationId]=@locationId,[fullName]=@fullName,[phone]=@phone,[email]=@email,[qq]=@qq,[updateTime]=@updateTime where [adminId]=@adminId";

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
