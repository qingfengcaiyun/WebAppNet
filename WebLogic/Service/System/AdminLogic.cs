using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDao.Dao.System;
using Glibs.Sql;

namespace WebLogic.Service.System
{
    public class AdminLogic
    {
        private AdminDao dao;

        public AdminLogic()
        {
            this.dao = new AdminDao();
        }

        public Dictionary<string, object> GetOne(int userId)
        {
            return this.dao.GetOne(userId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string cityId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, cityId, msg);
        }

        public string GetPageJson(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, cityId, msg);
            return pr.PageJSON;
        }
    }
}
