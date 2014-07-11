using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class RoleDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public RoleDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int roleId)
        {
            this.sql = @"select [roleId],[roleName],[itemIndex] from [Sys_Roles] where [roleId]=@roleId";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            this.param = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(msg))
            {
                this.sql = @"select [roleId],[roleName],[itemIndex] from [Sys_Roles] order by [itemIndex] asc";
            }
            else
            {
                this.sql = @"select [roleId],[roleName],[itemIndex] from [Sys_Roles] where [roleName] like '%'+@msg+'%' order by [itemIndex] asc";
                this.param.Add("msg", msg);
            }

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int roleId)
        {
            this.sql = @"delete from [Sys_RoleFunc] where [roleId]=@roleId;delete from [Sys_Roles] where [roleId]=@roleId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Sys_Roles] ([roleName],[itemIndex])values(@roleName,@itemIndex);";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Sys_Roles] set [roleName]=@roleName,[itemIndex]=@itemIndex where [roleId]=@roleId";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleName", content["roleName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("roleId", content["roleId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
