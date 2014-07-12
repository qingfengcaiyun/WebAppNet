using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Renovation
{
    public class ArticleDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public ArticleDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int raId)
        {
            this.sql = @"select [raId],[processId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime] from [Renovation_Article] where [raId]=@raId";

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> getList(string msg, int processId)
        {
            this.sql = @"select [raId],[processId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime] from [Renovation_Article] where ([longTitle] like '%'+@msg+'%' or [shortTitle] like '%'+@msg+'%' ) order by [endTime] desc";

            this.param = new Dictionary<string, object>();
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords getPage(int pageSize, int pageNo, int cateId, int processId, string msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.param = new Dictionary<string, object>();

            if (cateId > 0)
            {
                pr.CountKey = @"a.[raId]";
                pr.SqlFields = @"a.[raId],a.[processId],a.[longTitle],a.[shortTitle],a.[keywords],a.[readCount],a.[itemIndex],a.[outLink],a.[isTop],a.[topTime],a.[insertTime],a.[updateTime]";
                pr.SqlOrderBy = @"a.[itemIndex] desc,a.[insertTime] desc";
                pr.SqlTable = @"[Renovation_Article] as a, [Renovation_Process] as r";
                pr.SqlWhere = @"a.[raId]=r.[raId] and r.[cateId]=@cateId and a.[processId]=@processId and (a.[longTitle] like '%'+@msg+'%' or a.[shortTitle] like '%'+@msg+'%' or a.[keywords] like '%'+@msg+'%' )";

                this.param.Add("cateId", cateId);
            }
            else
            {
                pr.CountKey = @"[raId]";
                pr.SqlFields = @"[raId],[processId],[longTitle],[shortTitle],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime]";
                pr.SqlOrderBy = @"[itemIndex] desc,[insertTime] desc";
                pr.SqlTable = @"[Renovation_Article]";
                pr.SqlWhere = @"[processId]=@processId and ([longTitle] like '%'+@msg+'%' or [shortTitle] like '%'+@msg+'%' or [keywords] like '%'+@msg+'%' )";
            }

            this.param.Add("processId", processId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int raId)
        {
            this.sql = @"delete from [Renovation_Article] where [raId]=@raId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Renovation_Article] ([processId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime])values(@processId,@longTitle,@shortTitle,@content,@keywords,@picUrl,@readCount,@itemIndex,@outLink,@isTop,@topTime,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", content["processId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
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
            this.sql = @"update [Renovation_Article] set [processId]=@processId,[longTitle]=@longTitle,[shortTitle]=@shortTitle,[content]=@content,[keywords]=@keywords,[picUrl]=@picUrl,[itemIndex]=@itemIndex,[outLink]=@outLink,[isTop]=@isTop,[topTime]=@topTime,[updateTime]=@updateTime where [raId]=@raId";

            this.param = new Dictionary<string, object>();
            this.param.Add("processId", content["processId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("raId", content["raId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadCount(int raId)
        {
            this.sql = @"update [Renovation_Article] set [readCount]=[readCount]+1 where [raId]=@raId";

            this.param = new Dictionary<string, object>();
            this.param.Add("raId", raId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
