using System.Text;

namespace Glibs.Sql
{
    public class SqlBuilder
    {
        private StringBuilder sqlTable;
        private StringBuilder sqlFields;
        private StringBuilder sqlTagField;
        private StringBuilder sqlWhere;
        private StringBuilder sqlOrderBy;
        private StringBuilder sqlIncrease;

        public SqlBuilder()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.sqlTable = new StringBuilder();
            this.sqlFields = new StringBuilder();
            this.sqlTagField = new StringBuilder();
            this.sqlWhere = new StringBuilder();
            this.sqlOrderBy = new StringBuilder();
            this.sqlIncrease = new StringBuilder();
        }

        public void AddTable(string tableName)
        {
            if (this.sqlTable.ToString().Length > 0)
            {
                this.sqlTable.Append(",");
            }

            this.sqlTable.Append("[");
            this.sqlTable.Append(tableName);
            this.sqlTable.Append("]");
        }

        public void AddTable(string tableName, string shortName)
        {
            if (this.sqlTable.ToString().Length > 0)
            {
                this.sqlTable.Append(",");
            }

            this.sqlTable.Append("[");
            this.sqlTable.Append(tableName);
            this.sqlTable.Append("] as ");
            this.sqlTable.Append(shortName);
        }

        public void ClearTable()
        {
            this.sqlTable = new StringBuilder();
        }

        public void AddOrderBy(string fieldName, bool isAsc)
        {
            if (this.sqlOrderBy.ToString().Length > 0)
            {
                this.sqlOrderBy.Append(",");
            }

            this.sqlOrderBy.Append("[");
            this.sqlOrderBy.Append(fieldName);
            this.sqlOrderBy.Append("] ");

            if (isAsc)
            {
                this.sqlOrderBy.Append("asc");
            }
            else
            {
                this.sqlOrderBy.Append("desc");
            }
        }

        public void AddOrderBy(string shortName, string fieldName, bool isAsc)
        {
            if (this.sqlOrderBy.ToString().Length > 0)
            {
                this.sqlOrderBy.Append(",");
            }

            this.sqlOrderBy.Append(shortName);
            this.sqlOrderBy.Append(".[");
            this.sqlOrderBy.Append(fieldName);
            this.sqlOrderBy.Append("] ");

            if (isAsc)
            {
                this.sqlOrderBy.Append("asc");
            }
            else
            {
                this.sqlOrderBy.Append("desc");
            }
        }

        public void ClearOrderBy()
        {
            this.sqlOrderBy = new StringBuilder();
        }

        public void AddWhere(string relation, string leftField, string equalStr, string rightField)
        {
            this.sqlWhere.Append(" ");

            if (!string.IsNullOrEmpty(relation))
            {
                this.sqlWhere.Append(relation);
                this.sqlWhere.Append(" ");
            }

            this.sqlWhere.Append("[");
            this.sqlWhere.Append(leftField);
            this.sqlWhere.Append("] ");
            this.sqlWhere.Append(equalStr);
            this.sqlWhere.Append(" [");
            this.sqlWhere.Append(rightField);
            this.sqlWhere.Append("] ");
        }

        public void AddWhere(string relation, string leftTable, string leftField, string equalStr, string rightValue)
        {
            this.sqlWhere.Append(" ");

            if (!string.IsNullOrEmpty(relation))
            {
                this.sqlWhere.Append(relation);
                this.sqlWhere.Append(" ");
            }

            if (!string.IsNullOrEmpty(leftTable))
            {
                this.sqlWhere.Append(leftTable);
                this.sqlWhere.Append(".");
            }

            this.sqlWhere.Append("[");
            this.sqlWhere.Append(leftField);
            this.sqlWhere.Append("] ");
            this.sqlWhere.Append(equalStr);
            this.sqlWhere.Append(" ");
            this.sqlWhere.Append(rightValue);
        }

        public void AddWhere(string relation, string leftTable, string leftField, string equalStr, string rightTable, string rightField)
        {
            this.sqlWhere.Append(" ");

            if (!string.IsNullOrEmpty(relation))
            {
                this.sqlWhere.Append(relation);
                this.sqlWhere.Append(" ");
            }

            this.sqlWhere.Append(leftTable);
            this.sqlWhere.Append(".[");
            this.sqlWhere.Append(leftField);
            this.sqlWhere.Append("] ");
            this.sqlWhere.Append(equalStr);
            this.sqlWhere.Append(" ");
            this.sqlWhere.Append(rightTable);
            this.sqlWhere.Append(".[");
            this.sqlWhere.Append(rightField);
            this.sqlWhere.Append("]");
        }

        public void ClearWhere()
        {
            this.sqlWhere = new StringBuilder();
        }

        public void AddField(string fieldName)
        {
            if (this.sqlFields.ToString().Length > 0)
            {
                this.sqlFields.Append(",");
            }
            this.sqlFields.Append("[");
            this.sqlFields.Append(fieldName);
            this.sqlFields.Append("]");
        }

        public void AddField(string shortName, string fieldName)
        {
            if (this.sqlFields.ToString().Length > 0)
            {
                this.sqlFields.Append(",");
            }

            this.sqlFields.Append(shortName);
            this.sqlFields.Append(".[");
            this.sqlFields.Append(fieldName);
            this.sqlFields.Append("]");
        }

        public void AddField(string shortName, string fieldName, string nickName)
        {
            if (this.sqlFields.ToString().Length > 0)
            {
                this.sqlFields.Append(",");
            }

            this.sqlFields.Append(shortName);
            this.sqlFields.Append(".[");
            this.sqlFields.Append(fieldName);
            this.sqlFields.Append("] as ");
            this.sqlFields.Append(nickName);
        }

        public void ClearField()
        {
            this.sqlFields = new StringBuilder();
        }

        public void AddIncrease(string fieldName, int num)
        {
            if (this.sqlIncrease.ToString().Length > 0)
            {
                this.sqlIncrease.Append(",");
            }
            this.sqlIncrease.Append("[");
            this.sqlIncrease.Append(fieldName);
            this.sqlIncrease.Append("]=[");
            this.sqlIncrease.Append(fieldName);
            this.sqlIncrease.Append("]");
            if (num > 0)
            {
                this.sqlIncrease.Append("+");
            }
            this.sqlIncrease.Append(num);
        }

        public void AddIncrease(string shortName, string fieldName, int num)
        {
            if (this.sqlIncrease.ToString().Length > 0)
            {
                this.sqlIncrease.Append(",");
            }

            this.sqlIncrease.Append(shortName);
            this.sqlIncrease.Append(".[");
            this.sqlIncrease.Append(fieldName);
            this.sqlIncrease.Append("]=");
            this.sqlIncrease.Append(shortName);
            this.sqlIncrease.Append(".[");
            this.sqlIncrease.Append(fieldName);
            this.sqlIncrease.Append("]");
            if (num > 0)
            {
                this.sqlIncrease.Append("+");
            }
            this.sqlIncrease.Append(num);
        }

        public void ClearIncrease()
        {
            this.sqlIncrease = new StringBuilder();
        }

        public void SetTagField(string fieldName)
        {
            this.sqlTagField = new StringBuilder();
            this.sqlTagField.Append("[");
            this.sqlTagField.Append(fieldName);
            this.sqlTagField.Append("]");
        }

        public void SetTagField(string shortName, string fieldName)
        {
            this.sqlTagField = new StringBuilder();
            this.sqlTagField.Append(shortName);
            this.sqlTagField.Append(".[");
            this.sqlTagField.Append(fieldName);
            this.sqlTagField.Append("]");
        }

        public void ClearTagField()
        {
            this.sqlTagField = new StringBuilder();
        }

        public string SqlCount()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select count(");
            str.Append(this.sqlTagField.ToString());
            str.Append(") from ");
            str.Append(this.sqlTable.ToString());

            if (this.sqlWhere.ToString().Length > 0)
            {
                str.Append(" where");
                str.Append(this.sqlWhere.ToString());
            }

            return str.ToString();
        }

        public string SqlSelect()
        {
            StringBuilder str = new StringBuilder();

            str.Append("select ");

            if (this.sqlFields.ToString().Length > 0)
                str.Append(this.sqlFields.ToString());
            else
                str.Append("*");

            str.Append(" from ");
            str.Append(this.sqlTable.ToString());

            if (this.sqlWhere.ToString().Length > 0)
            {
                str.Append(" where");
                str.Append(this.sqlWhere.ToString());
            }

            if (this.sqlOrderBy.ToString().Length > 0)
            {
                str.Append(" order by ");
                str.Append(this.sqlOrderBy.ToString());
            }

            return str.ToString();
        }

        public string SqlInsert()
        {
            StringBuilder str = new StringBuilder();

            str.Append("insert into ");
            str.Append(this.sqlTable.ToString());
            str.Append(" (");
            str.Append(this.sqlFields.ToString());
            str.Append(")values(");
            str.Append(this.sqlFields.ToString().Replace("[", "@").Replace("]", ""));
            str.Append(")");

            return str.ToString();
        }

        public string SqlUpdate()
        {
            string[] ss = this.sqlFields.ToString().Replace("[", "").Replace("]", "").Split(',');

            StringBuilder f = new StringBuilder();

            foreach (string s in ss)
            {
                f.Append(",[");
                f.Append(s);
                f.Append("]=@");
                f.Append(s);
            }

            StringBuilder str = new StringBuilder();

            str.Append("update ");
            str.Append(this.sqlTable.ToString());
            str.Append(" set ");
            str.Append(f.ToString().Substring(1));
            str.Append(" where");
            str.Append(this.sqlWhere.ToString());

            return str.ToString();
        }

        public string SqlIncrease()
        {
            StringBuilder str = new StringBuilder();

            str.Append("update ");
            str.Append(this.sqlTable.ToString());
            str.Append(" set ");
            str.Append(this.sqlIncrease.ToString());
            str.Append(" where");
            str.Append(this.sqlWhere.ToString());

            return str.ToString();
        }

        public string SqlDelete()
        {
            StringBuilder str = new StringBuilder();

            str.Append("delete from ");
            str.Append(this.sqlTable.ToString());
            str.Append(" where");
            str.Append(this.sqlWhere.ToString());

            return str.ToString();
        }

        public string SqlPage(int pageSize, int startIndex)
        {
            StringBuilder str = new StringBuilder();

            str.Append("select top ");
            str.Append(pageSize);
            str.Append(" ");
            str.Append(this.sqlFields.ToString());
            str.Append(" from ");
            str.Append(this.sqlTable.ToString());
            str.Append(" where");
            str.Append(this.sqlWhere.ToString());

            str.Append(" and ");
            str.Append(this.sqlTagField.ToString());
            str.Append(" not in (select top ");
            str.Append(startIndex);
            str.Append(" ");
            str.Append(this.sqlTagField.ToString());
            str.Append(" from ");
            str.Append(this.sqlTable.ToString());
            str.Append(" where");
            str.Append(this.sqlWhere.ToString());

            if (sqlOrderBy != null)
            {
                str.Append(" order by ");
                str.Append(this.sqlOrderBy.ToString());
            }
            str.Append(")");

            if (sqlOrderBy != null)
            {
                str.Append(" order by ");
                str.Append(this.sqlOrderBy.ToString());
            }

            return str.ToString();
        }
    }
}
