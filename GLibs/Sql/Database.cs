using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Glibs.Sql
{
    public class Database
    {
        private SqlConnection conn;
        private SqlCommand cmd;

        public Database(string connstr)
        {
            this.conn = new SqlConnection(connstr);
        }

        // 打开数据库
        private void OpenDB()
        {
            if (string.CompareOrdinal(this.conn.State.ToString(), "Closed") == 0)
            {
                this.conn.Open();
            }
        }

        // 关闭数据库
        private void CloseDB()
        {
            if (string.CompareOrdinal(this.conn.State.ToString(), "Open") == 0)
            {
                this.conn.Close();
            }
        }

        private Dictionary<string, object> BuildDataRow(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();

                DataRow dr = dt.Rows[0];

                for (int i = 0, j = dt.Columns.Count; i < j; i++)
                {
                    row.Add(dt.Columns[i].ColumnName, dr[dt.Columns[i]]);
                }

                return row;
            }
            else
            {
                return null;
            }
        }

        private List<Dictionary<string, object>> BuildDataTable(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;

                DataColumnCollection columns = dt.Columns;
                DataRow dr = null;

                for (int l = 0, k = dt.Rows.Count; l < k; l++)
                {
                    dr = dt.Rows[l];
                    row = new Dictionary<string, object>();
                    for (int i = 0, j = columns.Count; i < j; i++)
                    {
                        row.Add(columns[i].ColumnName, dr[columns[i]]);
                    }

                    list.Add(row);
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        private List<List<Dictionary<string, object>>> BuildDataSet(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                List<List<Dictionary<string, object>>> lists = new List<List<Dictionary<string, object>>>();

                for (int a = 0, b = ds.Tables.Count; a < b; a++)
                {
                    lists.Add(this.BuildDataTable(ds.Tables[a]));
                }

                return lists;
            }
            else
            {
                return null;
            }
        }

        public List<List<Dictionary<string, object>>> GetDataSet(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<List<Dictionary<string, object>>> lists = this.BuildDataSet(ds);
            this.CloseDB();
            return lists;
        }

        public List<Dictionary<string, object>> GetDataTable(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Dictionary<string, object>> list = this.BuildDataTable(ds.Tables[0]);
            this.CloseDB();
            return list;
        }

        public Dictionary<string, object> GetDataRow(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Dictionary<string, object> row = this.BuildDataRow(ds.Tables[0]);
            this.CloseDB();
            return row;
        }

        public object GetDataValue(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            object value = this.cmd.ExecuteScalar();
            this.CloseDB();
            return value;
        }

        public string GetDataValueString(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlDataReader sdr = this.cmd.ExecuteReader();
            StringBuilder str = new StringBuilder();

            int i = 0;
            while (sdr.NextResult())
            {
                if (i > 0)
                {
                    str.Append(",");
                }
                str.Append(sdr[0].ToString());

                i++;
            }

            this.CloseDB();
            return str.ToString();
        }

        public long Insert(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;
            this.cmd.Parameters.Clear();

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlTransaction tran = null;

            try
            {
                tran = this.conn.BeginTransaction();
                this.cmd.Transaction = tran;
                object o = this.cmd.ExecuteScalar();
                tran.Commit();

                return long.Parse(o.ToString());
            }
            catch
            {
                tran.Rollback();
                return 0;
            }
            finally
            {
                this.CloseDB();
            }
        }

        public bool Update(string sql, Dictionary<string, object> param)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;
            this.cmd.Parameters.Clear();

            if (param.Count > 0)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                }
            }

            SqlTransaction tran = null;

            try
            {
                tran = this.conn.BeginTransaction();
                this.cmd.Transaction = tran;
                this.cmd.ExecuteNonQuery();
                tran.Commit();

                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                this.CloseDB();
            }
        }

        public bool Batch(string sql, List<Dictionary<string, object>> paramList)
        {
            this.OpenDB();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = sql;
            SqlTransaction tran = null;

            try
            {
                tran = this.conn.BeginTransaction();
                this.cmd.Transaction = tran;
                if (paramList.Count > 0)
                {
                    foreach (Dictionary<string, object> param in paramList)
                    {
                        this.cmd.Parameters.Clear();
                        foreach (KeyValuePair<string, object> kv in param)
                        {
                            this.cmd.Parameters.Add(new SqlParameter(kv.Key, kv.Value));
                        }
                        this.cmd.ExecuteNonQuery();
                    }
                }

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                this.CloseDB();
            }
        }
    }
}
