using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;
using System;

namespace WebDao.Dao.System
{

    public class FunctionDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public FunctionDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(int funcId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcId");
            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("parentNo");
            this.s.AddField("funcUrl");
            this.s.AddField("isLeaf");
            this.s.AddField("isDeleted");

            this.s.AddWhere("", "", "funcId", "=", "@funcId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("funcId", funcId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetListByUserId(string parentNo, int userId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_UserRole");

            this.s.AddField("roleId");

            this.s.AddWhere(string.Empty, string.Empty, "userId", "=", "@userId");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_RoleFunc");

            this.s.AddField("funcId");

            this.s.AddWhere(string.Empty, string.Empty, "roleId", "in", "(" + this.sql + ")");

            this.sql = this.s.SqlSelect();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcId");
            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("parentNo");
            this.s.AddField("funcUrl");
            this.s.AddField("isLeaf");
            this.s.AddField("isDeleted");

            this.s.AddWhere(string.Empty, string.Empty, "isDeleted", "=", "0");
            this.s.AddWhere("and", string.Empty, "parentNo", "like", "@parentNo+'%'");
            this.s.AddWhere("and", string.Empty, "funcId", "in", "(" + this.sql + ")");

            this.s.AddOrderBy("funcNo", true);

            this.sql = this.s.SqlSelect();

            //this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [isDeleted]=0 and [parentNo] like @parentNo+'%' and [funcId] in (select [funcId] from [Sys_RoleFunc] where [roleId] in (select [roleId] from [Sys_UserRole] where [userId]=@userId)) order by [funcNo] asc";

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            this.param.Add("userId", userId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcId");
            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("parentNo");
            this.s.AddField("funcUrl");
            this.s.AddField("isLeaf");
            this.s.AddField("isDeleted");

            this.s.AddWhere(string.Empty, string.Empty, "isDeleted", "=", "0");
            this.s.AddWhere("and", string.Empty, "parentNo", "like", "@parentNo+'%'");

            this.s.AddOrderBy("funcNo", true);

            this.sql = this.s.SqlSelect();

            //this.sql = @"select [funcId],[funcName],[funcNo],[parentNo],[funcUrl],[isLeaf],[isDeleted] from [Sys_Function] where [parentNo] like @parentNo+'%' and [isDeleted]=0 order by [funcNo] asc";

            if (!RegexDo.IsNumber(parentNo))
            {
                parentNo = "0";
            }

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", parentNo);
            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool Delete(string funcId)
        {
            this.s = new SqlBuilder();
            this.s.AddTable("Sys_Function");
            this.s.AddWhere("", "", "funcId", "=", "@funcId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("funcId", funcId);

            return this.db.Update(this.sql, this.param);
        }

        public long Insert(Dictionary<string, object> content)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "processNo", "=", "@processNo");

            this.sql = this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("processNo", content["parentNo"]);
            this.param.Add("isLeaf", 0);

            this.db.Update(this.sql, this.param);

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("parentNo");
            this.s.AddField("isLeaf");
            this.s.AddField("funcUrl");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("funcName", content["funcName"]);
            this.param.Add("funcNo", content["funcNo"]);
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("isLeaf", 1);
            this.param.Add("funcUrl", content["funcUrl"]);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Update(Dictionary<string, object> content)
        {
            Dictionary<string, object> func = this.GetOne(Int32.Parse(content["funcId"].ToString()));

            if (!func["funcNo"].ToString().StartsWith(content["parentNo"].ToString()))
            {
                List<Dictionary<string, object>> list = this.GetList(func["funcNo"].ToString());

                if (list != null && list.Count > 0)
                {
                    List<Dictionary<string, object>> paramList = new List<Dictionary<string, object>>();
                    Dictionary<string, object> item = null;

                    string parentNo = content["funcNo"].ToString();

                    for (int i = 0, j = list.Count; i < j; i++)
                    {
                        item = list[i];
                        this.param = new Dictionary<string, object>();

                        this.param.Add("funcNo", parentNo + item["funcNo"].ToString().Substring(func["funcNo"].ToString().Length));
                        this.param.Add("parentNo", parentNo + item["parentNo"].ToString().Substring(func["funcNo"].ToString().Length));
                        this.param.Add("funcId", Int32.Parse(item["funcId"].ToString()));

                        paramList.Add(this.param);
                    }

                    this.s = new SqlBuilder();

                    this.s.AddTable("Sys_Function");

                    this.s.AddField("funcName");
                    this.s.AddField("funcNo");
                    this.s.AddField("parentNo");

                    this.s.AddWhere("", "", "funcId", "=", "@funcId");

                    this.sql = this.s.SqlUpdate();
                    this.db.Batch(this.sql, paramList);
                }
            }

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("isLeaf");

            this.s.AddWhere("", "", "funcNo", "=", "@funcNo");

            this.sql = this.s.SqlUpdate();

            this.s = new SqlBuilder();

            this.s.AddTable("Sys_Function");

            this.s.AddField("funcName");
            this.s.AddField("funcNo");
            this.s.AddField("parentNo");

            this.s.AddWhere("", "", "funcId", "=", "@funcId");

            this.sql = this.sql + ";" + this.s.SqlUpdate();

            this.param = new Dictionary<string, object>();
            this.param.Add("parentNo", content["parentNo"]);
            this.param.Add("funcName", content["funcName"]);
            this.param.Add("funcNo", content["funcNo"]);
            this.param.Add("funcId", content["funcId"]);
            this.param.Add("isLeaf", 0);

            return this.db.Update(this.sql, this.param);
        }
    }
}
