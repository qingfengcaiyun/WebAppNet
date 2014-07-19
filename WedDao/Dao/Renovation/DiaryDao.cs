using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class DiaryDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public DiaryDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int diaryId)
        {
            this.sql = @"select [diaryId],[locationId],[userId],[clientId],[memberId],[processId],[projectId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[insertTime],[updateTime] from [Renovation_Diary] where [diaryId]=@diaryId";

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            this.sql = @"select [diaryId],[locationId],[userId],[clientId],[memberId],[processId],[projectId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[insertTime],[updateTime] from [Renovation_Diary] where [locationId]=@locationId and ([longTitle] like '%'+@msg+'%' or [shortTitle] like '%'+@msg+'%' ) order by [insertTime] desc";

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.CountKey = @"[diaryId]";
            pr.SqlFields = @"[diaryId],[locationId],[userId],[clientId],[memberId],[processId],[projectId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[insertTime],[updateTime]";
            pr.SqlOrderBy = @"[insertTime] desc";
            pr.SqlTable = @"[Renovation_Diary]";
            pr.SqlWhere = @"[locationId]=@locationId and ([longTitle] like '%'+@msg+'%' or [shortTitle] like '%'+@msg+'%' )";

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }

        public bool Delete(int diaryId)
        {
            this.sql = @"delete from [Renovation_Diary] where [diaryId]=@diaryId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [Renovation_Diary] ([locationId],[userId],[clientId],[memberId],[processId],[projectId],[longTitle],[shortTitle],[content],[keywords],[picUrl],[readCount],[insertTime],[updateTime])values(@locationId,@userId,@clientId,@memberId,@processId,@projectId,@longTitle,@shortTitle,@content,@keywords,@picUrl,@readCount,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("userId", content["userId"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("processId", content["processId"]);
            this.param.Add("projectId", content["projectId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [Renovation_Diary] set [locationId]=@locationId,[userId]=@userId,[clientId]=@clientId,[memberId]=@memberId,[processId]=@processId,[projectId]=@projectId,[longTitle]=@longTitle,[shortTitle]=@shortTitle,[content]=@content,[keywords]=@keywords,[picUrl]=@picUrl,[updateTime]=@updateTime where [diaryId]=@diaryId";

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("userId", content["userId"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("processId", content["processId"]);
            this.param.Add("projectId", content["projectId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("readCount", content["readCount"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("diaryId", content["diaryId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadCount(int diaryId)
        {
            this.sql = @"update [Renovation_Diary] set [readCount]=[readCount]+1 where [diaryId]=@diaryId";

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
