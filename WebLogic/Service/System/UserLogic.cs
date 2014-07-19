using System;
using System.Collections.Generic;
using System.Web;
using Glibs.Util;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class UserLogic
    {
        private UserDao uDao = null;
        private AdminDao aDao = null;

        public UserLogic()
        {
            this.aDao = new AdminDao();
            this.uDao = new UserDao();
        }

        public bool Login(string userName, string userPwd)
        {
            Dictionary<string, object> user = this.uDao.GetOne(userName, Cryption.GetPassword(userPwd));

            if (user != null && user.Count > 0)
            {
                if (HttpContext.Current.Session["cUser"] != null)
                {
                    HttpContext.Current.Session.Remove("cUser");
                }
                HttpContext.Current.Session.Add("cUser", user);

                this.uDao.SetLastlogin(Int32.Parse(user["userId"].ToString()));

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePwd(string md5Pwd)
        {
            return this.uDao.UpdateUserPwd(Cryption.GetPassword(md5Pwd), md5Pwd, Int32.Parse(((Dictionary<string, object>)HttpContext.Current.Session["cUser"])["userId"].ToString()));
        }

        public bool UpdatePwds()
        {
            return this.uDao.UpdateUserPwds();
        }

        public Dictionary<string, object> GetAdminByUserId(int userId)
        {
            return this.aDao.GetOne(userId);
        }

        public bool SaveAdmin(Dictionary<string, object> content)
        {
            return this.aDao.Update(content);
        }
    }
}
