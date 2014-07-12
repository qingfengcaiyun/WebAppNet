using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Renovation
{
    public class BuildingDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public BuildingDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int buildingId)
        {
            this.sql = @"select [buildingId],[buildingsName],[cityId],[regionId],[itemIndex] from [Renovation_Buildings] where [buildingId]=@buildingId";

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", buildingId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int cityId, int regionId)
        {
            this.param = new Dictionary<string, object>();
            if (regionId > 0)
            {
                this.sql = @"select [buildingId],[buildingsName],[cityId],[regionId],[itemIndex] from [Renovation_Buildings] where [regionId]=@regionId and [cityId]=@cityId and [buildingsName] like '%'+@msg+'%' order by [itemIndex] asc";
                this.param.Add("regionId", regionId);
            }
            else
            {
                this.sql = @"select [buildingId],[buildingsName],[cityId],[regionId],[itemIndex] from [Renovation_Buildings] where [cityId]=@cityId and [buildingsName] like '%'+@msg+'%' order by [itemIndex] asc";
            }
            this.param.Add("cityId", cityId);
            this.param.Add("msg", msg);
            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, int regionId, string msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.CountKey = @"[buildingId]";
            pr.SqlFields = @"[buildingId],[buildingsName],[cityId],[regionId],[itemIndex]";
            pr.SqlOrderBy = @"[itemIndex] asc";
            pr.SqlTable = @"[Renovation_Buildings]";

            this.param = new Dictionary<string, object>();
            if (regionId > 0)
            {
                pr.SqlWhere = @"[regionId]=@regionId and [cityId]=@cityId and [buildingsName] like '%'+@msg+'%'";

                this.param.Add("regionId", regionId);
            }
            else
            {
                pr.SqlWhere = @"[cityId]=@cityId and [buildingsName] like '%'+@msg+'%'";
            }

            this.param.Add("cityId", cityId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int buildingId)
        {
            this.sql = @"delete from [Renovation_Buildings] where [buildingId]=@buildingId";

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", buildingId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Renovation_Buildings] ([buildingsName],[cityId],[regionId],[itemIndex])values(@buildingsName,@cityId,@regionId,@itemIndex)";

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingsName", content["buildingsName"]);
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("regionId", content["regionId"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Renovation_Buildings] set [buildingsName]=@buildingsName,[cityId]=@cityId,[regionId]=@regionId,[itemIndex]=@itemIndex where [buildingId]=@buildingId";

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
