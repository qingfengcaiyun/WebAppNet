using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Renovation
{
    public class ParameterDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public ParameterDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> getOne(int paramId)
        {
            this.sql = @"select [paramId],[locationId],[paramName],[paramKey],[paramValue],[itemIndex] from [Renovation_Parameters] where [paramId]=@paramId";

            this.param = new Dictionary<string, object>();
            this.param.Add("paramId", paramId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string paramKey)
        {
            this.param = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(paramKey))
            {
                this.sql = @"select [paramId],[locationId],[paramName],[paramKey],[paramValue],[itemIndex] from [Renovation_Parameters] order by [paramKey] asc,[itemIndex] desc";
            }
            else
            {
                this.sql = @"select [paramId],[locationId],[paramName],[paramKey],[paramValue],[itemIndex] from [Renovation_Parameters] where [paramKey]=@paramKey order by [itemIndex] desc";
                this.param.Add("paramKey", paramKey);
            }

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetParamTypeList()
        {
            this.param = new Dictionary<string, object>();
            this.sql = @"select distinct [paramName],[paramKey] from [Renovation_Parameters] order by [paramKey] asc";
            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int paramId)
        {
            this.sql = @"delete from [Renovation_Parameters] where [paramId]=@paramId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("paramId", paramId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Renovation_Parameters] ([locationId],[paramName],[paramKey],[paramValue],[itemIndex])values(@locationId,@paramName,@paramKey,@paramValue,@itemIndex)";

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("locationId", content["paramName"]);
            this.param.Add("locationId", content["paramKey"]);
            this.param.Add("locationId", content["paramValue"]);
            this.param.Add("locationId", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Renovation_Parameters] set [locationId]=@locationId,[paramName]=@paramName,[paramKey]=@paramKey,[paramValue]=@paramValue,[itemIndex]=@itemIndex where [paramId]=@paramId";

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
