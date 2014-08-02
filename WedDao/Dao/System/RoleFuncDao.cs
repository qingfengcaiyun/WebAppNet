using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class RoleFuncDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public RoleFuncDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int roleId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_RoleFunc");

            this.s.AddField("funcId");

            this.s.AddWhere("", "", "roleId", "=", "@roleId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("funcId");

            this.s.AddWhere("", "", "funcId", "in", "(" + this.sql + ")");

            this.s.AddOrderBy("funcNo", true);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(Int64[] funcIds, Int64 roleId)
        {
            if (funcIds != null && funcIds.Length > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Sys_RoleFunc");

                this.s.AddWhere("", "", "roleId", "=", "@roleId");

                this.sql = this.s.SqlDelete();

                this.param = new Dictionary<string, object>();
                this.param.Add("roleId", roleId);

                this.db.Update(this.sql, this.param);

                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 1, j = funcIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("roleId", roleId);
                    this.param.Add("funcId", funcIds[i]);

                    paramsList.Add(this.param);
                }

                this.s = new SqlBuilder();

                this.s.AddTable("Sys_RoleFunc");

                this.s.AddField("roleId");
                this.s.AddField("funcId");

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
