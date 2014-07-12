using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Users
{
    public class MemberDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public MemberDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int memberId)
        {
            this.sql = @"select [memberId],[userId],[locationId],[fullName],[shortName],[address],[tel],[cellphone],[fax],[qq],[email],[logoUrl],[memo],[insertTime],[updateTime] from [User_Member] where [memberId]=@memberId";

            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", memberId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            this.sql = @"select u.[userId],u.[userName],u.[lastLogin],u.[locationId],m.[memberId],m.[locationId],m.[fullName],m.[shortName],l.[cnName] from [Sys_Users] as u,[User_Member] as m,[Sys_Locations] as l where u.[userId]=m.[userId] and m.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and u.[isDeleted]=0 and u.[userType]='M' and (m.[fullName] like '%'+@msg+'%' or shortName like '%'+@msg+'%') order by l.[levelNo] asc, m.[fullName] asc";
            this.param = new Dictionary<string, object>();
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int locationId)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.CountKey = @"u.[userId]";
            pr.SqlFields = @"u.[userId],u.[userName],u.[lastLogin],u.[locationId],m.[memberId],m.[locationId],m.[fullName],m.[shortName],l.[cnName]";
            pr.SqlOrderBy = @"l.[levelNo] asc, m.[fullName] asc";
            pr.SqlTable = @"[Sys_Users] as u,[User_Member] as m,[Sys_Locations] as l";
            pr.SqlWhere = @"u.[userId]=m.[userId] and m.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and u.[isDeleted]=0 and u.[userType]='M' and (m.[fullName] like '%'+@msg+'%' or m.[shortName] like '%'+@msg+'%')";

            this.param = new Dictionary<string, object>();
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

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
            this.sql = @"insert into [User_Member] ([userId],[locationId],[fullName],[shortName],[address],[tel],[cellphone],[fax],[qq],[email],[logoUrl],[memo],[insertTime],[updateTime])values(@userId,@locationId,@fullName,@shortName,@address,@tel,@cellphone,@fax,@qq,@email,@logoUrl,@memo,@insertTime,@updateTime)";

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
            this.param.Add("logoUrl", content["logoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [User_Member] set [userId]=@userId,[locationId]=@locationId,[fullName]=@fullName,[shortName]=@shortName,[address]=@address,[tel]=@tel,[cellphone]=@cellphone,[fax]=@fax,[qq]=@qq,[email]=@email,[logoUrl]=@logoUrl,[memo]=@memo,[updateTime]=@updateTime where [memberId]=@memberId";

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
            this.param.Add("logoUrl", content["logoUrl"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("updateTime", now);

            return this.db.Update(this.sql, this.param);
        }
    }
}
