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
        private SqlBuilder s = null;

        public DiaryDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int diaryId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary", "d");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("User_Client", "c");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Renovation_Process", "proc");
            this.s.AddTable("Renovation_Project", "proj");

            this.s.AddField("l", "cnName");

            this.s.AddField("u", "userName");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("proc", "processName");

            this.s.AddField("proj", "pName");

            this.s.AddField("d", "diaryId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "clientId");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "processId");
            this.s.AddField("d", "projectId");
            this.s.AddField("d", "longTitle");
            this.s.AddField("d", "shortTitle");
            this.s.AddField("d", "content");
            this.s.AddField("d", "keywords");
            this.s.AddField("d", "picUrl");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");

            this.s.AddWhere("", "d", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "d", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "d", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "d", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "d", "processId", "=", "proc", "processId");
            this.s.AddWhere("and", "d", "projectId", "=", "proj", "projectId");
            this.s.AddWhere("and", "d", "diaryId", "=", "@diaryId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary", "d");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("User_Client", "c");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Renovation_Process", "proc");
            this.s.AddTable("Renovation_Project", "proj");

            this.s.AddField("l", "cnName");

            this.s.AddField("u", "userName");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("proc", "processName");

            this.s.AddField("proj", "pName");

            this.s.AddField("d", "diaryId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "clientId");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "processId");
            this.s.AddField("d", "projectId");
            this.s.AddField("d", "longTitle");
            this.s.AddField("d", "shortTitle");
            this.s.AddField("d", "content");
            this.s.AddField("d", "keywords");
            this.s.AddField("d", "picUrl");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");

            this.s.AddWhere("", "d", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "d", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "d", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "d", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "d", "processId", "=", "proc", "processId");
            this.s.AddWhere("and", "d", "projectId", "=", "proj", "projectId");
            this.s.AddWhere("and", "d", "locationId", "=", "@locationId");
            this.s.AddWhere("and", "(d", "longTitle", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "d", "shortTitle", "like", "'%'+@msg+'%')");

            this.s.AddOrderBy("d", "insertTime", false);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary", "d");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");
            this.s.AddTable("User_Client", "c");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Renovation_Process", "proc");
            this.s.AddTable("Renovation_Project", "proj");

            this.s.SetTagField("d", "diaryId");

            this.s.AddField("l", "cnName");

            this.s.AddField("u", "userName");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("proc", "processName");

            this.s.AddField("proj", "pName");

            this.s.AddField("d", "diaryId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "clientId");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "processId");
            this.s.AddField("d", "projectId");
            this.s.AddField("d", "longTitle");
            this.s.AddField("d", "shortTitle");
            this.s.AddField("d", "content");
            this.s.AddField("d", "keywords");
            this.s.AddField("d", "picUrl");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");

            this.s.AddWhere("", "d", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "d", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "d", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "d", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "d", "processId", "=", "proc", "processId");
            this.s.AddWhere("and", "d", "projectId", "=", "proj", "projectId");
            this.s.AddWhere("and", "d", "locationId", "=", "@locationId");
            this.s.AddWhere("and", "(d", "longTitle", "like", "'%'+@msg+'%'");
            this.s.AddWhere("or", "d", "shortTitle", "like", "'%'+@msg+'%')");

            this.s.AddOrderBy("d", "insertTime", false);

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.s.SqlCount(), this.param).ToString());
            pr.SetBaseParam();

            pr.PageResult = this.db.GetDataTable(this.s.SqlPage(pr.PageSize, pr.StartIndex), this.param);

            return pr;
        }

        public bool Delete(int diaryId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary");

            this.s.AddWhere("", "", "diaryId", "=", "@diaryId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary");

            this.s.AddField("locationId");
            this.s.AddField("userId");
            this.s.AddField("clientId");
            this.s.AddField("memberId");
            this.s.AddField("processId");
            this.s.AddField("projectId");
            this.s.AddField("longTitle");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("readCount");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

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
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary");

            this.s.AddField("locationId");
            this.s.AddField("userId");
            this.s.AddField("clientId");
            this.s.AddField("memberId");
            this.s.AddField("processId");
            this.s.AddField("projectId");
            this.s.AddField("longTitle");
            this.s.AddField("shortTitle");
            this.s.AddField("content");
            this.s.AddField("keywords");
            this.s.AddField("picUrl");
            this.s.AddField("readCount");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.s.AddWhere("", "", "diaryId", "=", "@diaryId");

            this.sql = this.s.SqlUpdate();

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

        public int SetReadCount(int diaryId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Diary");

            this.s.AddField("readCount");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "diaryId", "=", "@diaryId");

            this.sql = this.s.SqlIncrease() + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("diaryId", diaryId);

            return (int)this.db.GetDataValue(this.sql, this.param);
        }
    }
}
