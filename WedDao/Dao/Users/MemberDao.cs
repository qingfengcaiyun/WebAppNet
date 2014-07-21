using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Users
{
    public class MemberDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public MemberDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(long memberId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.AddField("l", "cnName");
            this.s.AddField("m", "memberId");
            this.s.AddField("m", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("m", "locationId");
            this.s.AddField("m", "fullName");
            this.s.AddField("m", "shortName");
            this.s.AddField("m", "address");
            this.s.AddField("m", "tel");
            this.s.AddField("m", "cellphone");
            this.s.AddField("m", "fax");
            this.s.AddField("m", "qq");
            this.s.AddField("m", "email");
            this.s.AddField("m", "transit");
            this.s.AddField("m", "logoUrl");
            this.s.AddField("m", "memo");
            this.s.AddField("m", "insertTime");
            this.s.AddField("m", "updateTime");

            this.s.AddWhere("", "m", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "m", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "m", "memberId", "=", "@memberId");

            this.sql = this.s.SqlSelect();

            //this.sql = @"select [memberId],[userId],[locationId],[fullName],[shortName],[address],[tel],[cellphone],[fax],[qq],[email],[logoUrl],[memo],[insertTime],[updateTime] from [User_Member] where [memberId]=@memberId";

            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", memberId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            this.param = new Dictionary<string, object>();

            this.s = new SqlBuilder();

            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.AddOrderBy("m", "fullName", true);

            this.s.AddField("l", "cnName");
            this.s.AddField("m", "memberId");
            this.s.AddField("m", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("m", "locationId");
            this.s.AddField("m", "fullName");
            this.s.AddField("m", "shortName");
            this.s.AddField("m", "address");
            this.s.AddField("m", "tel");
            this.s.AddField("m", "cellphone");
            this.s.AddField("m", "fax");
            this.s.AddField("m", "qq");
            this.s.AddField("m", "email");
            this.s.AddField("m", "transit");
            this.s.AddField("m", "logoUrl");
            this.s.AddField("m", "memo");
            this.s.AddField("m", "insertTime");
            this.s.AddField("m", "updateTime");

            this.s.AddWhere("", "m", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "m", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "m", "locationId", "=", "@locationId");

            this.param.Add("locationId", locationId);

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "m", "memberId", "like", "'%'+@msg+'%'");
                this.param.Add("msg", msg);
            }

            this.sql = this.s.SqlSelect();

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int locationId)
        {
            this.param = new Dictionary<string, object>();

            this.s = new SqlBuilder();

            this.s.AddTable("User_Member", "m");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Sys_User", "u");

            this.s.AddOrderBy("m", "fullName", true);

            this.s.SetTagField("m", "memberId");

            this.s.AddField("l", "cnName");
            this.s.AddField("m", "memberId");
            this.s.AddField("m", "userId");
            this.s.AddField("u", "userName");
            this.s.AddField("u", "lastLogin");
            this.s.AddField("m", "locationId");
            this.s.AddField("m", "fullName");
            this.s.AddField("m", "shortName");
            this.s.AddField("m", "address");
            this.s.AddField("m", "tel");
            this.s.AddField("m", "cellphone");
            this.s.AddField("m", "fax");
            this.s.AddField("m", "qq");
            this.s.AddField("m", "email");
            this.s.AddField("m", "transit");
            this.s.AddField("m", "logoUrl");
            this.s.AddField("m", "memo");
            this.s.AddField("m", "insertTime");
            this.s.AddField("m", "updateTime");

            this.s.AddWhere("", "m", "userId", "=", "u", "userId");
            this.s.AddWhere("and", "m", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "m", "locationId", "=", "@locationId");

            this.param.Add("locationId", locationId);

            if (!string.IsNullOrEmpty(msg))
            {
                this.s.AddWhere("and", "m", "memberId", "like", "'%'+@msg+'%'");
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

        public bool Delete(int userId, int memberId)
        {
            this.sql = @"update [Sys_Users] set [isDeleted]=true where [userId]=@userId;update [User_Member] set [isDeleted]=true where [memberId]=@memberId ";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("memberId", memberId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Member");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("shortName");
            this.s.AddField("address");
            this.s.AddField("tel");
            this.s.AddField("cellphone");
            this.s.AddField("fax");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("transit");
            this.s.AddField("logoUrl");
            this.s.AddField("memo");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("shortName", content["shortName"]);
            this.param.Add("address", content["address"]);
            this.param.Add("tel", content["tel"]);
            this.param.Add("cellphone", content["cellphone"]);
            this.param.Add("fax", content["fax"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("transit", content["transit"]);
            this.param.Add("logoUrl", content["logoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("User_Member");

            this.s.AddField("userId");
            this.s.AddField("locationId");
            this.s.AddField("fullName");
            this.s.AddField("shortName");
            this.s.AddField("address");
            this.s.AddField("tel");
            this.s.AddField("cellphone");
            this.s.AddField("fax");
            this.s.AddField("qq");
            this.s.AddField("email");
            this.s.AddField("transit");
            this.s.AddField("logoUrl");
            this.s.AddField("memo");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("shortName", content["shortName"]);
            this.param.Add("address", content["address"]);
            this.param.Add("tel", content["tel"]);
            this.param.Add("cellphone", content["cellphone"]);
            this.param.Add("fax", content["fax"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("transit", content["transit"]);
            this.param.Add("logoUrl", content["logoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("updateTime", DateTime.Now);

            return this.db.Update(this.sql, this.param);
        }
    }
}
