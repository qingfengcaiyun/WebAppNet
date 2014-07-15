using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Info
{
    public class ActivityDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public ActivityDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int actId)
        {
            this.sql = @"select [actId],[cityId],[actName],[startTime],[endTime],[publicAdpic],[content],[address],[phone],[qq],[keywords],[readCount],[itemIndex] from [Info_Activity] where [actId]=@actId";

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> getList(String msg)
        {
            this.sql = @"select [actId],[actName],[startTime],[endTime],[publicAdpic],[address],[phone],[qq],[keywords],[readCount],[itemIndex] from [Info_Activity] where [actName] like '%'+@msg+'%' order by [itemIndex] desc,[endTime] desc";

            this.param = new Dictionary<string, object>();
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, String msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.CountKey = @"[actId]";
            pr.SqlFields = @"[actId],[actName],[startTime],[endTime],[publicAdpic],[address],[phone],[qq],[keywords],[readCount],[itemIndex]";
            pr.SqlOrderBy = @"[itemIndex] desc,[endTime] desc";
            pr.SqlTable = @"[Info_Activity]";
            pr.SqlWhere = @"[cityId]=@cityId and [actName] like '%'+@msg+'%'";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", cityId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int actId)
        {
            this.sql = @"delete from [Info_Activity] where [actId]=@actId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Info_Activity] ([cityId],[actName],[startTime],[endTime],[publicAdpic],[content],[address],[phone],[qq],[keywords],[readCount],[itemIndex])values(@cityId,@actName,@startTime,@endTime,@publicAdpic,@content,@address,@phone,@qq,@keywords,@readCount,@itemIndex)";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("actName", content["actName"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("endTime", content["endTime"]);
            this.param.Add("publicAdpic", content["publicAdpic"]);
            this.param.Add("content", content["content"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("itemIndex", content["itemIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Info_Activity] set [cityId]=@cityId,[actName]=@actName,[startTime]=@startTime,[endTime]=@endTime,[publicAdpic]=@publicAdpic,[content]=@content,[address]=@address,[phone]=@phone,[qq]=@qq,[keywords]=@keywords,[readcount]=@readcount,[itemIndex]=@itemIndex where [actId]=@actId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("actName", content["actName"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("endTime", content["endTime"]);
            this.param.Add("publicAdpic", content["publicAdpic"]);
            this.param.Add("content", content["content"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("actId", content["actId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadcount(int actId)
        {
            this.sql = "update [Info_Activity] set [readcount]=[readcount]+1 where [actId]=@actId";

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
