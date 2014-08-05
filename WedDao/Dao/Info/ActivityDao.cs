using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Info
{
    public class ActivityDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ActivityDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int actId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity", "a");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "actId");
            this.s.AddField("a", "cityId");
            this.s.AddField("a", "actName");
            this.s.AddField("a", "titleColor");
            this.s.AddField("a", "startTime");
            this.s.AddField("a", "endTime");
            this.s.AddField("a", "publicAdpic");
            this.s.AddField("a", "content");
            this.s.AddField("a", "address");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "readCount");
            this.s.AddField("a", "isChecked");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "isClosed");
            this.s.AddField("a", "onIndex");

            this.s.AddWhere(string.Empty, "l", "locationId", "=", "a", "cityId");
            this.s.AddWhere("and", "a", "actId", "=", "@actId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetListOnIndex(int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "levelNo");
            this.s.AddWhere("", "l", "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "locationId");
            this.s.AddWhere("", "l", "levelNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity", "a");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "actId");
            this.s.AddField("a", "cityId");
            this.s.AddField("a", "actName");
            this.s.AddField("a", "titleColor");
            this.s.AddField("a", "startTime");
            this.s.AddField("a", "endTime");
            this.s.AddField("a", "publicAdpic");
            this.s.AddField("a", "content");
            this.s.AddField("a", "address");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "readCount");
            this.s.AddField("a", "isChecked");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "isClosed");
            this.s.AddField("a", "onIndex");

            this.s.AddWhere(string.Empty, "l", "locationId", "=", "a", "cityId");
            this.s.AddWhere("and", "a", "onIndex", "=", "1");
            this.s.AddWhere("and", "a", "cityId", "in", "(" + this.sql + ")");

            this.s.AddOrderBy("a", "itemIndex", false);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "levelNo");
            this.s.AddWhere("", "l", "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "locationId");
            this.s.AddWhere("", "l", "levelNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity", "a");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "actId");
            this.s.AddField("a", "cityId");
            this.s.AddField("a", "actName");
            this.s.AddField("a", "titleColor");
            this.s.AddField("a", "startTime");
            this.s.AddField("a", "endTime");
            this.s.AddField("a", "publicAdpic");
            this.s.AddField("a", "content");
            this.s.AddField("a", "address");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "readCount");
            this.s.AddField("a", "isChecked");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "isClosed");
            this.s.AddField("a", "onIndex");

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            this.s.AddWhere(string.Empty, "l", "locationId", "=", "a", "cityId");
            this.s.AddWhere("and", "a", "cityId", "in", "(" + this.sql + ")");

            if (!string.IsNullOrEmpty(msg.Trim()))
            {
                this.s.AddWhere("and", "a", "actName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.s.AddOrderBy("a", "itemIndex", false);
            this.s.AddOrderBy("a", "endTime", false);

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "levelNo");
            this.s.AddWhere("", "l", "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location", "l");
            this.s.AddField("l", "locationId");
            this.s.AddWhere("", "l", "levelNo", "like", "(" + this.sql + ")+'%'");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity", "a");
            this.s.AddTable("Sys_Location", "l");

            this.s.SetTagField("a", "actId");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("a", "actId");
            this.s.AddField("a", "cityId");
            this.s.AddField("a", "actName");
            this.s.AddField("a", "titleColor");
            this.s.AddField("a", "startTime");
            this.s.AddField("a", "endTime");
            this.s.AddField("a", "publicAdpic");
            this.s.AddField("a", "content");
            this.s.AddField("a", "address");
            this.s.AddField("a", "phone");
            this.s.AddField("a", "qq");
            this.s.AddField("a", "keywords");
            this.s.AddField("a", "readCount");
            this.s.AddField("a", "isChecked");
            this.s.AddField("a", "itemIndex");
            this.s.AddField("a", "isClosed");
            this.s.AddField("a", "onIndex");

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            this.s.AddWhere(string.Empty, "l", "locationId", "=", "a", "cityId");
            this.s.AddWhere("and", "a", "cityId", "in", "(" + this.sql + ")");

            if (!string.IsNullOrEmpty(msg.Trim()))
            {
                this.s.AddWhere("and", "a", "actName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.s.AddOrderBy("a", "itemIndex", false);
            this.s.AddOrderBy("a", "endTime", false);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.s.SqlCount(), this.param).ToString());
            pr.SetBaseParam();

            pr.PageResult = this.db.GetDataTable(this.s.SqlPage(pr.PageSize, pr.StartIndex), this.param);

            return pr;
        }

        public bool Delete(int actId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity");

            this.s.AddWhere(string.Empty, string.Empty, "actId", "=", "@actId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity");

            this.s.AddField("cityId");
            this.s.AddField("actName");
            this.s.AddField("titleColor");
            this.s.AddField("startTime");
            this.s.AddField("endTime");
            this.s.AddField("publicAdpic");
            this.s.AddField("content");
            this.s.AddField("address");
            this.s.AddField("phone");
            this.s.AddField("qq");
            this.s.AddField("keywords");
            this.s.AddField("readCount");
            this.s.AddField("isChecked");
            this.s.AddField("itemIndex");
            this.s.AddField("isClosed");
            this.s.AddField("onIndex");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("actName", content["actName"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("endTime", content["endTime"]);
            this.param.Add("publicAdpic", content["publicAdpic"]);
            this.param.Add("content", JsonDo.CleanCharForJson(content["content"].ToString()));
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("isChecked", 0);
            this.param.Add("isClosed", 0);
            this.param.Add("readCount", 0);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("onIndex", content["onIndex"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity");

            this.s.AddField("cityId");
            this.s.AddField("actName");
            this.s.AddField("titleColor");
            this.s.AddField("startTime");
            this.s.AddField("endTime");
            this.s.AddField("publicAdpic");
            this.s.AddField("content");
            this.s.AddField("address");
            this.s.AddField("phone");
            this.s.AddField("qq");
            this.s.AddField("keywords");
            this.s.AddField("itemIndex");
            this.s.AddField("onIndex");

            this.s.AddWhere("", "", "actId", "=", "@actId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("actName", content["actName"]);
            this.param.Add("titleColor", content["titleColor"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("endTime", content["endTime"]);
            this.param.Add("publicAdpic", content["publicAdpic"]);
            this.param.Add("content", JsonDo.CleanCharForJson(content["content"].ToString()));
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("keywords", content["keywords"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("onIndex", content["onIndex"]);
            this.param.Add("actId", content["actId"]);

            return this.db.Update(this.sql, this.param);
        }

        public int SetReadcount(int actId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity");

            this.s.AddField("readCount");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "actId", "=", "@actId");

            this.sql = this.s.SqlIncrease() + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("actId", actId);

            return (int)this.db.GetDataValue(this.sql, this.param);
        }

        public bool SetOnIndex(int actId)
        {
            Dictionary<string, object> item = this.GetOne(actId);

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Activity");
            this.s.AddField("onIndex");
            this.s.AddWhere("", "", "actId", "=", "@actId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            if (Boolean.Parse(item["onIndex"].ToString()))
            {
                this.param.Add("onIndex", 0);
            }
            else
            {
                this.param.Add("onIndex", 1);
            }

            this.param.Add("actId", actId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
