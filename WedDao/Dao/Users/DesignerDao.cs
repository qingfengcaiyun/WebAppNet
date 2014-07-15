using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Users
{
    public class DesignerDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public DesignerDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int designerId)
        {
            this.sql = @"select [designerId],[userId],[locationId],[fullName],[sex],[memberId],[job],[tel],[cellphone],[qq],[email],[photoUrl],[memo],[insertTime],[updateTime],[isDeleted] from [User_Designer] where [designerId]=@designerId";

            this.param = new Dictionary<string, object>();
            this.param.Add("designerId", designerId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int memberId, int locationId)
        {
            this.param = new Dictionary<string, object>();
            if (memberId > 0)
            {
                this.sql = @"select d.[designerId],d.[userId],d.[locationId],d.[fullName],d.[sex],d.[memberId],d.[job],d.[tel],d.[cellphone],d.[qq],d.[email],d.[photoUrl],d.[insertTime],d.[updateTime],m.[shortName] from [User_Member] as m,[User_Designer] as d where d.[memberId]=m.[memberId] and d.[memberId]=@memberId and d.[locationId]=@locationId and d.[isDeleted]=0 and d.[fullName] like '%'+@msg+'%' order by d.[fullName] asc";
                this.param.Add("memberId", memberId);
            }
            else
            {
                this.sql = @"select d.[designerId],d.[userId],d.[locationId],d.[fullName],d.[sex],d.[memberId],d.[job],d.[tel],d.[cellphone],d.[qq],d.[email],d.[photoUrl],d.[insertTime],d.[updateTime],m.[shortName] from [User_Member] as m,[User_Designer] as d where d.[memberId]=m.[memberId] and m.[locationId]=@locationId and d.[isDeleted]=false and d.[fullName] like '%'+@msg+'%' order by d.[fullName] asc";
            }
            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int memberId, int locationId)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.CountKey = @"d.[designerId]";
            pr.SqlFields = @"d.[designerId],d.[userId],d.[locationId],d.[fullName],d.[sex],d.[memberId],d.[job],d.[tel],d.[cellphone],d.[qq],d.[email],d.[photoUrl],d.[insertTime],d.[updateTime],m.[shortName]";
            pr.SqlOrderBy = @"d.[fullName] asc";
            pr.SqlTable = @"[User_Member] as m,[User_Designer] as d";

            this.param = new Dictionary<string, object>();
            if (memberId > 0)
            {
                pr.SqlWhere = @"d.[memberId]=m.[memberId] and d.[memberId]=@memberId and d.[locationId]=@locationId and d.[isDeleted]=0 and d.[fullName] like '%'+@msg+'%'";

                this.param.Add("memberId", memberId);
            }
            else
            {
                pr.SqlWhere = @"d.[memberId]=m.[memberId] and d.[locationId]=@locationId and d.[isDeleted]=0 and d.[fullName] like '%'+@msg+'%'";
            }

            this.param.Add("locationId", locationId);
            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

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
            this.sql = @"insert into [User_Designer] ([userId],[locationId],[fullName],[sex],[memberId],[job],[tel],[cellphone],[qq],[email],[photoUrl],[memo],[insertTime],[updateTime],[isDeleted])values(@userId,@locationId,@fullName,@sex,@memberId,@job,@tel,@cellphone,@qq,@email,@photoUrl,@memo,@,@,0)";

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
            this.sql = @"update [User_Designer] set [userId]=@userId,[locationId]=@locationId,[fullName]=@fullName,[sex]=@sex,[memberId]=@memberId,[job]=@job,[tel]=@tel,[cellphone]=@cellphone,[qq]=@qq,[email]=@email,[photoUrl]=@photoUrl,[memo]=@memo,[updateTime]=@updateTime where [designerId]=@designerId";

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
