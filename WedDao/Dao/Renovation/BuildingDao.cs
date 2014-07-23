using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class BuildingDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public BuildingDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(long buildingId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "cityId");
            this.s.AddField("b", "regionId");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "regionId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "buildingId", "=", "@buildingId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", buildingId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int cityId, int regionId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "cityId");
            this.s.AddField("b", "regionId");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "regionId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "cityId", "=", "@cityId");

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", cityId);

            if (regionId > 0)
            {
                this.s.AddWhere("and", "b", "regionId", "=", "@regionId");
                this.param.Add("regionId", regionId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "b", "buildingsName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.s.AddOrderBy("b", "itemIndex", false);

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, int regionId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.SetTagField("b", "buildingId");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "cityId");
            this.s.AddField("b", "regionId");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "regionId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "cityId", "=", "@cityId");

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", cityId);

            if (regionId > 0)
            {
                this.s.AddWhere("and", "b", "regionId", "=", "@regionId");
                this.param.Add("regionId", regionId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "b", "buildingsName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.s.AddOrderBy("b", "itemIndex", false);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = this.s.SqlCount();
            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();

            this.sql = this.s.SqlPage(pr.PageSize, pr.StartIndex);

            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }

        public bool Delete(long buildingId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddWhere("", "", "buildingId", "=", "@buildingId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", buildingId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddField("buildingsName");
            this.s.AddField("cityId");
            this.s.AddField("regionId");
            this.s.AddField("itemIndex");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsName", content["buildingsName"]);
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("regionId", content["regionId"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddField("buildingsName");
            this.s.AddField("cityId");
            this.s.AddField("regionId");
            this.s.AddField("itemIndex");

            this.s.AddWhere("", "", "buildingId", "=", "@buildingId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsName", content["buildingsName"]);
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("regionId", content["regionId"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("buildingId", content["buildingId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
