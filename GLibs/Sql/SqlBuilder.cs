using System.Collections.Generic;
using System.Text;

namespace Glibs.Sql
{
    public class SqlBuilder
    {
        private List<SqlTable> sqlTable;
        private List<SqlField> sqlFields;
        private SqlField sqlTagField;
        private List<SqlWhere> sqlWhere;
        private List<SqlOrderBy> sqlOrderBy;

        public SqlBuilder() { }

        private string GetTable()
        {
            if (sqlTable != null && sqlTable.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (SqlTable t in sqlTable)
                {
                    if (!string.IsNullOrEmpty(s.ToString().Trim()))
                    {
                        s.Append(",");
                    }

                    s.Append(t.GetString());
                }

                return s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetFields()
        {
            StringBuilder f = new StringBuilder();

            foreach (SqlField t in sqlFields)
            {
                if (!string.IsNullOrEmpty(f.ToString().Trim()))
                {
                    f.Append(",");
                }

                f.Append(t.GetString());
            }

            return f.ToString();
        }

        private string GetFields(bool isUpdate)
        {
            if (isUpdate)
            {
                StringBuilder f = new StringBuilder();

                foreach (SqlField t in sqlFields)
                {
                    if (!string.IsNullOrEmpty(f.ToString().Trim()))
                    {
                        f.Append(",");
                    }

                    f.Append(t.GetString());
                    f.Append("=@");
                    f.Append(t.FieldName);
                }

                return f.ToString();
            }
            else
            {
                StringBuilder f = new StringBuilder();
                StringBuilder v = new StringBuilder();

                foreach (SqlField t in sqlFields)
                {
                    if (!string.IsNullOrEmpty(f.ToString().Trim()))
                    {
                        f.Append(",");
                    }

                    if (!string.IsNullOrEmpty(v.ToString().Trim()))
                    {
                        v.Append(",");
                    }

                    f.Append(t.GetString());

                    v.Append("@");
                    v.Append(t.FieldName);
                }

                return " (" + f.ToString() + ")values(" + v.ToString() + ")";
            }
        }

        private string GetWhere()
        {
            if (sqlWhere != null && sqlWhere.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (SqlWhere sw in sqlWhere)
                {
                    s.Append(sw.GetString());
                }

                return "where" + s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetOrderBy()
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
                    s.Append(sob.GetString());
                }

                return " order by " + s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string SqlCount()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select count(");
            str.Append(this.sqlTagField.GetString());
            str.Append(") from ");
            str.Append(this.GetTable());
            str.Append(this.GetWhere());
            str.Append(";");

            return str.ToString();
        }

        public string SqlSelect()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select ");
            str.Append(this.GetFields());
            str.Append(" from ");
            str.Append(this.GetTable());
            str.Append(" ");
            str.Append(this.GetWhere());

            if (sqlOrderBy != null && sqlOrderBy.Count > 0)
            {
                str.Append(this.GetOrderBy());
            }

            str.Append(";");

            return str.ToString();
        }

        public string SqlInsert()
        {
            StringBuilder str = new StringBuilder();

            str.Append("insert into ");
            str.Append(this.GetTable());
            str.Append(this.GetFields(false));
            str.Append(";");

            return str.ToString();
        }

        public string SqlUpdate()
        {
            StringBuilder str = new StringBuilder();

            str.Append("update ");
            str.Append(GetTable());
            str.Append(" set ");
            str.Append(this.GetFields(true));
            str.Append(" ");
            str.Append(this.GetWhere());
            str.Append(";");

            return str.ToString();
        }

        public string SqlDelete()
        {
            StringBuilder str = new StringBuilder();

            str.Append("delete from ");
            str.Append(this.GetTable());
            str.Append(" ");
            str.Append(this.GetWhere());
            str.Append(";");

            return str.ToString();
        }

        public string SqlPage()
        {
            StringBuilder f = new StringBuilder();

            foreach (SqlField t in sqlFields)
            {
                if (!string.IsNullOrEmpty(f.ToString().Trim()))
                {
                    f.Append(",");
                }

                f.Append(t.GetString());
            }

            StringBuilder str = new StringBuilder();

            str.Append("select top @pageSize ");
            str.Append(this.GetFields());
            str.Append(" from ");
            str.Append(GetTable());
            str.Append(" ");
            str.Append(GetWhere());

            str.Append(" and ");
            str.Append(this.sqlTagField.GetString());
            str.Append(" not in (select top @startIndex ");
            str.Append(this.sqlTagField.GetString());
            str.Append(" from ");
            str.Append(GetTable());
            str.Append(" ");
            str.Append(GetWhere());
            str.Append(" ");
            if (sqlOrderBy != null && sqlOrderBy.Count > 0)
            {
                str.Append(GetOrderBy());
            }
            str.Append(") ");

            if (sqlOrderBy != null && sqlOrderBy.Count > 0)
            {
                str.Append(GetOrderBy());
            }

            str.Append(";");

            return str.ToString();
        }

        public List<SqlTable> SqlTable
        {
            get { return sqlTable; }
            set { sqlTable = value; }
        }

        public List<SqlField> SqlFields
        {
            get { return sqlFields; }
            set { sqlFields = value; }
        }

        public SqlField SqlTagField
        {
            get { return sqlTagField; }
            set { sqlTagField = value; }
        }

        public List<SqlWhere> SqlWhere
        {
            get { return sqlWhere; }
            set { sqlWhere = value; }
        }

        public List<SqlOrderBy> SqlOrderBy
        {
            get { return sqlOrderBy; }
            set { sqlOrderBy = value; }
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

        private string rightValue;

        public SqlWhere()
        {

        }

        public SqlWhere(string relation, string leftField, string equalStr, string rightField)
        {
            this.relation = relation;
            this.leftField = leftField;
            this.equalStr = equalStr;
            this.rightField = rightField;
        }

        public SqlWhere(string relation, string leftTable, string leftField, string equalStr, string rightValue)
        {
            this.relation = relation;
            this.leftTable = leftTable;
            this.leftField = leftField;
            this.equalStr = equalStr;
            this.rightValue = rightValue;
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

        public string GetString()
        {
            StringBuilder s = new StringBuilder();

            s.Append(" ");

            if (!string.IsNullOrEmpty(this.relation))
            {
                s.Append(this.relation);
                s.Append(" ");
            }

            if (!string.IsNullOrEmpty(this.leftTable))
            {
                s.Append(this.leftTable);
                s.Append(".");
            }

            s.Append("[");
            s.Append(this.leftField);
            s.Append("]");
            s.Append(this.equalStr);

            if (!string.IsNullOrEmpty(this.rightTable))
            {
                s.Append(this.rightTable);
                s.Append(".");
            }

            if (string.IsNullOrEmpty(this.rightValue))
            {
                s.Append("[");
                s.Append(this.rightField);
                s.Append("]");
            }
            else
            {
                s.Append(this.rightValue);
            }

            return s.ToString();
        }

        public string Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        public string LeftTable
        {
            get { return leftTable; }
            set { leftTable = value; }
        }

        public string LeftField
        {
            get { return leftField; }
            set { leftField = value; }
        }

        public string EqualStr
        {
            get { return equalStr; }
            set { equalStr = value; }
        }

        public string RightTable
        {
            get { return rightTable; }
            set { rightTable = value; }
        }

        public string RightField
        {
            get { return rightField; }
            set { rightField = value; }
        }
    }

    public class SqlOrderBy
    {
        private string shortName;
        private string fieldName;
        private bool isAsc;

        public SqlOrderBy() { }

        public SqlOrderBy(string fieldName, bool isAsc)
        {
            this.fieldName = fieldName;
            this.isAsc = isAsc;
        }

        public SqlOrderBy(string shortName, string fieldName, bool isAsc)
        {
            this.shortName = shortName;
            this.fieldName = fieldName;
            this.isAsc = isAsc;
        }

        public string GetString()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrEmpty(this.shortName))
            {
                s.Append(this.shortName);
                s.Append(".");
            }

            s.Append("[");
            s.Append(this.fieldName);
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

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public bool IsAsc
        {
            get { return isAsc; }
            set { isAsc = value; }
        }
    }

    public class SqlTable
    {
        private string shortName;
        private string tableName;

        public SqlTable() { }

        public SqlTable(string tableName)
        {
            this.tableName = tableName;
        }

        public SqlTable(string tableName, string shortName)
        {
            this.shortName = shortName;
            this.tableName = tableName;
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string GetString()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrEmpty(this.shortName))
            {
                s.Append(this.shortName);
                s.Append(".");
            }

            s.Append("[");
            s.Append(this.tableName);
            s.Append("]");

            return s.ToString();
        }
    }

    public class SqlField
    {
        private string shortName;
        private string fieldName;

        public SqlField() { }

        public SqlField(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public SqlField(string shortName, string fieldName)
        {
            this.shortName = shortName;
            this.fieldName = fieldName;
        }

        public string GetString()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrEmpty(this.shortName))
            {
                s.Append(this.shortName);
                s.Append(".");
            }

            s.Append("[");
            s.Append(this.fieldName);
            s.Append("]");

            return s.ToString();
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }
    }
}
