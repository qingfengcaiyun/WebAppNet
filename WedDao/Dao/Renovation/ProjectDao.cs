using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class ProjectDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public ProjectDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.s.AddField("projectId");
            this.s.AddField("cityId");
            this.s.AddField("regionId");
            this.s.AddField("buildingId");
            this.s.AddField("memberId");
            this.s.AddField("designerId");
            this.s.AddField("pName");
            this.s.AddField("clientId");
            this.s.AddField("isClosed");
            this.s.AddField("startTime");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectParam", "r");
            this.s.AddTable("Renovation_Parameter", "p");

            this.s.AddField("p", "paramName");
            this.s.AddField("p", "paramValue");

            this.s.AddWhere(string.Empty, "r", "projectId", "=", "@projectId");
            this.s.AddWhere("and", "r", "paramId", "=", "p", "paramId");

            this.sql = this.sql + ";" + this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectPic", "p");
            this.s.AddTable("Sys_FileInfo", "f");

            this.s.AddField("f", "filePath");
            this.s.AddField("p", "itemIndex");
            this.s.AddField("f", "fileName");

            this.s.AddWhere(string.Empty, "f", "fileId", "=", "p", "fileId");
            this.s.AddWhere("and", "p", "projectId", "=", "@projectId");

            this.s.AddOrderBy("p", "itemIndex", true);

            this.sql = this.sql + ";" + this.s.SqlSelect() + ";";

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            List<List<Dictionary<string, object>>> lists = this.db.GetDataSet(this.sql, this.param);

            lists[0][0].Add("params", lists[1]);
            lists[0][0].Add("pics", lists[2]);

            return lists[0][0];
        }

        public List<Dictionary<string, object>> GetList(string msg, int cityId, int regionId)
        {
            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", cityId);

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project", "p");
            this.s.AddTable("Renovation_Building", "b");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("User_Designer", "d");

            this.s.AddOrderBy("p", "projectId", false);

            this.s.AddField("p", "pName");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("l", "cnName");
            this.s.AddField("m", "fullName", "member");
            this.s.AddField("d", "fullName", "designer");
            this.s.AddField("p", "isClosed");
            this.s.AddField("p", "startTime");

            this.s.AddWhere(string.Empty, "p", "buildingId", "=", "b", "buildingId");
            this.s.AddWhere("and", "p", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "p", "designerId", "=", "d", "designerId");
            this.s.AddWhere("and", "p", "regionId", "=", "l", "locationId");
            this.s.AddWhere("and", "p", "cityId", "=", "@cityId");

            if (regionId > 0)
            {
                this.s.AddWhere("and", "p", "regionId", "=", "@regionId");
                this.param.Add("regionId", regionId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.param.Add("msg", msg);

                this.s.AddWhere("and", "(p", "pName", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "b", "buildingsName", "like", "'%'+@msg+'%')");
            }

            return this.db.GetDataTable(this.s.SqlSelect(), this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, int regionId, string msg)
        {
            this.param = new Dictionary<string, object>();

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project", "p");
            this.s.AddTable("Renovation_Building", "b");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("User_Designer", "d");

            this.s.AddOrderBy("p", "projectId", false);

            this.s.AddField("p", "pName");
            this.s.AddField("b", "buildingsName");
            this.s.AddField("l", "cnName");
            this.s.AddField("m", "fullName", "member");
            this.s.AddField("d", "fullName", "designer");
            this.s.AddField("p", "isClosed");
            this.s.AddField("p", "startTime");

            this.s.SetTagField("p", "projectId");

            this.s.AddWhere(string.Empty, "p", "buildingId", "=", "b", "buildingId");
            this.s.AddWhere("and", "p", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "p", "designerId", "=", "d", "designerId");
            this.s.AddWhere("and", "p", "regionId", "=", "l", "locationId");
            this.s.AddWhere("and", "p", "cityId", "=", "@cityId");

            if (regionId > 0)
            {
                this.s.AddWhere("and", "p", "regionId", "=", "@regionId");
                this.param.Add("regionId", regionId);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.param.Add("msg", msg);

                this.s.AddWhere("and", "(p", "pName", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "b", "buildingsName", "like", "'%'+@msg+'%')");
            }

            this.param.Add("cityId", cityId);

            PageRecords pr = new PageRecords();
            pr.CurrentPage = pageNo;
            pr.PageSize = pageSize;

            pr.RecordsCount = Int32.Parse(this.db.GetDataValue(this.s.SqlCount(), this.param).ToString());
            pr.SetBaseParam();

            pr.PageResult = this.db.GetDataTable(this.s.SqlPage(pr.PageSize, pr.StartIndex), this.param);

            return pr;
        }

        public bool Delete(long projectId)
        {
            this.sql = @"delete from [Renovation_Project] where [projectId]=@projectId;delete from [Renovation_ProjectParam] where [projectId]=@projectId;delete from [Renovation_ProjectPic] where [projectId]=@projectId;";

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content, int[] paramIds, long[] fileIds)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("cityId");
            this.s.AddField("regionId");
            this.s.AddField("buildingId");
            this.s.AddField("memberId");
            this.s.AddField("designerId");
            this.s.AddField("pName");
            this.s.AddField("clientId");
            this.s.AddField("isClosed");
            this.s.AddField("startTime");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;
            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("regionId", content["regionId"]);
            this.param.Add("buildingId", content["buildingId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("designerId", content["designerId"]);
            this.param.Add("pName", content["pName"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("isClosed", 0);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            long projectId = this.db.Insert(this.sql, this.param);

            if (paramIds != null && paramIds.Length > 0)
            {
                //开始delete
                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectParam");

                this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

                this.sql = this.s.SqlDelete();

                this.s.ClearTable();

                this.s.AddTable("Renovation_ProjectPic");

                this.sql = this.sql + ";" + this.s.SqlDelete();

                this.param = new Dictionary<string, object>();
                this.param.Add("projectId", projectId);

                this.db.Update(this.sql, this.param);

                //开始insert

                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectParam");

                this.s.AddField("projectId");
                this.s.AddField("paramId");

                this.sql = this.s.SqlInsert();

                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();

                foreach (int paramId in paramIds)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("paramId", paramId);

                    paramList.Add(this.param);
                }

                this.db.Batch(this.sql, paramList);

                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectPic");

                this.s.AddField("projectId");
                this.s.AddField("fileId");
                this.s.AddField("memo");
                this.s.AddField("itemIndex");
                this.s.AddField("insertTime");

                this.sql = this.s.SqlInsert();

                paramList = new List<Dictionary<string, object>>();

                for (int i = 0, j = fileIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("fileId", fileIds[i]);
                    this.param.Add("memo", string.Empty);
                    this.param.Add("itemIndex", 1001 + i);
                    this.param.Add("readCount", DateTime.Now);

                    paramList.Add(this.param);
                }

                this.db.Batch(this.sql, paramList);
            }

            return projectId;
        }

        public bool Update(Dictionary<string, object> content, int[] paramIds, long[] fileIds)
        {
            long projectId = Int64.Parse(content["projectId"].ToString());

            List<Dictionary<string, object>> paramList = null;

            if (paramIds != null && paramIds.Length > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectParam");

                this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

                this.sql = this.s.SqlDelete();
                this.param = new Dictionary<string, object>();
                this.param.Add("projectId", projectId);

                this.db.Update(this.sql, this.param);

                this.s.ClearWhere();

                this.s.AddField("projectId");
                this.s.AddField("paramId");

                this.sql = this.s.SqlInsert();

                paramList = new List<Dictionary<string, object>>();

                foreach (int paramId in paramIds)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("paramId", paramId);

                    paramList.Add(this.param);
                }

                this.db.Batch(this.sql, paramList);
            }

            if (fileIds != null && fileIds.Length > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectPic");

                this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

                this.sql = this.s.SqlDelete();
                this.param = new Dictionary<string, object>();
                this.param.Add("projectId", projectId);

                this.db.Update(this.sql, this.param);

                this.s.ClearWhere();

                this.s.AddField("projectId");
                this.s.AddField("fileId");
                this.s.AddField("memo");
                this.s.AddField("itemIndex");
                this.s.AddField("insertTime");

                this.sql = this.s.SqlInsert();

                paramList = new List<Dictionary<string, object>>();

                for (int i = 0, j = fileIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("fileId", fileIds[i]);
                    this.param.Add("memo", string.Empty);
                    this.param.Add("itemIndex", 1001 + i);
                    this.param.Add("readCount", DateTime.Now);

                    paramList.Add(this.param);
                }

                this.db.Batch(this.sql, paramList);
            }

            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("cityId");
            this.s.AddField("regionId");
            this.s.AddField("buildingId");
            this.s.AddField("memberId");
            this.s.AddField("designerId");
            this.s.AddField("pName");
            this.s.AddField("clientId");
            this.s.AddField("isClosed");
            this.s.AddField("startTime");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.sql = this.s.SqlUpdate();

            DateTime now = DateTime.Now;
            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("regionId", content["regionId"]);
            this.param.Add("buildingId", content["buildingId"]);
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("designerId", content["designerId"]);
            this.param.Add("pName", content["pName"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("isClosed", 0);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);
            this.param.Add("projectId", content["projectId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool SetReadCount(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.sql = this.s.SqlIncrease();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
