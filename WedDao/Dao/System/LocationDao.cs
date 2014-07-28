using System;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WebDao.Dao.System
{
    public class LocationDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public LocationDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int locationId)
        {
            this.s = new SqlBuilder();

            this.s.AddField("locationId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");
            this.s.AddField("isLeaf");

            this.s.AddTable("Sys_Location");

            this.s.AddWhere(string.Empty, string.Empty, "locationId", "=", "@locationId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("locationId", locationId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public Dictionary<string, object> GetOne(string levelNo)
        {
            this.s = new SqlBuilder();

            this.s.AddField("locationId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");
            this.s.AddField("isLeaf");

            this.s.AddTable("Sys_Location");

            this.s.AddWhere(string.Empty, string.Empty, "levelNo", "=", "@levelNo");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("levelNo", levelNo);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            this.s = new SqlBuilder();

            this.s.AddField("locationId");
            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");
            this.s.AddField("isLeaf");

            this.s.AddTable("Sys_Location");

            this.s.AddWhere(string.Empty, string.Empty, "parentNo", "like", "@parentNo+'%'");

            this.s.AddOrderBy("levelNo", true);

            this.sql = this.s.SqlSelect();

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "levelNo", "=", "@levelNo");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isLeaf", 0);
            this.param.Add("levelNo", content["parentNo"]);

            this.db.Update(this.sql, this.param);

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location");

            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");
            this.s.AddField("isLeaf");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("levelNo", content["levelNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("levelCnName", content["levelCnName"]);
            this.param.Add("levelEnName", content["levelEnName"]);
            this.param.Add("isLeaf", 1);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            Dictionary<string, object> location = this.GetOne(Int32.Parse(content["locationId"].ToString()));

            if (!location["levelNo"].ToString().StartsWith(content["parentNo"].ToString()))
            {
                List<Dictionary<string, object>> list = this.GetList(location["levelNo"].ToString());

                if (list != null && list.Count > 0)
                {
                    List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();
                    Dictionary<string, object> item = null;

                    string parentNo = content["levelNo"].ToString();

                    for (int i = 0, j = list.Count; i < j; i++)
                    {
                        item = list[i];
                        this.param = new Dictionary<string, object>();

                        this.param.Add("levelNo", parentNo + item["levelNo"].ToString().Substring(location["levelNo"].ToString().Length));
                        this.param.Add("parentNo", parentNo + item["parentNo"].ToString().Substring(location["levelNo"].ToString().Length));
                        this.param.Add("locationId", Int32.Parse(item["locationId"].ToString()));

                        paramList.Add(this.param);
                    }

                    this.s = new SqlBuilder();

                    this.s.AddTable("Sys_Location");

                    this.s.AddField("cnName");
                    this.s.AddField("enName");
                    this.s.AddField("levelNo");
                    this.s.AddField("parentNo");
                    this.s.AddField("levelCnName");
                    this.s.AddField("levelEnName");

                    this.s.AddWhere("", "", "locationId", "=", "@locationId");

                    this.sql = this.s.SqlUpdate();
                    this.db.Batch(this.sql, paramList);
                }
            }

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "levelNo", "=", "@levelNo");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Location");

            this.s.AddField("cnName");
            this.s.AddField("enName");
            this.s.AddField("levelNo");
            this.s.AddField("parentNo");
            this.s.AddField("levelCnName");
            this.s.AddField("levelEnName");

            this.s.AddWhere("", "", "locationId", "=", "@locationId");

            this.sql = this.sql + ";" + this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("cnName", content["cnName"]);
            this.param.Add("enName", content["enName"]);
            this.param.Add("levelNo", content["levelNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("levelCnName", content["levelCnName"]);
            this.param.Add("levelEnName", content["levelEnName"]);
            this.param.Add("locationId", content["locationId"]);

            return this.db.Update(this.sql, this.param);
        }

        public bool Delete(string levelNo)
        {
            this.s = new SqlBuilder();
            this.s.AddTable("Sys_Location");
            this.s.AddWhere("", "", "levelNo", "like", "@levelNo+'%'");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("levelNo", levelNo);

            return this.db.Update(this.sql, this.param);
        }
    }
}
