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
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("cnName");
            s.SqlFields.Add("enName");
            s.SqlFields.Add("levelNo");
            s.SqlFields.Add("parentNo");
            s.SqlFields.Add("levelCnName");
            s.SqlFields.Add("levelEnName");
            s.SqlFields.Add("isLeaf");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Location");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "locationId", "=", "@locationId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public Dictionary<string, object> GetOne(string levelNo)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("cnName");
            s.SqlFields.Add("enName");
            s.SqlFields.Add("levelNo");
            s.SqlFields.Add("parentNo");
            s.SqlFields.Add("levelCnName");
            s.SqlFields.Add("levelEnName");
            s.SqlFields.Add("isLeaf");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Location");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "levelNo", "=", "@levelNo");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("levelNo", levelNo);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new SqlField();
            s.SqlFields.Add("locationId");
            s.SqlFields.Add("cnName");
            s.SqlFields.Add("enName");
            s.SqlFields.Add("levelNo");
            s.SqlFields.Add("parentNo");
            s.SqlFields.Add("levelCnName");
            s.SqlFields.Add("levelEnName");
            s.SqlFields.Add("isLeaf");

            s.SqlTable = new SqlTable();
            s.SqlTable.Add("Sys_Location");

            s.SqlWhere = new SqlWhere();
            s.SqlWhere.Add(string.Empty, string.Empty, "parentNo", "like", "@parentNo+'%'");

            s.SqlOrderBy = new SqlOrderBy();
            s.SqlOrderBy.Add("levelNo", true);

            this.sql = s.SqlSelect();

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
