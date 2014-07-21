using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Users
{
    public class DesignerDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public DesignerDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int designerId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.AddField("m", "fullName", "member");
            this.s.AddField("l", "cnName");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("d", "designerId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "fullName");
            this.s.AddField("d", "sex");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "job");
            this.s.AddField("d", "tel");
            this.s.AddField("d", "cellphone");
            this.s.AddField("d", "qq");
            this.s.AddField("d", "email");
            this.s.AddField("d", "photoUrl");
            this.s.AddField("d", "memo");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");
            this.s.AddField("d", "isDeleted");

            this.s.AddWhere("", "u", "userId", "=", "d", "userId");
            this.s.AddWhere("and", "l", "locationId", "=", "d", "locationId");
            this.s.AddWhere("and", "m", "memberId", "=", "d", "memberId");
            this.s.AddWhere("and", "d", "designerId", "=", "@designerId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("designerId", designerId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int memberId, int locationId)
        {
            this.param = new Dictionary<string, object>();

            this.s = new SqlBuilder();

            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.AddField("m", "fullName", "member");
            this.s.AddField("l", "cnName");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("d", "designerId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "fullName");
            this.s.AddField("d", "sex");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "job");
            this.s.AddField("d", "tel");
            this.s.AddField("d", "cellphone");
            this.s.AddField("d", "qq");
            this.s.AddField("d", "email");
            this.s.AddField("d", "photoUrl");
            this.s.AddField("d", "memo");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");
            this.s.AddField("d", "isDeleted");

            this.s.AddOrderBy("d", "fullName", true);

            this.s.AddWhere("", "u", "userId", "=", "d", "userId");
            this.s.AddWhere("and", "l", "locationId", "=", "d", "locationId");
            this.s.AddWhere("and", "m", "memberId", "=", "d", "memberId");
            this.s.AddWhere("and", "d", "locationId", "=", "@locationId");

            this.param.Add("locationId", locationId);

            if (memberId > 0)
            {
                this.s.AddWhere("and", "d", "memberId", "=", "@memberId");
                this.param.Add("memberId", memberId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "d", "fullName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int memberId, int locationId)
        {
            this.param = new Dictionary<string, object>();

            this.s = new SqlBuilder();

            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.SetTagField("d", "designerId");

            this.s.AddField("m", "fullName", "member");
            this.s.AddField("l", "cnName");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("d", "designerId");
            this.s.AddField("d", "userId");
            this.s.AddField("d", "locationId");
            this.s.AddField("d", "fullName");
            this.s.AddField("d", "sex");
            this.s.AddField("d", "memberId");
            this.s.AddField("d", "job");
            this.s.AddField("d", "tel");
            this.s.AddField("d", "cellphone");
            this.s.AddField("d", "qq");
            this.s.AddField("d", "email");
            this.s.AddField("d", "photoUrl");
            this.s.AddField("d", "memo");
            this.s.AddField("d", "insertTime");
            this.s.AddField("d", "updateTime");
            this.s.AddField("d", "isDeleted");

            this.s.AddOrderBy("d", "fullName", true);

            this.s.AddWhere("", "u", "userId", "=", "d", "userId");
            this.s.AddWhere("and", "l", "locationId", "=", "d", "locationId");
            this.s.AddWhere("and", "m", "memberId", "=", "d", "memberId");
            this.s.AddWhere("and", "d", "locationId", "=", "@locationId");

            this.param.Add("locationId", locationId);

            if (memberId > 0)
            {
                this.s.AddWhere("and", "d", "memberId", "=", "@memberId");
                this.param.Add("memberId", memberId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "d", "fullName", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }


            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.s.SqlCount(), this.param).ToString());
            pr.SetBaseParam();

            pr.PageResult = this.db.GetDataTable(this.s.SqlPage(pr.PageSize, pr.StartIndex), this.param);

            return pr;
        }

        public bool Delete(int userId, int designerId)
        {
            this.sql = @"update [Sys_Users] set [isDeleted]=1 where [userId]=@userId;update [User_Designer] set [isDeleted]=1 where [designerId]=@designerId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("designerId", designerId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Designer");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("sex");
            this.s.AddField("memberId");
            this.s.AddField("job");
            this.s.AddField("tel");
            this.s.AddField("cellphone");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("photoUrl");
            this.s.AddField("memo");
            this.s.AddField("isDeleted");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("job", content["job"]);
            this.param.Add("tel", content["tel"]);
            this.param.Add("cellphone", content["cellphone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("photoUrl", content["photoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Designer");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("sex");
            this.s.AddField("memberId");
            this.s.AddField("job");
            this.s.AddField("tel");
            this.s.AddField("cellphone");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("photoUrl");
            this.s.AddField("memo");
            this.s.AddField("updateTime");

            this.s.AddWhere(string.Empty, string.Empty, "designerId", "=", "@designerId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("job", content["job"]);
            this.param.Add("tel", content["tel"]);
            this.param.Add("cellphone", content["cellphone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("photoUrl", content["photoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("designerId", content["designerId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
