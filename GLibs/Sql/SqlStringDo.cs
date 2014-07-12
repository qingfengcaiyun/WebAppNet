using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibs.Sql
{
    public class SqlStringDo
    {
        private Dictionary<string, string> sqlTable;
        private Dictionary<string, string> sqlFields;
        private KeyValuePair<string, string> countKey;
        private List<SqlWhere> sqlWhere;
        private List<SqlOrderBy> sqlOrderBy;
        //"select count() from table where ";

        private string GetTableString()
        {
            if (sqlTable != null && sqlTable.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (KeyValuePair<string, string> kv in sqlTable)
                {
                    if (!string.IsNullOrEmpty(s.ToString().Trim()))
                    {
                        s.Append(",");
                    }

                    s.Append("[");
                    s.Append(kv.Value);
                    s.Append("] as ");
                    s.Append(kv.Key);
                }

                return s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetWhereSql()
        {
            if (sqlWhere != null && sqlWhere.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (SqlWhere sw in sqlWhere)
                {
                    s.Append(sw.GetWhereItem());
                }

                return s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetOrderBySql()
        {
            if (sqlOrderBy != null && sqlOrderBy.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (SqlOrderBy sob in sqlOrderBy)
                {
                    if (!string.IsNullOrEmpty(s.ToString().Trim()))
                    {
                        s.Append(",");
                    }
                    s.Append(sob.GetOrderByItem());
                }

                return " order by " + s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetCountSql()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select count(");
            str.Append(countKey.Key);
            str.Append(".[");
            str.Append(countKey.Value);
            str.Append("]) from ");
            str.Append(GetTableString());
            str.Append(" where 1=1 ");
            str.Append(GetWhereSql());

            return str.ToString();
        }

        public string GetSelectSql()
        {
            StringBuilder f = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in sqlFields)
            {
                if (!string.IsNullOrEmpty(f.ToString().Trim()))
                {
                    f.Append(",");
                }

                f.Append(kv.Key);
                f.Append(".[");
                f.Append(kv.Value);
                f.Append("]");
            }

            StringBuilder str = new StringBuilder();

            str.Append("select ");
            str.Append(f.ToString());
            str.Append(" from ");
            str.Append(GetTableString());
            str.Append(" where 1=1 ");
            str.Append(GetWhereSql());

            if (sqlOrderBy != null && sqlOrderBy.Count > 0)
            {
                str.Append(GetOrderBySql());
            }

            return str.ToString();
        }

        public string GetInsertSql()
        {
            StringBuilder f = new StringBuilder();
            StringBuilder v = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in sqlFields)
            {
                if (!string.IsNullOrEmpty(f.ToString().Trim()))
                {
                    f.Append(",");
                }

                if (!string.IsNullOrEmpty(v.ToString().Trim()))
                {
                    v.Append(",");
                }

                f.Append(kv.Key);
                f.Append(".[");
                f.Append(kv.Value);
                f.Append("]");

                v.Append("@");
                v.Append(kv.Value);
            }

            StringBuilder str = new StringBuilder();

            str.Append("insert into ");
            str.Append(GetTableString());
            str.Append(" (");
            str.Append(f.ToString());
            str.Append(")values(");
            str.Append(v.ToString());
            str.Append(")");

            return str.ToString();
        }

        public string GetUpdateSql()
        {
            StringBuilder f = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in sqlFields)
            {
                if (!string.IsNullOrEmpty(f.ToString().Trim()))
                {
                    f.Append(",");
                }

                f.Append(kv.Key);
                f.Append(".[");
                f.Append(kv.Value);
                f.Append("]=@");
                f.Append(kv.Value);
            }

            StringBuilder str = new StringBuilder();

            str.Append("update ");
            str.Append(GetTableString());
            str.Append(" set ");
            str.Append(f.ToString());
            str.Append(" where 1=1 ");
            str.Append(GetWhereSql());

            return str.ToString();
        }
    }

    public class SqlWhere
    {
        private string relation;
        private string leftTable;
        private string leftField;
        private string equalStr;
        private string rightTable;
        private string rightField;

        public SqlWhere()
        {

        }

        public SqlWhere(string relation, string leftTable, string leftField, string equalStr, string rightTable, string rightField)
        {
            this.relation = relation;
            this.leftTable = leftTable;
            this.leftField = leftField;
            this.equalStr = equalStr;
            this.rightTable = rightTable;
            this.rightField = rightField;
        }

        public string GetWhereItem()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" ");
            s.Append(relation);
            s.Append(" ");
            s.Append(leftTable);
            s.Append(".[");
            s.Append(leftField);
            s.Append("] ");
            s.Append(equalStr);
            s.Append(" ");

            if (string.IsNullOrEmpty(rightTable))
            {
                s.Append(rightField);
            }
            else
            {
                s.Append(rightTable);
                s.Append(".[");
                s.Append(rightField);
                s.Append("]");
            }

            return s.ToString();
        }

        public string Relation
        {
            set { relation = value; }
        }

        public string LeftTable
        {
            set { leftTable = value; }
        }

        public string LeftField
        {
            set { leftField = value; }
        }

        public string EqualStr
        {
            set { equalStr = value; }
        }

        public string RightTable
        {
            set { rightTable = value; }
        }

        public string RightField
        {
            set { rightField = value; }
        }
    }

    public class SqlOrderBy
    {
        private string sqlTable;
        private string sqlField;
        private bool isAsc;

        public string GetOrderByItem()
        {
            StringBuilder s = new StringBuilder();

            s.Append(sqlTable);
            s.Append(".[");
            s.Append(sqlField);
            s.Append("] ");
            if (isAsc)
            {
                s.Append("asc");
            }
            else
            {
                s.Append("desc");
            }

            return s.ToString();
        }

        public string SqlTable
        {
            set { sqlTable = value; }
        }

        public string SqlField
        {
            set { sqlField = value; }
        }

        public bool IsAsc
        {
            set { isAsc = value; }
        }
    }
}
