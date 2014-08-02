using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class BuildingsDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public BuildingsDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(long buildingsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "locationId");
            this.s.AddField("b", "address");
            this.s.AddField("b", "picUrl");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "buildingsId", "=", "@buildingsId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsId", buildingsId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "levelNo");
            this.s.AddWhere("", "l", "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "locationId");
            this.s.AddWhere("", "l", "levelNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "locationId");
            this.s.AddField("b", "address");
            this.s.AddField("b", "picUrl");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "locationId", "in", "(" + this.sql + ")");

            this.param = new Dictionary<string, object>();

            this.param.Add("locationId", locationId);

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "b", "buildingsName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.s.AddOrderBy("b", "itemIndex", false);

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "levelNo");
            this.s.AddWhere("", "l", "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "locationId");
            this.s.AddWhere("", "l", "levelNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("Sys_Location", "l");

            this.s.SetTagField("b", "buildingsId");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsId");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("b", "locationId");
            this.s.AddField("b", "address");
            this.s.AddField("b", "picUrl");
            this.s.AddField("b", "itemIndex");

            this.s.AddWhere("", "b", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "b", "locationId", "in", "(" + this.sql + ")");

            this.param = new Dictionary<string, object>();

            this.param.Add("locationId", locationId);

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

        public bool Delete(long buildingsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddWhere("", "", "buildingsId", "=", "@buildingsId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsId", buildingsId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddField("buildingsName");
            this.s.AddField("locationId");
            this.s.AddField("address");
            this.s.AddField("picUrl");
            this.s.AddField("itemIndex");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsName", content["buildingsName"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("address", content["address"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Buildings");

            this.s.AddField("buildingsName");
            this.s.AddField("locationId");
            this.s.AddField("address");
            this.s.AddField("picUrl");
            this.s.AddField("itemIndex");

            this.s.AddWhere("", "", "buildingsId", "=", "@buildingsId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsName", content["buildingsName"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("address", content["address"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("buildingsId", content["buildingsId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
