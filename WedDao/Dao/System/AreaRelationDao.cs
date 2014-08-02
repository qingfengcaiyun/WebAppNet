using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class AreaRelationDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public AreaRelationDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int areaId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_AreaRelation");

            this.s.AddField("locationId");

            this.s.AddWhere("", "", "areaId", "=", "@areaId");

            this.sql = this.s.SqlSelect();

            s = new SqlBuilder();

            this.s.AddTable("Sys_Location");

            this.s.AddField("locationId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");
            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "locationId", "in", "(" + this.sql + ")");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(int[] locationIds, int areaId)
        {
            if (locationIds != null && locationIds.Length > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Sys_AreaRelation");

                this.s.AddWhere("", "", "areaId", "=", "@areaId");

                this.sql = this.s.SqlDelete();

                this.param = new Dictionary<string, object>();
                this.param.Add("areaId", areaId);

                this.db.Update(this.sql, this.param);

                this.s = new SqlBuilder();

                this.s.AddTable("Sys_AreaRelation");

                this.s.AddField("areaId");
                this.s.AddField("locationId");

                this.sql = this.s.SqlDelete();

                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 0, j = locationIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("areaId", areaId);
                    this.param.Add("locationId", locationIds[i]);

                    paramsList.Add(this.param);
                }

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
