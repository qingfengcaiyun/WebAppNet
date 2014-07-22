using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.Info
{
    public class RelationshipDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public RelationshipDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int newsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Relationship", "r");
            this.s.AddTable("Info_Category", "c");

            this.s.AddField("c", "cateName");

            this.s.AddField("r", "cateId");
            this.s.AddField("r", "newsId");

            this.s.AddWhere("", "r", "cateId", "=", "c", "cateId");
            this.s.AddWhere("and", "r", "newsId", "=", "@newsId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public string GetCateList(Int64 newsId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Info_Relationship");
            this.s.AddField("cateId");
            this.s.AddWhere("", "", "newsId", "=", "@newsId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataValueString(this.sql, this.param);
        }

        public bool SaveList(Int64[] cateIds, Int64 newsId)
        {
            if (newsId > 0)
            {
                this.s = new SqlBuilder();
                this.s.AddTable("Info_Relationship");
                this.s.AddWhere("", "", "newsId", "=", "@newsId");

                this.sql = this.s.SqlDelete();

                this.param = new Dictionary<string, object>();
                this.param.Add("newsId", newsId);

                this.db.Update(this.sql, this.param);
            }

            if (cateIds.Length > 0)
            {
                this.s = new SqlBuilder();
                this.s.AddTable("Info_Relationship");
                this.s.AddField("cateId");
                this.s.AddField("newsId");

                this.sql = this.s.SqlInsert();
                List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();

                for (int i = 0, j = cateIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("cateId", cateIds[i]);
                    this.param.Add("newsId", newsId);

                    paramList.Add(this.param);
                }

                return this.db.Batch(this.sql, paramList);
            }
            else
            {
                return false;
            }
        }
    }
}
