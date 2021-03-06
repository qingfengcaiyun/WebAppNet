﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Glibs.Util;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class FileInfoLogic
    {
        private FileInfoDao dao;

        public FileInfoLogic()
        {
            this.dao = new FileInfoDao();
        }

        public Int64 Insert(string fileName, string extName, string filePath, string fileType, DateTime uploadTime)
        {
            return this.dao.Insert(fileName, extName, filePath, fileType, uploadTime);
        }

        public bool Delete(Int64 fileId)
        {
            Dictionary<string, object> f = this.dao.GetOne(fileId);

            if (f != null && f.Count > 0)
            {
                string filePath = WebPageCore.GetMapPath(f["filePath"].ToString() + f["fileName"].ToString() + "." + f["extName"].ToString());

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                this.dao.Delete(fileId);

                return File.Exists(filePath);
            }
            else
            {
                return true;
            }
        }

        public string GetFileIds(string[] filePaths)
        {
            StringBuilder s = new StringBuilder();

            if (filePaths != null && filePaths.Length > 0)
            {
                for (int i = 0, j = filePaths.Length; i < j; i++)
                {
                    s.Append(",");
                    s.Append(this.dao.GetFileId(filePaths[i]));
                }
            }

            return s.ToString().Substring(1);
        }

        public void ClearFiles()
        {

        }
    }
}
