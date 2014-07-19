using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class WorksDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public WorksDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(long workId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Renovation_Works");

            s.AddField("workId");
            s.AddField("memberId");
            s.AddField("longTitle");
            s.AddField("shortTitle");
            s.AddField("memo");
            s.AddField("keywords");
            s.AddField("readCount");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("itemIndex");
            s.AddField("insertTime");
            s.AddField("updateTime");

            s.AddWhere(string.Empty, string.Empty, "workId", "=", "@workId");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("workId", workId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, long memberId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Renovation_Works");

            s.SetTagField("workId");

            s.AddField("workId");
            s.AddField("memberId");
            s.AddField("longTitle");
            s.AddField("shortTitle");
            s.AddField("memo");
            s.AddField("keywords");
            s.AddField("readCount");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("itemIndex");
            s.AddField("insertTime");
            s.AddField("updateTime");

            s.AddWhere(string.Empty, string.Empty, "memberId", "=", "@memberId");

            s.AddOrderBy("isTop", false);
            s.AddOrderBy("itemIndex", false);
            s.AddOrderBy("insertTime", false);

            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", memberId);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = s.SqlCount();
            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();
            this.sql = s.SqlPage(pr.PageSize, pr.StartIndex);
            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }

        public bool Delete(int workId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Renovation_WorkPic");

            s.AddWhere(string.Empty, string.Empty, "workId", "=", "@workId");

            this.sql = s.SqlDelete();

            s = new SqlBuilder();

            s.AddTable("Renovation_Works");

            s.AddWhere(string.Empty, string.Empty, "workId", "=", "@workId");

            this.sql = this.sql + ";" + s.SqlDelete() + ";";

            //this.sql = @"delete from [Info_Relationship] where [newsId]=@newsId;delete from [Info_Article] where [newsId]=@newsId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("workId", workId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Renovation_Works");

            s.AddField("memberId");
            s.AddField("longTitle");
            s.AddField("shortTitle");
            s.AddField("memo");
            s.AddField("keywords");
            s.AddField("readCount");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("itemIndex");
            s.AddField("insertTime");
            s.AddField("updateTime");

            this.sql = s.SqlInsert();

            //this.sql = @"insert into [Info_Article] ([cityId],[longTitle],[titleColor],[shortTitle],[content],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime])values(@cityId,@longTitle,@titleColor,@shortTitle,@content,@keywords,@readCount,@itemIndex,@outLink,@isTop,@topTime,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("memo", content["memo"].ToString().Replace('\"', '\''));
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("readCount", 0);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddWhere(string.Empty, string.Empty, "workId", "=", "@workId");

            s.AddTable("Renovation_Works");

            s.AddField("longTitle");
            s.AddField("shortTitle");
            s.AddField("memo");
            s.AddField("keywords");
            s.AddField("readCount");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("itemIndex");
            s.AddField("insertTime");
            s.AddField("updateTime");

            this.sql = s.SqlUpdate();

            //this.sql = @"update [Info_Article] set [cityId]=@cityId,[longTitle]=@longTitle,[titleColor]=@titleColor,[shortTitle]=@shortTitle,[content]=@content,[keywords]=@keywords,[itemIndex]=@itemIndex,[outLink]=@outLink,[isTop]=@isTop,[topTime]=@topTime,[updateTime]=@updateTime where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("memo", content["memo"].ToString().Replace('\"', '\''));
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("readCount", 0);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadCount(int workId)
        {
            this.sql = @"update [Renovation_Works] set [readCount]=[readCount]+1 where [workId]=@workId";

            this.param = new Dictionary<string, object>();
            this.param.Add("workId", workId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
