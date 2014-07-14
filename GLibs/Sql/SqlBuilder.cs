using System.Collections.Generic;
using System.Text;

namespace Glibs.Sql
{
    public class SqlBuilder
    {
        private SqlTable sqlTable;
        private SqlField sqlFields;
        private SqlField sqlTagField;
        private SqlWhere sqlWhere;
        private SqlOrderBy sqlOrderBy;

        public SqlBuilder() { }

        public string SqlCount()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select count(");
            str.Append(this.sqlTagField.GetString());
            str.Append(") from ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());
            str.Append(";");

            return str.ToString();
        }

        public string SqlSelect()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select ");
            str.Append(this.sqlFields.GetString());
            str.Append(" from ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());

            if (sqlOrderBy != null)
            {
                str.Append(" ");
                str.Append(this.sqlOrderBy.GetString());
            }

            str.Append(";");

            return str.ToString();
        }

        public string SqlInsert()
        {
            StringBuilder str = new StringBuilder();

            str.Append("insert into ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlFields.GetString(false));
            str.Append(";");

            return str.ToString();
        }

        public string SqlUpdate()
        {
            StringBuilder str = new StringBuilder();

            str.Append("update ");
            str.Append(this.sqlTable.GetString());
            str.Append(" set ");
            str.Append(this.sqlFields.GetString(false));
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());
            str.Append(";");

            return str.ToString();
        }

        public string SqlDelete()
        {
            StringBuilder str = new StringBuilder();

            str.Append("delete from ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());
            str.Append(";");

            return str.ToString();
        }

        public string SqlPage()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select top @pageSize ");
            str.Append(this.sqlFields.GetString());
            str.Append(" from ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());

            str.Append(" and ");
            str.Append(this.sqlTagField.GetString());
            str.Append(" not in (select top @startIndex ");
            str.Append(this.sqlTagField.GetString());
            str.Append(" from ");
            str.Append(this.sqlTable.GetString());
            str.Append(" ");
            str.Append(this.sqlWhere.GetString());
            str.Append(" ");

            if (sqlOrderBy != null)
            {
                str.Append(this.sqlOrderBy.GetString());
            }
            str.Append(") ");

            if (sqlOrderBy != null)
            {
                str.Append(this.sqlOrderBy.GetString());
            }

            str.Append(";");

            return str.ToString();
        }

        public SqlTable SqlTable
        {
            get { return sqlTable; }
            set { sqlTable = value; }
        }

        public SqlField SqlFields
        {
            get { return sqlFields; }
            set { sqlFields = value; }
        }

        public SqlField SqlTagField
        {
            get { return sqlTagField; }
            set { sqlTagField = value; }
        }

        public SqlWhere SqlWhere
        {
            get { return sqlWhere; }
            set { sqlWhere = value; }
        }

        public SqlOrderBy SqlOrderBy
        {
            get { return sqlOrderBy; }
            set { sqlOrderBy = value; }
        }
    }

    public class SqlWhere
    {
        private List<string> relation;
        private List<string> leftTable;
        private List<string> leftField;
        private List<string> equalStr;
        private List<string> rightTable;
        private List<string> rightField;

        private List<string> rightValue;

        public SqlWhere()
        {
            this.relation = new List<string>();
            this.leftTable = new List<string>();
            this.leftField = new List<string>();
            this.equalStr = new List<string>();
            this.rightTable = new List<string>();
            this.rightField = new List<string>();
            this.rightValue = new List<string>();
        }

        public void Add(string relation, string leftField, string equalStr, string rightField)
        {
            this.relation.Add(relation);
            this.leftTable.Add(string.Empty);
            this.leftField.Add(leftField);
            this.equalStr.Add(equalStr);
            this.rightTable.Add(string.Empty);
            this.rightField.Add(rightField);
            this.rightValue.Add(string.Empty);
        }

        public void Add(string relation, string leftTable, string leftField, string equalStr, string rightValue)
        {
            this.relation.Add(relation);
            this.leftTable.Add(leftTable);
            this.leftField.Add(leftField);
            this.equalStr.Add(equalStr);
            this.rightTable.Add(string.Empty);
            this.rightField.Add(string.Empty);
            this.rightValue.Add(rightValue);
        }

        public void Add(string relation, string leftTable, string leftField, string equalStr, string rightTable, string rightField)
        {
            this.relation.Add(relation);
            this.leftTable.Add(leftTable);
            this.leftField.Add(leftField);
            this.equalStr.Add(equalStr);
            this.rightTable.Add(rightTable);
            this.rightField.Add(rightField);
            this.rightValue.Add(string.Empty);
        }

        public string GetString()
        {
            if (this.relation.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                for (int i = 0, j = this.relation.Count; i < j; i++)
                {
                    s.Append(" ");

                    if (!string.IsNullOrEmpty(this.relation[i]))
                    {
                        s.Append(this.relation[i]);
                        s.Append(" ");
                    }

                    if (!string.IsNullOrEmpty(this.leftTable[i]))
                    {
                        s.Append(this.leftTable[i]);
                        s.Append(".");
                    }

                    s.Append("[");
                    s.Append(this.leftField[i]);
                    s.Append("]");
                    s.Append(this.equalStr[i]);


                    if (!string.IsNullOrEmpty(this.rightTable[i]))
                    {
                        s.Append(this.rightTable[i]);
                        s.Append(".");
                    }

                    if (string.IsNullOrEmpty(this.rightValue[i]))
                    {
                        s.Append("[");
                        s.Append(this.rightField[i]);
                        s.Append("]");
                    }
                    else
                    {
                        s.Append(this.rightValue[i]);
                    }
                }

                return "where" + s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class SqlOrderBy
    {
        private List<string> shortName;
        private List<string> fieldName;
        private List<bool> isAsc;

        public SqlOrderBy()
        {
            this.shortName = new List<string>();
            this.fieldName = new List<string>();
            this.isAsc = new List<bool>();
        }

        public void Add(string fieldName, bool isAsc)
        {
            this.fieldName.Add(fieldName);
            this.isAsc.Add(isAsc);
        }

        public void Add(string shortName, string fieldName, bool isAsc)
        {
            this.shortName.Add(shortName);
            this.fieldName.Add(fieldName);
            this.isAsc.Add(isAsc);
        }

        public void Clear()
        {
            this.shortName = new List<string>();
            this.fieldName = new List<string>();
            this.isAsc = new List<bool>();
        }

        public string GetString()
        {
            if (this.fieldName.Count > 0 && this.fieldName.Count == this.isAsc.Count)
            {
                StringBuilder s = new StringBuilder();

                for (int i = 0, j = this.fieldName.Count; i < j; i++)
                {
                    s.Append(",");
                    if (this.shortName.Count == this.shortName.Count)
                    {
                        s.Append(this.shortName[i]);
                        s.Append(".");
                    }

                    s.Append("[");
                    s.Append(this.fieldName[i]);
                    s.Append("] ");

                    if (this.isAsc[i])
                    {
                        s.Append("asc");
                    }
                    else
                    {
                        s.Append("desc");
                    }
                }

                return "order by " + s.ToString().Substring(1);
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class SqlTable
    {
        private List<string> shortName;
        private List<string> tableName;

        public SqlTable()
        {
            this.shortName = new List<string>();
            this.tableName = new List<string>();
        }

        public void Add(string tableName)
        {
            this.tableName.Add(tableName);
        }

        public void Add(string tableName, string shortName)
        {
            this.tableName.Add(tableName);
            this.shortName.Add(shortName);
        }

        public void Clear()
        {
            this.shortName = new List<string>();
            this.tableName = new List<string>();
        }

        public string GetString()
        {
            StringBuilder s = new StringBuilder();

            for (int i = 0, j = this.tableName.Count; i < j; i++)
            {
                s.Append(",");

                if (this.shortName.Count == this.tableName.Count)
                {
                    s.Append(this.shortName[i]);
                    s.Append(".");
                }

                s.Append("[");
                s.Append(this.tableName[i]);
                s.Append("]");
            }

            return s.ToString().Substring(1);
        }
    }

    public class SqlField
    {
        private List<string> shortName;
        private List<string> fieldName;

        public SqlField()
        {
            this.shortName = new List<string>();
            this.fieldName = new List<string>();
        }

        public void Add(string fieldName)
        {
            this.fieldName.Add(fieldName);
        }

        public void Add(string shortName, string fieldName)
        {
            this.fieldName.Add(fieldName);
            this.shortName.Add(shortName);
        }

        public void Clear()
        {
            this.shortName = new List<string>();
            this.fieldName = new List<string>();
        }

        public string GetString()
        {
            if (this.fieldName.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                if (this.shortName.Count > 0)
                {
                    for (int i = 0, j = this.fieldName.Count; i < j; i++)
                    {
                        s.Append(",");
                        s.Append(this.shortName[i]);
                        s.Append(".[");
                        s.Append(this.fieldName[i]);
                        s.Append("]");
                    }
                }
                else
                {
                    foreach (string f in fieldName)
                    {
                        s.Append(",[");
                        s.Append(f);
                        s.Append("]");
                    }
                }

                return s.ToString().Substring(1);
            }
            else
            {
                return "*";
            }
        }

        public string GetString(bool isUpdate)
        {
            if (isUpdate)
            {
                StringBuilder f = new StringBuilder();

                foreach (string t in this.fieldName)
                {
                    if (!string.IsNullOrEmpty(f.ToString().Trim()))
                    {
                        f.Append(",");
                    }

                    f.Append("[");
                    f.Append(t);
                    f.Append("]=@");
                    f.Append(t);
                }

                return f.ToString();
            }
            else
            {
                StringBuilder f = new StringBuilder();
                StringBuilder v = new StringBuilder();

                foreach (string t in this.fieldName)
                {
                    if (!string.IsNullOrEmpty(f.ToString().Trim()))
                    {
                        f.Append(",");
                    }

                    if (!string.IsNullOrEmpty(v.ToString().Trim()))
                    {
                        v.Append(",");
                    }

                    f.Append("[");
                    f.Append(t);
                    f.Append("]");

                    v.Append("@");
                    v.Append(t);
                }

                return " (" + f.ToString() + ")values(" + v.ToString() + ")";
            }
        }
    }
}
