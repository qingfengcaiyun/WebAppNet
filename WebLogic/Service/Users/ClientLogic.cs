using System;
using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Users;

namespace WebLogic.Service.Users
{
    public class ClientLogic
    {
        private ClientDao dao = null;

        public ClientLogic()
        {
            this.dao = new ClientDao();
        }

        public Dictionary<string, object> GetOne(int clientId)
        {
            return this.dao.GetOne(clientId);
        }

        public bool Delete(int userId, int clientId)
        {
            return this.dao.Delete(userId, clientId);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public List<Dictionary<string, object>> GetList(String msg, int locationId)
        {
            return this.dao.GetList(msg, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, String msg, int locationId)
        {
            return this.dao.GetPage(pageSize, pageNo, msg, locationId);
        }
    }
}
