using System;
using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Users;

namespace WebLogic.Service.Users
{
    public class DesignerLogic
    {
        private DesignerDao dao = null;

        public DesignerLogic()
        {
            this.dao = new DesignerDao();
        }

        public Dictionary<string, object> GetOne(int designerId)
        {
            return this.dao.GetOne(designerId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int memberId, int locationId)
        {
            return this.dao.GetList(msg, memberId, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int memberId, int locationId)
        {
            return this.dao.GetPage(pageSize, pageNo, msg, memberId, locationId);
        }

        public bool Delete(int userId, int designerId)
        {
            return this.dao.Delete(userId, designerId);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }
    }
}
