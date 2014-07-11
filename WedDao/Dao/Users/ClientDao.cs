using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.Users
{
    public class ClientDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public ClientDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int clientId)
        {
            this.sql = @"select [clientId],[userId],[locationId],[trueName],[sex],[address],[phone],[qq],[email],[isDeleted],[insertTime],[updateTime] from [User_Client] where [clientId]=@clientId";

            this.param = new Dictionary<string, object>();
            this.param.Add("clientId", clientId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public bool Delete(int userId, int clientId)
        {
            this.sql = @"update [Sys_Users] set [isDeleted]=1 where [userId]=@userId;update [User_Client] set [isDeleted]=1 where [clientId]=@clientId ";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", userId);
            this.param.Add("clientId", clientId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.sql = @"insert into [User_Client] ([userId],[locationId],[fullName],[sex],[address],[phone],[qq],[email],[isDeleted],[insertTime],[updateTime])values(@userId,@locationId,@fullName,@sex,@address,@phone,@qq,@email,0,@insertTime,@updateTime)";

            DateTime now = DateTime.Now;

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.sql = @"update [User_Client] set [userId]=@userId,[locationId]=@locationId,[fullName]=@fullName,[sex]=@sex,[address]=@address,[phone]=@phone,[qq]=@qq,[email]=@email,[updateTime]=@updateTime where [clientId]=@clientId";

            this.param = new Dictionary<string, object>();
            this.param.Add("userId", content["userId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("fullName", content["fullName"]);
            this.param.Add("sex", content["sex"]);
            this.param.Add("address", content["address"]);
            this.param.Add("phone", content["phone"]);
            this.param.Add("qq", content["qq"]);
            this.param.Add("email", content["email"]);
            this.param.Add("updateTime", DateTime.Now);
            this.param.Add("clientId", content["clientId"]);

            return this.db.Update(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(String msg, int locationId)
        {
            this.param = new Dictionary<string, object>();
            if (locationId > 0)
            {
                this.sql = @"select u.[userId],u.[userName],u.[lastLogin],c.[clientId],c.[locationId],c.[fullName],c.[phone],l.[cnName] from [Sys_Users] as u,[User_Client] as c,[Sys_Locations] as l where u.[userId]=c.[userId] and c.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and c.[isDeleted]=0 and u.[userType]='C' and c.[locationId]=@locationId and (c.[fullName] like '%'+@msg+'%' or c.[phone] like '%'+@msg+'%') order by c.[fullName] asc";
                this.param.Add("locationId", locationId);
            }
            else
            {
                this.sql = @"select u.[userId],u.[userName],u.[lastLogin],c.[clientId],c.[locationId],c.[fullName],c.[phone],l.[cnName] from [Sys_Users] as u,[User_Client] as c,[Sys_Locations] as l where u.[userId]=c.[userId] and c.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and c.[isDeleted]=0 and u.[userType]='C' and (c.[fullName] like '%'+@msg+'%' or c.[phone] like '%'+@msg+'%') order by c.[fullName] asc";
            }

            this.param.Add("msg", msg);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, String msg, int locationId)
        {
            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            this.param = new Dictionary<string, object>();

            if (locationId > 0)
            {
                pr.CountKey = @"c.[clientId]";
                pr.SqlFields = @"u.[userId],u.[userName],u.[lastLogin],c.[clientId],c.[locationId],c.[fullName],c.[phone],l.[cnName]";
                pr.SqlOrderBy = @"c.[fullName] asc";
                pr.SqlTable = @"[Sys_Users] as u,[User_Client] as c,[Sys_Locations] as l";
                pr.SqlWhere = @"u.[userId]=c.[userId] and c.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and c.[isDeleted]=0 and u.[userType]='C' and c.[locationId]=@locationId and (c.[fullName] like '%'+@msg+'%' or c.[phone] like '%'+@msg+'%')";

                this.param.Add("locationId", locationId);
            }
            else
            {
                pr.CountKey = @"c.[clientId]";
                pr.SqlFields = @"u.[userId],u.[userName],u.[lastLogin],c.[clientId],c.[locationId],c.[fullName],c.[phone],l.[cnName]";
                pr.SqlOrderBy = @"c.[fullName] asc";
                pr.SqlTable = @"[Sys_Users] as u,[User_Client] as c,[Sys_Locations] as l";
                pr.SqlWhere = @"u.[userId]=c.[userId] and c.[locationId]=l.[locationId] and u.[locationId]=l.[locationId] and c.[isDeleted]=0 and u.[userType]='C' and (c.[fullName] like '%'+@msg+'%' or c.[phone] like '%'+@msg+'%')";
            }

            this.param.Add("msg", msg);

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(pr.CountSql, this.param).ToString());
            pr.SetBaseParam();
            pr.PageResult = this.db.GetDataTable(pr.QuerySql, this.param);

            return pr;
        }
    }
}
