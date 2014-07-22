using System;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;

namespace WebDao.Dao.Info
{
    public class CategoryDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public CategoryDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int cateId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category", "c");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName");

            this.s.AddField("c", "cateId");
            this.s.AddField("c", "cityId");
            this.s.AddField("c", "cateName");
            this.s.AddField("c", "cateNo");
            this.s.AddField("c", "parentNo");
            this.s.AddField("c", "isLeaf");

            this.s.AddWhere("", "l", "locationId", "=", "c", "cityId");
            this.s.AddWhere("and", "c", "cateId", "=", "@cateId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("cateId", cateId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public string GetCateId(string cateNo)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category");

            this.s.AddField("cateId");

            this.s.AddWhere("", "", "cateNo", "=", "@cateNo");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("cateNo", cateNo);

            return this.db.GetDataValue(this.sql, this.param).ToString();
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category", "c");
            this.s.AddTable("Sys_Location", "l");

            this.s.AddField("l", "cnName");

            this.s.AddField("c", "cateId");
            this.s.AddField("c", "cityId");
            this.s.AddField("c", "cateName");
            this.s.AddField("c", "cateNo");
            this.s.AddField("c", "parentNo");
            this.s.AddField("c", "isLeaf");

            this.s.AddWhere("", "l", "locationId", "=", "c", "cityId");
            this.s.AddWhere("and", "c", "parentNo", "like", "@parentNo+'%'");

            this.s.AddOrderBy("c", "cateNo", true);

            this.sql = this.s.SqlSelect();

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int cateId)
        {
            this.s = new SqlBuilder();
            this.s.AddTable("Info_Category");
            this.s.AddField("cateNo");
            this.s.AddWhere("", "", "cateId", "=", "@cateId");
            string cateNos = this.s.SqlSelect();

            this.s = new SqlBuilder();
            this.s.AddTable("Info_Category");
            this.s.AddField("cateId");
            this.s.AddWhere("", "", "cateNo", "like", "(" + cateNos + ")+'%'");
            string cateIds = this.s.SqlSelect();

            this.s = new SqlBuilder();
            this.s.AddTable("Info_Relationship");
            this.s.AddWhere("", "", "cateId", "in", "(" + cateIds + ")");

            this.sql = this.s.SqlDelete();

            this.s = new SqlBuilder();
            this.s.AddTable("Info_Category");
            this.s.AddWhere("", "", "cateNo", "like", "(" + cateNos + ")+'%'");

            this.sql = this.sql + ";" + this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("cateId", cateId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category");
            this.s.AddField("isLeaf");
            this.s.AddWhere("", "", "cateNo", "=", "@parentNo");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isLeaf", 0);
            this.param.Add("parentNo", content["parentNo"]);

            this.db.Update(this.sql, this.param);

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category");

            this.s.AddField("cityId");
            this.s.AddField("cateName");
            this.s.AddField("cateNo");
            this.s.AddField("parentNo");
            this.s.AddField("isLeaf");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateName", content["cateName"]);
            this.param.Add("cateNo", content["cateNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("isLeaf", 1);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            Dictionary<string, object> cate = this.GetOne(Int32.Parse(content["cateId"].ToString()));

            if (!cate["cateNo"].ToString().StartsWith(content["parentNo"].ToString()))
            {
                List<Dictionary<string, object>> list = this.GetList(cate["cateNo"].ToString());

                if (list != null && list.Count > 0)
                {
                    List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();
                    Dictionary<string, object> item = null;

                    string parentNo = content["cateNo"].ToString();

                    for (int i = 0, j = list.Count; i < j; i++)
                    {
                        item = list[i];

                        this.param = new Dictionary<string, object>();
                        this.param.Add("cateNo", parentNo + item["cateNo"].ToString().Substring(cate["cateNo"].ToString().Length));
                        this.param.Add("parentNo", parentNo + item["parentNo"].ToString().Substring(cate["cateNo"].ToString().Length));
                        this.param.Add("cateId", Int32.Parse(item["cateId"].ToString()));

                        //this.db.Update(this.sql, this.param);

                        paramList.Add(this.param);
                    }

                    this.s = new SqlBuilder();

                    this.s.AddTable("Info_Category");

                    this.s.AddField("cateNo");
                    this.s.AddField("parentNo");

                    this.s.AddWhere("", "", "cateId", "=", "@cateId");

                    this.sql = this.s.SqlUpdate();

                    this.db.Batch(this.sql, paramList);
                }
            }

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "cateNo", "=", "@parentNo");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();

            this.s.AddTable("Info_Category");

            this.s.AddField("cityId");
            this.s.AddField("cateName");
            this.s.AddField("cateNo");
            this.s.AddField("parentNo");

            this.s.AddWhere("", "", "cateId", "=", "@cateId");

            this.sql = this.sql + ";" + this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("isLeaf", 0);
            this.param.Add("cateNo", content["cateNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateName", content["cateName"]);
            this.param.Add("cateId", content["cateId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
