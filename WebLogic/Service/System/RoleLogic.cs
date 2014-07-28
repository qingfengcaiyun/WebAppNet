using System;
using System.Collections.Generic;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class RoleLogic
    {
        private RoleDao dao;

        public RoleLogic()
        {
            this.dao = new RoleDao();
        }

        public Dictionary<string, object> GetOne(int roleId)
        {
            return this.dao.GetOne(roleId);
        }

        public List<Dictionary<string, object>> GetList(string msg)
        {
            return this.dao.GetList(msg);
        }

        public bool Delete(int roleId)
        {
            return this.dao.Delete(roleId);
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
