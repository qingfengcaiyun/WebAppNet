using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Renovation
{
    public class ProjectDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public ProjectDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int projectId)
        {
            this.sql = @"select [projectId],[buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime] from [Renovation_Project] where [projectId]=@projectId";

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> getList(string msg, int cityId, int regionId)
        {
            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", cityId);
            this.param.Add("msg", msg);

            if (regionId > 0)
            {
                this.sql = @"select [projectId],[buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime] from [Renovation_Project] where [regionId]=@regionId and [cityId]=@cityId and [pName] like '%'+@msg+'%' order by [itemIndex] asc";
                this.param.Add("regionId", regionId);
            }
            else
            {
                this.sql = @"select [projectId],[buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime] from [Renovation_Project] where [cityId]=@cityId and [pName] like '%'+@msg+'%' order by [itemIndex] asc";
            }

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, int regionId, string msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.param = new Dictionary<string, object>();

            if (regionId > 0)
            {
                pr.CountKey = @"[projectId]";
                pr.SqlFields = @"[projectId],[buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime]";
                pr.SqlOrderBy = @"[itemIndex] asc";
                pr.SqlTable = @"[Renovation_Project]";
                pr.SqlWhere = @"[regionId]=@regionId and [cityId]=@cityId and [buildingsName] like '%'+@msg+'%'";

                this.param.Add("regionId", regionId);
            }
            else
            {
                pr.CountKey = @"[projectId]";
                pr.SqlFields = @"[projectId],[buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime]";
                pr.SqlOrderBy = @"[itemIndex] asc";
                pr.SqlTable = @"[Renovation_Project]";
                pr.SqlWhere = @"[cityId]=@cityId and [buildingsName] like '%'+@msg+'%'";
            }

            this.param.Add("cityId", cityId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int projectId)
        {
            this.sql = @"delete from [Renovation_Project] where [projectId]=@projectId";

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Renovation_Project] ([buildingId],[styleId],[houseId],[memberId],[roomId],[priceId],[pName],[clientId],[typeId],[startTime],[insertTime],[updateTime])values(@buildingId,@styleId,@houseId,@memberId,@roomId,@priceId,@pName,@clientId,@typeId,@startTime,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;
            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", content["buildingId"]);
            this.param.Add("styleId", content["styleId"]);
            this.param.Add("houseId", content["houseId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("roomId", content["roomId"]);
            this.param.Add("priceId", content["priceId"]);
            this.param.Add("pName", content["pName"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("typeId", content["typeId"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Renovation_Project] set [buildingId]=@buildingId,[styleId]=@styleId,[houseId]=@houseId,[memberId]=@memberId,[roomId]=@roomId,[priceId]=@priceId,[pName]=@pName,[clientId]=@clientId,[typeId]=@typeId,[startTime]=@startTime,[updateTime]=@updateTime where [projectId]=@projectId";

            this.param = new Dictionary<string, object>();
            this.param.Add("buildingId", content["buildingId"]);
            this.param.Add("styleId", content["styleId"]);
            this.param.Add("houseId", content["houseId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("roomId", content["roomId"]);
            this.param.Add("priceId", content["priceId"]);
            this.param.Add("pName", content["pName"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("typeId", content["typeId"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("projectId", content["projectId"]);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Update(this.sql, this.param);
        }
    }
}
