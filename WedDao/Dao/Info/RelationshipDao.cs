using System.Collections.Generic;
using Glibs.Sql;
using System;

namespace WedDao.Dao.Info
{
    public class RelationshipDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public RelationshipDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int newsId)
        {
            this.sql = @"select [cateId],[newsId],[isPrimary] from [Info_Relationship] where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public string GetCateList(Int64 newsId)
        {
            this.sql = @"select [cateId] from [Info_Relationship] where [newsId]=@newsId";

            this.param = new Dictionary<string, object>();
            this.param.Add("newsId", newsId);

            return this.db.GetDataValueString(this.sql, this.param);
        }

        public bool SaveList(Int64[] cateIds, Int64 newsId)
        {
            if (newsId > 0)
            {
                this.sql = @"delete from [Info_Relationship] where [newsId]=@newsId;";

                this.param = new Dictionary<string, object>();
                this.param.Add("newsId", newsId);

                this.db.Update(this.sql, this.param);
            }

            if (cateIds.Length > 0)
            {
                this.sql = @"insert into [Info_Relationship] ([cateId],[newsId])values(@cateId,@newsId)";
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
