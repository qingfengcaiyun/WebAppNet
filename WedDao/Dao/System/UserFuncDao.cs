﻿using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class UserFuncDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public UserFuncDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int funcId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_UserFunc");

            this.s.AddField("userId");

            this.s.AddWhere("", "", "funcId", "=", "@funcId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_User");

            this.s.AddWhere("", "", "isDeleted", "=", "0");
            this.s.AddWhere("", "", "userId", "in", "(" + this.sql + ")");

            this.s.AddField("userId");
            this.s.AddField("userName");
            this.s.AddField("userType");

            this.s.AddOrderBy("userName", true);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("funcId", funcId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(Int64[] userIds, Int64 roleId)
        {
            if (userIds != null && userIds.Length > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Sys_UserRole");

                this.s.AddWhere("", "", "roleId", "=", "@roleId");

                this.sql = this.s.SqlDelete();

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

                this.s = new SqlBuilder();

                this.s.AddTable("Sys_UserRole");

                this.s.AddField("roleId");
                this.s.AddField("userId");

                this.sql = this.s.SqlInsert();

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
