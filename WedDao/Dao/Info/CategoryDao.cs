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

        public CategoryDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int cateId)
        {
            this.sql = @"select [cateId],[cityId],[cateName],[cateNo],[parentNo],[isLeaf] from [Info_Category] where [cateId]=@cateId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cateId", cateId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public string GetCateId(string cateNo)
        {
            this.sql = @"select [cateId] from [Info_Category] where [cateNo]=@cateNo";

            this.param = new Dictionary<string, object>();
            this.param.Add("cateNo", cateNo);

            return this.db.GetDataValue(this.sql, this.param).ToString();
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.sql = @"select [cateId],[cityId],[cateName],[cateNo],[parentNo],[isLeaf] from [Info_Category] where [parentNo] like @parentNo+'%' order by [cateNo] asc";

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(int cateId)
        {
            this.sql = @"delete from [Info_Relationship] where [cateId] in (select [cateId] from [Info_Category] where [cateNo] like (select [cateNo] from [Info_Category] where [cateId]=@cateId)+'%' ); delete from [Info_Category] where [cateNo] like (select [cateNo] from [Info_Category] where [cateId]=@cateId)+'%' );";

            this.param = new Dictionary<string, object>();
            this.param.Add("cateId", cateId);

            return this.db.Update(this.sql, this.param);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            this.sql = @"update [Info_Category] set [isLeaf]=0 where [cateNo]=@cateNo;insert into [Info_Category] ([cityId],[cateName],[cateNo],[parentNo],[isLeaf])values(@cityId,@cateName,@cateNo,@parentNo,1)";

            this.param = new Dictionary<string, object>();
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateName", content["cateName"]);
            this.param.Add("cateNo", content["cateNo"]);
            this.param.Add("parentNo", content["parentNo"]);

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

                    this.sql = @"update [Info_Category] set [cateNo]=@cateNo,[parentNo]=@parentNo where [cateId]=@cateId";

                    this.db.Batch(this.sql, paramList);
                }
            }

            this.sql = @"update [Info_Category] set [isLeaf]=0 where [cateNo]=@parentNo;update [Info_Category] set [cityId]=@cityId,[cateName]=@cateName,[cateNo]=@cateNo,[parentNo]=@parentNo where [cateId]=@cateId";

            this.param = new Dictionary<string, object>();
            this.param.Add("cateNo", content["cateNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("cityId", content["cityId"]);
            this.param.Add("cateName", content["cateName"]);
            this.param.Add("cateId", content["cateId"]);

            return this.db.Update(this.sql, this.param);
        }
    }
}
