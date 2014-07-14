using System.Collections.Generic;
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
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_UserFunc");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("userId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "funcId", "=", "@funcId");

            this.sql = s.SqlSelect();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_User");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add("", "", "isDeleted", "=", "0");
            s.SqlWhere.Add("", "", "userId", "in", "(" + this.sql + ")");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("userId");
            s.SqlFields.Add("userName");
            s.SqlFields.Add("userType");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("userName", true);

            this.sql = s.SqlSelect();

            //this.sql = @"select [userId],[userName],[userType] from [Sys_User] where [isDelete]=0 and [userId] in (select [userId] from [Sys_UserFunc] where [funcId]=@funcId) order by [userName] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("funcId", funcId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(int[] userIds, int roleId)
        {
            if (userIds != null && userIds.Length > 0)
            {
                SqlBuilder s = new SqlBuilder();

                s.SqlTable = new SqlTable();
                s.SqlTable.Add("Sys_UserRole");

                s.SqlWhere = new SqlWhere();
                s.SqlWhere.Add("", "", "roleId", "=", "@roleId");

                this.sql = s.SqlDelete();

                //this.sql = @"delete from [Sys_UserRole] where [roleId]=@roleId;";

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

                s = new SqlBuilder();

                s.SqlTable = new SqlTable();
                s.SqlTable.Add("Sys_UserRole");

                s.SqlFields = new SqlField();
                s.SqlFields.Add("roleId");
                s.SqlFields.Add("userId");

                this.sql = s.SqlInsert();

                //this.sql = @"insert into [Sys_UserRole] ([roleId],[userId])values(@roleId,@userId)";

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
