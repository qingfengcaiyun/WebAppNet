using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Info
{
    public class NewsDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public NewsDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int newsId)
        {
            this.sql = @"select [newsId],[cityId],[longTitle],[titleColor],[shortTitle],[content],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime] from [Info_Article] where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.param = new Dictionary<string, object>();

            if (cateId > 0)
            {
                pr.CountKey = @"a.[newsId]";
                pr.SqlFields = @"[newsId],[cityId],[longTitle],[titleColor],[shortTitle],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime]";
                pr.SqlOrderBy = @"[itemIndex] desc,[insertTime] desc";
                pr.SqlTable = @"[Info_News]";
                pr.SqlWhere = @"a.[newsId]=r.[newsId] and r.[cateId]=@cateId and a.[cityId] in (" + cityId + @") and (a.[longTitle] like '%'+@msg+'%' or a.[shortTitle] like '%'+@msg+'%' or a.[keywords] like '%'+@msg+'%' )";

                this.param.Add("cateId", cateId);
                this.param.Add("msg", msg);
            }
            else
            {
                pr.CountKey = @"[newsId]";
                pr.SqlFields = @"[newsId],[cityId],[longTitle],[titleColor],[shortTitle],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime]";
                pr.SqlOrderBy = @"[itemIndex] desc,[insertTime] desc";
                pr.SqlTable = @"[Info_News]";
                pr.SqlWhere = @"[cityId] in (" + cityId + @") and ([longTitle] like '%'+@msg+'%' or [shortTitle] like '%'+@msg+'%' or [keywords] like '%'+@msg+'%')";

                this.param.Add("msg", msg);
            }

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int actId)
        {
            this.sql = @"delete from [Info_Relationship] where [newsId]=@;delete from [Info_Article] where [newsId]=@;";

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Info_Article] ([cityId],[longTitle],[titleColor],[shortTitle],[content],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime])values(@cityId,@longTitle,@titleColor,@shortTitle,@content,@keywords,@readCount,@itemIndex,@outLink,@isTop,@topTime,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Info_Article] set [cityId]=@cityId,[longTitle]=@longTitle,[titleColor]=@titleColor,[shortTitle]=@shortTitle,[content]=@content,[keywords]=@keywords,[itemIndex]=@itemIndex,[outLink]=@outLink,[isTop]=@isTop,[topTime]=@topTime,[updateTime]=@updateTime where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetreadCount(int newsId)
        {
            this.sql = @"update [Info_Article] set [readCount]=[readCount]+1 where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
