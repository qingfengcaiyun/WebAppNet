﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class UserFuncDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public UserFuncDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int funcId)
        {
            this.sql = @"select [userId],[userName],[userType] from [Sys_Users] where [isDelete]=0 and [userId] in (select [userId] from [Sys_UserFunc] where [funcId]=@funcId) order by [userName] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("funcId", funcId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(int[] userIds, int roleId)
        {
            if (userIds != null && userIds.Length > 0)
            {
                this.sql = @"delete from [Sys_UserRole] where [roleId]=@roleId;";

                this.param = new Dictionary<string, object>();
                this.param.Add("roleId", roleId);

                this.db.Update(this.sql, this.param);

                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 0, j = userIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("roleId", roleId);
                    this.param.Add("userId", userIds[i]);

                    paramsList.Add(this.param);
                }

                this.sql = @"insert into [Sys_UserRole] ([roleId],[userId])values(@roleId,@userId)";

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
