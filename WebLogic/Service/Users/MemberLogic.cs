using System;
using System.Collections.Generic;
using System.Text;
using Glibs.Sql;
using Glibs.Util;
using WebDao.Dao.System;
using WebDao.Dao.Users;

namespace WebLogic.Service.Users
{
    public class MemberLogic
    {
        private MemberDao dao = null;
        private UserDao udao = null;

        public MemberLogic()
        {
            this.dao = new MemberDao();
            this.udao = new UserDao();
        }

        public Dictionary<string, object> GetOne(long memberId)
        {
            return this.dao.GetOne(memberId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            return this.dao.GetList(msg, locationId);
        }

        public string GetTree(string msg, int locationId)
        {
            List<Dictionary<string, object>> list = this.dao.GetList(msg, locationId);

            if (list != null && list.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (Dictionary<string, object> item in list)
                {
                    s.Append(",{\"id\":\"");
                    s.Append(item["memberId"].ToString());
                    s.Append("\",\"text\":\"");
                    s.Append(item["fullName"].ToString());
                    s.Append("\"}");
                }

                return "[" + s.ToString().Substring(1) + "]";
            }
            else
            {
                return "[]";
            }
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int locationId)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, msg, locationId);

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

        public string GetPageJson(int pageSize, int pageNo, string msg, int locationId)
        {
            return this.GetPage(pageSize, pageNo, msg, locationId).PageJSON;
        }

        public bool Delete(long memberId)
        {
            return this.dao.Delete(memberId);
        }

        public long Insert(Dictionary<string, object> content)
        {
            content.Add("md5Pwd", Cryption.OneWayEncryption("1234", EncryptionFormat.MD5));
            content.Add("userPwd", Cryption.GetPassword(Cryption.OneWayEncryption("1234", EncryptionFormat.MD5)));
            content.Add("userType", "M");

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
