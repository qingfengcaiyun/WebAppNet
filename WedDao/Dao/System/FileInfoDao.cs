using System;
using System.Collections.Generic;
using Glibs.Sql;

namespace WebDao.Dao.System
{
    public class FileInfoDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;
        private SqlBuilder s = null;

        public FileInfoDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public Dictionary<string, object> GetOne(Int64 fileId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_FileInfo");

            this.s.AddField("fileName");
            this.s.AddField("extName");
            this.s.AddField("fileType");
            this.s.AddField("filePath");
            this.s.AddField("uploadTime");

            this.s.AddWhere(string.Empty, string.Empty, "fileId", "=", "@fileId");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("fileId", fileId);

            return this.db.GetDataRow(this.sql, this.param);
        }

        public object GetFileId(string filePath)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_FileInfo");

            this.s.AddField("fileId");

            this.s.AddWhere(string.Empty, string.Empty, "filePath", "=", "@filePath");

            this.sql = this.s.SqlSelect();

            this.param = new Dictionary<string, object>();
            this.param.Add("filePath", filePath);

            return this.db.GetDataValue(this.sql, this.param);
        }

        public Int64 Insert(string fileName, string extName, string filePath, string fileType, DateTime uploadTime)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_FileInfo");

            this.s.AddField("fileName");
            this.s.AddField("extName");
            this.s.AddField("fileType");
            this.s.AddField("filePath");
            this.s.AddField("uploadTime");

            this.sql = this.s.SqlInsert();

            this.param = new Dictionary<string, object>();
            this.param.Add("fileName", fileName);
            this.param.Add("extName", extName);
            this.param.Add("fileType", fileType);
            this.param.Add("filePath", filePath);
            this.param.Add("uploadTime", uploadTime);

            return this.db.Insert(this.sql, this.param);
        }

        public bool Delete(Int64 fileId)
        {
            this.s = new SqlBuilder();

            this.s.AddTable("Sys_FileInfo");

            this.s.AddWhere(string.Empty, string.Empty, "fileId", "=", "@fileId");

            this.sql = this.s.SqlDelete();

            this.param = new Dictionary<string, object>();
            this.param.Add("fileId", fileId);

            return this.db.Update(this.sql, this.param);
        }
    }
}
