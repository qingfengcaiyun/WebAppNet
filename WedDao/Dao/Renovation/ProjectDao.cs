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

            this.s.AddTable("Renovation_Project", "p");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Client", "c");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsName");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("d", "fullName", "designer");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("p", "projectId");
            this.s.AddField("p", "memberId");
            this.s.AddField("p", "designerId");
            this.s.AddField("p", "locationId");
            this.s.AddField("p", "buildingsId");
            this.s.AddField("p", "projectName");
            this.s.AddField("p", "clientId");
            this.s.AddField("p", "picSnap");
            this.s.AddField("p", "memo");
            this.s.AddField("p", "isClosed");
            this.s.AddField("p", "startTime");
            this.s.AddField("p", "itemIndex");
            this.s.AddField("p", "insertTime");
            this.s.AddField("p", "updateTime");

            this.s.AddWhere("", "p", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "p", "buildingsId", "=", "b", "buildingsId");
            this.s.AddWhere("and", "p", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "p", "designerId", "=", "d", "designerId");
            this.s.AddWhere("and", "p", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "p", "projectId", "=", "@projectId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetParams(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectParam", "pp");
            this.s.AddTable("Renovation_Parameter", "p");

            this.s.AddField("pp", "pptId");
            this.s.AddField("pp", "projectId");
            this.s.AddField("pp", "paramId");

            this.s.AddField("p", "paramName");
            this.s.AddField("p", "paramValue");

            this.s.AddWhere("", "pp", "paramId", "=", "p", "paramId");
            this.s.AddWhere("and", "pp", "projectId", "=", "@projectId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetPictures(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectPic");

            this.s.AddField("ppcId");
            this.s.AddField("projectId");
            this.s.AddField("picPath");
            this.s.AddField("insertTime");

            this.s.AddWhere("", "", "projectId", "=", "@projectId");

            this.s.AddOrderBy("insertTime", true);

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId, long memberId, long designerId)
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

            this.s.AddTable("Renovation_Project", "p");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Client", "c");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsName");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("d", "fullName", "designer");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("p", "projectId");
            this.s.AddField("p", "memberId");
            this.s.AddField("p", "designerId");
            this.s.AddField("p", "locationId");
            this.s.AddField("p", "buildingsId");
            this.s.AddField("p", "projectName");
            this.s.AddField("p", "clientId");
            this.s.AddField("p", "picSnap");
            this.s.AddField("p", "memo");
            this.s.AddField("p", "isClosed");
            this.s.AddField("p", "startTime");
            this.s.AddField("p", "itemIndex");
            this.s.AddField("p", "insertTime");
            this.s.AddField("p", "updateTime");

            this.s.AddWhere("", "p", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "p", "buildingsId", "=", "b", "buildingsId");
            this.s.AddWhere("and", "p", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "p", "designerId", "=", "d", "designerId");
            this.s.AddWhere("and", "p", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "p", "locationId", "in", "(" + this.sql + ")");

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            if (memberId > 0)
            {
                this.param.Add("memberId", memberId);
                this.s.AddWhere("and", "p", "memberId", "=", "@memberId");
            }

            if (designerId > 0)
            {
                this.param.Add("designerId", designerId);
                this.s.AddWhere("and", "p", "designerId", "=", "@designerId");
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.param.Add("msg", msg);

                this.s.AddWhere("and", "(p", "projectName", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "b", "buildingsName", "like", "'%'+@msg+'%')");
            }

            return this.db.GetDataTable(this.s.SqlSelect(), this.param);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, long memberId, long designerId, string msg)
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

            this.s.AddTable("Renovation_Project", "p");
            this.s.AddTable("Sys_Location", "l");
            this.s.AddTable("Renovation_Buildings", "b");
            this.s.AddTable("User_Member", "m");
            this.s.AddTable("User_Designer", "d");
            this.s.AddTable("User_Client", "c");

            this.s.SetTagField("p", "projectId");

            this.s.AddField("l", "cnName", "location");

            this.s.AddField("b", "buildingsName");

            this.s.AddField("m", "fullName", "member");

            this.s.AddField("d", "fullName", "designer");

            this.s.AddField("c", "fullName", "client");

            this.s.AddField("p", "projectId");
            this.s.AddField("p", "memberId");
            this.s.AddField("p", "designerId");
            this.s.AddField("p", "locationId");
            this.s.AddField("p", "buildingsId");
            this.s.AddField("p", "projectName");
            this.s.AddField("p", "clientId");
            this.s.AddField("p", "picSnap");
            this.s.AddField("p", "memo");
            this.s.AddField("p", "isClosed");
            this.s.AddField("p", "startTime");
            this.s.AddField("p", "itemIndex");
            this.s.AddField("p", "insertTime");
            this.s.AddField("p", "updateTime");

            this.s.AddOrderBy("p", "insertTime", false);

            this.s.AddWhere("", "p", "locationId", "=", "l", "locationId");
            this.s.AddWhere("and", "p", "buildingsId", "=", "b", "buildingsId");
            this.s.AddWhere("and", "p", "memberId", "=", "m", "memberId");
            this.s.AddWhere("and", "p", "designerId", "=", "d", "designerId");
            this.s.AddWhere("and", "p", "clientId", "=", "c", "clientId");
            this.s.AddWhere("and", "p", "locationId", "in", "(" + this.sql + ")");

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            if (memberId > 0)
            {
                this.param.Add("memberId", memberId);
                this.s.AddWhere("and", "p", "memberId", "=", "@memberId");
            }

            if (designerId > 0)
            {
                this.param.Add("designerId", designerId);
                this.s.AddWhere("and", "p", "designerId", "=", "@designerId");
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.param.Add("msg", msg);

                this.s.AddWhere("and", "(p", "projectName", "like", "'%'+@msg+'%'");
                this.s.AddWhere("or", "b", "buildingsName", "like", "'%'+@msg+'%')");
            }

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
            this.s = new SqlBuilder();

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.s.AddTable("Renovation_ProjectPic");

            this.sql = this.s.SqlDelete();

            this.s.ClearTable();

            this.s.AddTable("Renovation_ProjectParam");

            this.sql = this.sql + ";" + this.s.SqlDelete();

            this.s.ClearTable();

            this.s.AddTable("Renovation_Project");

            this.sql = this.sql + ";" + this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("memberId");
            this.s.AddField("designerId");
            this.s.AddField("locationId");
            this.s.AddField("buildingsId");
            this.s.AddField("projectName");
            this.s.AddField("clientId");
            this.s.AddField("memo");
            this.s.AddField("picSnap");
            this.s.AddField("isClosed");
            this.s.AddField("startTime");
            this.s.AddField("itemIndex");
            this.s.AddField("insertTime");
            this.s.AddField("updateTime");

            this.sql = this.s.SqlInsert();

            DateTime now = DateTime.Now;
            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("designerId", content["designerId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("buildingsId", content["buildingsId"]);
            this.param.Add("projectName", content["projectName"]);
            this.param.Add("clientId", content["clientId"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("picSnap", content["picSnap"]);
            this.param.Add("isClosed", 0);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("insertTime", now);
            this.param.Add("updateTime", now);

            return this.db.Insert(this.sql, this.param);
        }

        public bool SavePictures(long projectId, List<Dictionary<string, object>> projectPics)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectPic");
            this.s.AddWhere("", "", "projectId", "=", "@projectId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            this.db.Update(this.sql, this.param);

            if (projectPics != null && projectPics.Count > 0)
            {
                this.s = new SqlBuilder();

                this.s.AddTable("Renovation_ProjectPic");

                this.s.AddField("projectId");
                this.s.AddField("picPath");
                this.s.AddField("insertTime");

                this.sql = this.s.SqlInsert();

                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();

                foreach (Dictionary<string, object> projectPic in projectPics)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("picPath", projectPic["picPath"]);
                    this.param.Add("insertTime", DateTime.Now);

                    paramList.Add(this.param);
                }

                return this.db.Batch(this.sql, paramList);
            }
            else
            {
                return false;
            }
        }

        public long SavePicture(long projectId, string picPath)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectPic");

            this.s.AddField("projectId");
            this.s.AddField("picPath");
            this.s.AddField("insertTime");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);
            this.param.Add("picPath", picPath);
            this.param.Add("insertTime", DateTime.Now);

            return this.db.Insert(this.sql, param);
        }

        public bool DelPicture(string ppcIds)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectPic");

            this.s.AddWhere("", "", "ppcId", "in", "(" + ppcIds + ")");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();

            return this.db.Update(this.sql, param);
        }

        public bool SaveParams(long projectId, int[] paramIds)
        {
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

                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();

                foreach (int paramId in paramIds)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("projectId", projectId);
                    this.param.Add("paramId", paramId);

                    paramList.Add(this.param);
                }

                return this.db.Batch(this.sql, paramList);
            }
            else
            {
                return false;
            }
        }

        public long SaveParam(long projectId, int paramId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectParam");

            this.s.AddField("projectId");
            this.s.AddField("paramId");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);
            this.param.Add("paramId", paramId);

            return this.db.Insert(this.sql, this.param);
        }

        public bool DelParam(long pptId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_ProjectParam");

            this.s.AddWhere("", "", "pptId", "=", "@pptId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("pptId", pptId);

            return this.db.Update(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("memberId");
            this.s.AddField("designerId");
            this.s.AddField("locationId");
            this.s.AddField("buildingsId");
            this.s.AddField("projectName");
            this.s.AddField("memo");
            this.s.AddField("picSnap");
            this.s.AddField("startTime");
            this.s.AddField("itemIndex");
            this.s.AddField("updateTime");

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.sql = this.s.SqlUpdate();

            DateTime now = DateTime.Now;
            this.param = new Dictionary<string, object>();
            this.param.Add("memberId", content["memberId"]);
            this.param.Add("designerId", content["designerId"]);
            this.param.Add("locationId", content["locationId"]);
            this.param.Add("buildingsId", content["buildingsId"]);
            this.param.Add("projectName", content["projectName"]);
            this.param.Add("memo", content["memo"]);
            this.param.Add("picSnap", content["picSnap"]);
            this.param.Add("startTime", content["startTime"]);
            this.param.Add("itemIndex", content["itemIndex"]);
            this.param.Add("updateTime", now);
            this.param.Add("projectId", content["projectId"]);

            return this.db.Update(this.sql, this.param);
        }

        public int SetReadCount(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("readCount");

            this.s.AddIncrease("readCount", 1);

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.sql = this.s.SqlIncrease() + ";" + this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("projectId", projectId);

            return (int)this.db.GetDataValue(this.sql, this.param);
        }

        public bool CloseProject(long projectId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Renovation_Project");

            this.s.AddField("isClosed");

            this.s.AddWhere(string.Empty, string.Empty, "projectId", "=", "@projectId");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isClosed", 1);
            this.param.Add("projectId", projectId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
