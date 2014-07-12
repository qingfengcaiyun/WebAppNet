using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WedDao.Dao.System
{
    public class LocationDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public LocationDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int locationId)
        {
            this.sql = @"select [locationId],[cnName],[enName],[levelNo],[parentNo],[levelCnName],[levelEnName],[isLeaf] from [Sys_Location] where [locationId]=@locationId";

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public Dictionary<string, object> GetOne(string levelNo)
        {
            this.sql = @"select [locationId],[cnName],[enName],[levelNo],[parentNo],[levelCnName],[levelEnName],[isLeaf] from [Sys_Location] where [levelNo]=@levelNo";

            this.param = new Dictionary<string, object>();
            this.param.Add("levelNo", levelNo);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.sql = @"select [locationId],[cnName],[enName],[levelNo],[parentNo],[levelCnName],[levelEnName],[isLeaf] from [Sys_Location] where [parentNo] like @parentNo+'%' order by [levelNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }
    }
}
