using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Users;

namespace WebLogic.Service.Users
{
    public class MemberLogic
    {
        private MemberDao dao = null;

        public MemberLogic()
        {
            this.dao = new MemberDao();
        }

        public Dictionary<string, object> GetOne(long memberId)
        {
            return this.dao.GetOne(memberId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            return this.dao.GetList(msg, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, string msg, int locationId)
        {
            return this.dao.GetPage(pageSize, pageNo, msg, locationId);
        }

        public bool Delete(int userId, int memberId)
        {
            return this.dao.Delete(userId, memberId);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }
    }
}
