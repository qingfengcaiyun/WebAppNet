using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class ParameterDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ParameterDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int paramId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Parameter", "p");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");
            this.s.AddField("p", "paramId");
            this.s.AddField("p", "locationId");
            this.s.AddField("p", "paramName");
            this.s.AddField("p", "paramKey");
            this.s.AddField("p", "paramValue");
            this.s.AddField("p", "itemIndex");

            this.s.AddWhere("", "l", "locationId", "=", "p", "locationId");
            this.s.AddWhere("and", "p", "paramId", "=", "@paramId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("paramId", paramId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string paramKey)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Parameter", "p");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");
            this.s.AddField("p", "paramId");
            this.s.AddField("p", "locationId");
            this.s.AddField("p", "paramName");
            this.s.AddField("p", "paramKey");
            this.s.AddField("p", "paramValue");
            this.s.AddField("p", "itemIndex");

            this.s.AddWhere("", "l", "locationId", "=", "p", "locationId");

            this.param = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(paramKey))
            {
                this.s.AddOrderBy("p", "paramKey", true);
            }
            else
            {
                this.s.AddWhere("and", "p", "paramKey", "=", "@paramKey");
                this.param.Add("paramKey", paramKey);
            }

            this.s.AddOrderBy("p", "itemIndex", false);

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetParamTypeList()
        {
            this.param = new Dictionary<string, object>();
            this.sql = @"select distinct [paramName],[paramKey] from [Renovation_Parameter] order by [paramKey] asc";
            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int paramId)
        {
            this.sql = @"delete from [Renovation_Parameter] where [paramId]=@paramId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("paramId", paramId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Parameter");

            this.s.AddField("locationId");
            this.s.AddField("paramName");
            this.s.AddField("paramKey");
            this.s.AddField("paramValue");
            this.s.AddField("itemIndex");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("paramName", content["paramName"]);
            this.param.Add("paramKey", content["paramKey"]);
            this.param.Add("paramValue", content["paramValue"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Parameter");

            this.s.AddField("locationId");
            this.s.AddField("paramName");
            this.s.AddField("paramKey");
            this.s.AddField("paramValue");
            this.s.AddField("itemIndex");

            this.s.AddWhere("", "", "paramId", "=", "@paramId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("paramName", content["paramName"]);
            this.param.Add("paramKey", content["paramKey"]);
            this.param.Add("paramValue", content["paramValue"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("paramId", content["paramId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
