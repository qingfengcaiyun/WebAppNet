using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class RoleFuncDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public RoleFuncDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int roleId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_RoleFunc");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("funcId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "roleId", "=", "@roleId");

            this.sql = s.SqlSelect();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Functions");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("funcName");
            s.SqlFields.Add("funcNo");
            s.SqlFields.Add("funcId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "funcId", "in", "(" + this.sql + ")");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("funcNo", true);

            this.sql = s.SqlSelect();

            //this.sql = @"select [funcName],[funcNo],[funcId] from [Sys_Functions] where [funcId] in (select [funcId] from [Sys_RoleFunc] where [roleId]=@roleId) order by [funcNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("roleId", roleId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(int[] funcIds, int roleId)
        {
            if (funcIds != null && funcIds.Length > 0)
            {
                SqlBuilder s = new SqlBuilder();

                s.SqlTable = new SqlTable();
                s.SqlTable.Add("Sys_RoleFunc");

                s.SqlWhere = new SqlWhere();
                s.SqlWhere.Add("", "", "roleId", "=", "@roleId");

                this.sql = s.SqlDelete();

                //this.sql = @"delete from [Sys_RoleFunc] where [roleId]=@roleId;";

                this.param = new Dictionary<string, object>();
                this.param.Add("roleId", roleId);

                this.db.Update(this.sql, this.param);


                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 0, j = funcIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("roleId", roleId);
                    this.param.Add("roleId", funcIds[i]);

                    paramsList.Add(this.param);
                }

                s = new SqlBuilder();

                s.SqlTable = new SqlTable();
                s.SqlTable.Add("Sys_RoleFunc");

                s.SqlFields = new SqlField();
                s.SqlFields.Add("roleId");
                s.SqlFields.Add("funcId");

                this.sql = s.SqlInsert();

                //this.sql = @"insert into [Sys_RoleFunc] ([roleId],[funcId])values(@roleId,@funcId)";
                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
