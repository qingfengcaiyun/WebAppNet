using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WedDao.Dao.System
{

    public class FunctionDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public FunctionDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetListByUserId(string parentNo, int userId)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_UserRole");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("roleId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = s.SqlSelect();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_RoleFunc");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("funcId");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "roleId", "in", "(" + this.sql + ")");

            this.sql = s.SqlSelect();

            s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Function");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("funcId");
            s.SqlFields.Add("funcName");
            s.SqlFields.Add("funcNo");
            s.SqlFields.Add("parentNo");
            s.SqlFields.Add("funcUrl");
            s.SqlFields.Add("isLeaf");
            s.SqlFields.Add("isDeleted");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "isDeleted", "=", "0");
            s.SqlWhere.Add("and", string.Empty, "parentNo", "like", "@parentNo+'%'");
            s.SqlWhere.Add("and", string.Empty, "funcId", "in", "(" + this.sql + ")");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("funcNo", true);

            this.sql = s.SqlSelect();

            //this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [isDeleted]=0 and [parentNo] like @parentNo+'%' and [funcId] in (select [funcId] from [Sys_RoleFunc] where [roleId] in (select [roleId] from [Sys_UserRole] where [userId]=@userId)) order by [funcNo] asc";

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            this.param.Add("userId", userId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Function");

            s.SqlFields = new SqlField();
            s.SqlFields.Add("funcId");
            s.SqlFields.Add("funcName");
            s.SqlFields.Add("funcNo");
            s.SqlFields.Add("parentNo");
            s.SqlFields.Add("funcUrl");
            s.SqlFields.Add("isLeaf");
            s.SqlFields.Add("isDeleted");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "isDeleted", "=", "0");
            s.SqlWhere.Add("and", string.Empty, "parentNo", "like", "@parentNo+'%'");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("funcNo", true);

            this.sql = s.SqlSelect();

            //this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [parentNo] like @parentNo+'%' and [isDeleted]=0 order by [funcNo] asc";

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            return this.db.GetDataTable(this.sql, this.param);
        }
    }
}
