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
            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [isDeleted]=0 and [parentNo] like @parentNo+'%' and [funcId] in (select [funcId] from [Sys_RoleFunc] where [roleId] in (select [roleId] from [Sys_UserRole] where [userId]=@userId)) order by [funcNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            this.param.Add("userId", userId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> getList(string parentNo)
        {
            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [parentNo] like @parentNo+'%' and [isDeleted]=0 order by [funcNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            return this.db.GetDataTable(this.sql, this.param);
        }
    }
}
