using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class CustomAreaDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public CustomAreaDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int areaId)
        {
            this.sql = @"select [areaId],[cnName],[enName],[itemIndex] from [Sys_CustomArea] where [areaId]=@areaId";

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList()
        {
            this.sql = @"select [areaId],[cnName],[enName],[itemIndex] from [Sys_CustomArea] order by [itemIndex] asc";

            this.param = new Dictionary<string, object>();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public Boolean Delete(int areaId)
        {
            this.sql = @"delete from [Sys_AreaRelation] where [areaId]=@areaId;delete from [Sys_CustomArea] where [areaId]=@areaId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.Update(this.sql, this.param);
        }

        public long insert(Dictionary<string, object> content)
        {
            this.sql = "insert into [Sys_CustomArea] ([cnName],[enName],[itemIndex])values(@cnName,@enName,@itemIndex);";

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Sys_CustomArea] set [cnName]=@cnName,[enName]=@enName,[itemIndex]=@itemIndex where [areaId]=@areaId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("areaId", content["areaId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
