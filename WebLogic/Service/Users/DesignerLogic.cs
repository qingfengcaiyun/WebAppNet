using System;
using System.Collections.Generic;
using Glibs.Sql;
using Glibs.Util;
using WebDao.Dao.System;
using WebDao.Dao.Users;

namespace WebLogic.Service.Users
{
    public class DesignerLogic
    {
        private DesignerDao dao = null;
        private UserDao udao = null;

        public DesignerLogic()
        {
            this.dao = new DesignerDao();
            this.udao = new UserDao();
        }

        public Dictionary<string, object> GetOne(long designerId)
        {
            return this.dao.GetOne(designerId);
        }

        public List<Dictionary<string, object>> GetList(string msg, long memberId, int locationId)
        {
            return this.dao.GetList(msg, memberId, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, long memberId, int locationId)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, msg, memberId, locationId);

            if (pr.PageResult != null && pr.PageResult.Count > 0)
            {
                for (int i = 0, j = pr.PageResult.Count; i < j; i++)
                {
                    if (Boolean.Parse(pr.PageResult[i]["isDeleted"].ToString()))
                    {
                        pr.PageResult[i].Add("delStr", "是");
                    }
                    else
                    {
                        pr.PageResult[i].Add("delStr", "否");
                    }
                }
            }

            return pr;
        }

        public string GetPageJson(int pageSize, int pageNo, string msg, long memberId, int locationId)
        {
            return this.GetPage(pageSize, pageNo, msg, memberId, locationId).PageJSON;
        }

        public bool Delete(long designerId)
        {
            return this.dao.Delete(designerId);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            content.Add("md5Pwd", Cryption.OneWayEncryption("1234", EncryptionFormat.MD5));
            content.Add("userPwd", Cryption.GetPassword(Cryption.OneWayEncryption("1234", EncryptionFormat.MD5)));
            content.Add("userType", "D");

            long userId = this.udao.Insert(content);
            content["userId"] = userId;
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }
    }
}
