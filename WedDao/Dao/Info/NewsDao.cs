using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Info
{
    public class NewsDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public NewsDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(Int64 newsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_News", "n");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Info_Category", "c");

            this.s.AddField("c", "cateName");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("n", "newsId");
            this.s.AddField("n", "cityId");
            this.s.AddField("n", "cateId");
            this.s.AddField("n", "longTitle");
            this.s.AddField("n", "titleColor");
            this.s.AddField("n", "shortTitle");
            this.s.AddField("n", "content");
            this.s.AddField("n", "fileIds");
            this.s.AddField("n", "keywords");
            this.s.AddField("n", "picUrl");
            this.s.AddField("n", "readCount");
            this.s.AddField("n", "itemIndex");
            this.s.AddField("n", "outLink");
            this.s.AddField("n", "isTop");
            this.s.AddField("n", "topTime");
            this.s.AddField("n", "insertTime");
            this.s.AddField("n", "updateTime");

            this.s.AddWhere("", "l", "locationId", "=", "n", "cityId");
            this.s.AddWhere("and", "c", "cateId", "=", "n", "cateId");
            this.s.AddWhere("and", "n", "newsId", "=", "@newsId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            this.param = new Dictionary<string, object>();

            if (cateId > 1)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Info_News", "n");
                this.s.AddTable("Sys_Location", "l");
                this.s.AddTable("Info_Category", "c");

                this.s.AddField("c", "cateName");

                this.s.AddField("l", "cnName", "location");

                this.s.AddField("n", "newsId");
                this.s.AddField("n", "cityId");
                this.s.AddField("n", "cateId");
                this.s.AddField("n", "longTitle");
                this.s.AddField("n", "titleColor");
                this.s.AddField("n", "shortTitle");
                this.s.AddField("n", "keywords");
                this.s.AddField("n", "readCount");
                this.s.AddField("n", "itemIndex");
                this.s.AddField("n", "isTop");
                this.s.AddField("n", "topTime");
                this.s.AddField("n", "insertTime");
                this.s.AddField("n", "updateTime");
                this.s.AddField("n", "isChecked");

                this.s.SetTagField("n", "newsId");

                this.s.AddOrderBy("n", "itemIndex", false);
                this.s.AddOrderBy("n", "insertTime", false);

                this.s.AddWhere("", "l", "locationId", "=", "n", "cityId");
                this.s.AddWhere("and", "c", "cateId", "=", "n", "cateId");
                this.s.AddWhere("and", "n", "cateId", "=", "@cateId");
                this.s.AddWhere("and", "n", "cityId", "in", "(" + cityId + ")");
                this.s.AddWhere("and", "(n", "longTitle", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "n", "shortTitle", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "n", "keywords", "like", "'%'+@msg+'%')");

                this.param.Add("cateId", cateId);
            }
            else
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Info_News", "n");
                this.s.AddTable("Sys_Location", "l");
                this.s.AddTable("Info_Category", "c");

                this.s.AddField("c", "cateName");

                this.s.AddField("l", "cnName", "location");

                this.s.AddField("n", "newsId");
                this.s.AddField("n", "cityId");
                this.s.AddField("n", "longTitle");
                this.s.AddField("n", "titleColor");
                this.s.AddField("n", "shortTitle");
                this.s.AddField("n", "keywords");
                this.s.AddField("n", "readCount");
                this.s.AddField("n", "itemIndex");
                this.s.AddField("n", "isTop");
                this.s.AddField("n", "topTime");
                this.s.AddField("n", "insertTime");
                this.s.AddField("n", "updateTime");
                this.s.AddField("n", "isChecked");

                s.SetTagField("n", "newsId");

                this.s.AddOrderBy("n", "itemIndex", false);
                this.s.AddOrderBy("n", "insertTime", false);

                this.s.AddWhere("", "l", "locationId", "=", "n", "cityId");
                this.s.AddWhere("and", "c", "cateId", "=", "n", "cateId");
                this.s.AddWhere("and", "n", "cityId", "in", "(" + cityId + ")");
                this.s.AddWhere("and", "(n", "longTitle", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "n", "shortTitle", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "n", "keywords", "like", "'%'+@msg+'%')");
            }

            this.param.Add("msg", msg);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.sql = this.s.SqlCount();
            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.sql, this.param).ToString());
            pr.SetBaseParam();

            this.sql = this.s.SqlPage(pr.PageSize, pr.StartIndex);

            pr.PageResult = this.db.GetDataTable(this.sql, this.param);

            return pr;
        }

        public bool Delete(int newsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Relationship");

            this.s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.sql = this.s.SqlDelete();

            this.s = new SqlBuilder();

            this.s.AddTable("Info_News");

            this.s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.sql = this.sql + ";" + this.s.SqlDelete() + ";";

            //this.sql = @"delete from [Info_Relationship] where [newsId]=@newsId;delete from [Info_Article] where [newsId]=@newsId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_News");

            this.s.AddField("cityId");
            this.s.AddField("cateId");
            this.s.AddField("longTitle");
            this.s.AddField("titleColor");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("fileIds");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("readCount");
            this.s.AddField("itemIndex");
            this.s.AddField("outLink");
            this.s.AddField("isTop");
            this.s.AddField("topTime");
            this.s.AddField("isChecked");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateId", content["cateId"]);
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
            this.param.Add("isChecked", 0);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_News");

            this.s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.s.AddField("cityId");
            this.s.AddField("cateId");
            this.s.AddField("longTitle");
            this.s.AddField("titleColor");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("fileIds");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("itemIndex");
            this.s.AddField("outLink");
            this.s.AddField("isTop");
            this.s.AddField("topTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlUpdate();
            
            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateId", content["cateId"]);
            this.param.Add("longTitle", content["longTitle"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("shortTitle", content["shortTitle"]);
            this.param.Add("content", content["content"].ToString().Replace('\"', '\''));
            this.param.Add("fileIds", content["fileIds"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("picUrl", content["picUrl"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("outLink", content["outLink"]);
            this.param.Add("isTop", content["isTop"]);
            this.param.Add("topTime", content["topTime"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("newsId", content["newsId"]);

            return this.db.Update(this.sql, this.param);
        }

        public int SetReadCount(int newsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_News");

            this.s.AddField("readCount");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "newsId", "=", "@newsId");

            this.sql = this.s.SqlIncrease() + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return (int)this.db.GetDataValue(this.sql, this.param);
        }

        public bool SetCheck(string newsIds)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_News");

            this.s.AddField("isChecked");

            this.s.AddWhere(string.Empty, string.Empty, "newsId", "in", "(" + newsIds + ")");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isChecked", 1);

            return this.db.Update(this.sql, this.param);
        }
    }
}
