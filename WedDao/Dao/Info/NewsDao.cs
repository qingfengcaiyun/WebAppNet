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

        public Dictionary<string, object> GetOne(Int64 newsId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Info_News");

            s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            s.AddField("newsId");
            s.AddField("cityId");
            s.AddField("longTitle");
            s.AddField("titleColor");
            s.AddField("shortTitle");
            s.AddField("content");
            s.AddField("fileIds");
            s.AddField("keywords");
            s.AddField("picUrl");
            s.AddField("readCount");
            s.AddField("itemIndex");
            s.AddField("outLink");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("insertTime");
            s.AddField("updateTime");

            this.sql = s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            SqlBuilder s = new SqlBuilder();

            this.param = new Dictionary<string, object>();

            if (cateId > 0)
            {
                s = new SqlBuilder();

                s.AddTable("Info_Relationship");

                s.AddField("newsId");

                s.AddWhere(string.Empty, string.Empty, "cateId", "=", "@cateId");

                this.sql = s.SqlSelect();

                s = new SqlBuilder();

                s.AddTable("Info_News", "n");

                s.AddField("n", "newsId");
                s.AddField("n", "cityId");
                s.AddField("n", "longTitle");
                s.AddField("n", "titleColor");
                s.AddField("n", "shortTitle");
                s.AddField("n", "keywords");
                s.AddField("n", "readCount");
                s.AddField("n", "itemIndex");
                s.AddField("n", "insertTime");
                s.AddField("n", "updateTime");

                s.SetTagField("n", "newsId");

                s.AddOrderBy("n", "itemIndex", false);
                s.AddOrderBy("n", "insertTime", false);

                s.AddWhere(string.Empty, "n", "newsId", "in", "(" + this.sql + ")");
                s.AddWhere("and", "n", "cityId", "in", "(" + cityId + ")");
                s.AddWhere("and", "(n", "longTitle", "like", "'%'+@msg+'%'");
                s.AddWhere("or", "n", "shortTitle", "like", "'%'+@msg+'%'");
                s.AddWhere("or", "n", "keywords", "like", "'%'+@msg+'%')");

                this.param.Add("cateId", cateId);
            }
            else
            {
                s = new SqlBuilder();

                s.AddTable("Info_News", "n");

                s.AddField("n", "newsId");
                s.AddField("n", "cityId");
                s.AddField("n", "longTitle");
                s.AddField("n", "titleColor");
                s.AddField("n", "shortTitle");
                s.AddField("n", "keywords");
                s.AddField("n", "readCount");
                s.AddField("n", "itemIndex");
                s.AddField("n", "insertTime");
                s.AddField("n", "updateTime");

                s.SetTagField("n", "newsId");

                s.AddOrderBy("n", "itemIndex", false);
                s.AddOrderBy("n", "insertTime", false);

                s.AddWhere(string.Empty, "n", "cityId", "in", "(" + cityId + ")");
                s.AddWhere("and", "(n", "longTitle", "like", "'%'+@msg+'%'");
                s.AddWhere("or", "n", "shortTitle", "like", "'%'+@msg+'%'");
                s.AddWhere("or", "n", "keywords", "like", "'%'+@msg+'%')");
            }

            this.param.Add("msg", msg);

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

        public bool Delete(int newsId)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Info_Relationship");

            s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.sql = s.SqlDelete();

            s = new SqlBuilder();

            s.AddTable("Info_Article");

            s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.sql = this.sql + s.SqlDelete();

            //this.sql = @"delete from [Info_Relationship] where [newsId]=@newsId;delete from [Info_Article] where [newsId]=@newsId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Info_News");

            s.AddField("cityId");
            s.AddField("longTitle");
            s.AddField("titleColor");
            s.AddField("shortTitle");
            s.AddField("content");
            s.AddField("fileIds");
            s.AddField("keywords");
            s.AddField("picUrl");
            s.AddField("readCount");
            s.AddField("itemIndex");
            s.AddField("outLink");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("insertTime");
            s.AddField("updateTime");

            this.sql = s.SqlInsert();

            //this.sql = @"insert into [Info_Article] ([cityId],[longTitle],[titleColor],[shortTitle],[content],[keywords],[readCount],[itemIndex],[outLink],[isTop],[topTime],[insertTime],[updateTime])values(@cityId,@longTitle,@titleColor,@shortTitle,@content,@keywords,@readCount,@itemIndex,@outLink,@isTop,@topTime,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"].ToString().Replace('\"', '\''));
            this.param.Add("fileIds", content["fileIds"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("readCount", 0);
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
            SqlBuilder s = new SqlBuilder();

            s.AddTable("Info_News");

            s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            s.AddField("cityId");
            s.AddField("longTitle");
            s.AddField("titleColor");
            s.AddField("shortTitle");
            s.AddField("content");
            s.AddField("fileIds");
            s.AddField("keywords");
            s.AddField("picUrl");
            s.AddField("itemIndex");
            s.AddField("outLink");
            s.AddField("isTop");
            s.AddField("topTime");
            s.AddField("updateTime");

            this.sql = s.SqlUpdate();

            //this.sql = @"update [Info_Article] set [cityId]=@cityId,[longTitle]=@longTitle,[titleColor]=@titleColor,[shortTitle]=@shortTitle,[content]=@content,[keywords]=@keywords,[itemIndex]=@itemIndex,[outLink]=@outLink,[isTop]=@isTop,[topTime]=@topTime,[updateTime]=@updateTime where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"]);
            this.param.Add("fileIds", content["fileIds"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadCount(int newsId)
        {
            this.sql = @"update [Info_News] set [readCount]=[readCount]+1 where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
