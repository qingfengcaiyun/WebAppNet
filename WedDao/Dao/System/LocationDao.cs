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

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("locationId"));
            s.SqlFields.Add(new SqlField("cnName"));
            s.SqlFields.Add(new SqlField("enName"));
            s.SqlFields.Add(new SqlField("levelNo"));
            s.SqlFields.Add(new SqlField("parentNo"));
            s.SqlFields.Add(new SqlField("levelCnName"));
            s.SqlFields.Add(new SqlField("levelEnName"));
            s.SqlFields.Add(new SqlField("isLeaf"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Location"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "locationId", "=", "@locationId"));

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public Dictionary<string, object> GetOne(string levelNo)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("locationId"));
            s.SqlFields.Add(new SqlField("cnName"));
            s.SqlFields.Add(new SqlField("enName"));
            s.SqlFields.Add(new SqlField("levelNo"));
            s.SqlFields.Add(new SqlField("parentNo"));
            s.SqlFields.Add(new SqlField("levelCnName"));
            s.SqlFields.Add(new SqlField("levelEnName"));
            s.SqlFields.Add(new SqlField("isLeaf"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Location"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "levelNo", "=", "@levelNo"));

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("levelNo", levelNo);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            SqlBuilder s = new SqlBuilder();

            s.SqlFields = new List<SqlField>();
            s.SqlFields.Add(new SqlField("locationId"));
            s.SqlFields.Add(new SqlField("cnName"));
            s.SqlFields.Add(new SqlField("enName"));
            s.SqlFields.Add(new SqlField("levelNo"));
            s.SqlFields.Add(new SqlField("parentNo"));
            s.SqlFields.Add(new SqlField("levelCnName"));
            s.SqlFields.Add(new SqlField("levelEnName"));
            s.SqlFields.Add(new SqlField("isLeaf"));

            s.SqlTable = new List<SqlTable>();
            s.SqlTable.Add(new SqlTable("Sys_Location"));

            s.SqlWhere = new List<SqlWhere>();
            s.SqlWhere.Add(new SqlWhere(string.Empty, string.Empty, "parentNo", "like", "@parentNo+'%'"));

            s.SqlOrderBy = new List<SqlOrderBy>();
            s.SqlOrderBy.Add(new SqlOrderBy("levelNo", true));

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
