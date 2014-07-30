using System;
using System.Collections.Generic;
using WebDao.Dao.System;
using System.Text;

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

        public String GetTree()
        {
            List<Dictionary<string, object>> list = this.dao.GetList("");

            if (list != null && list.Count > 1)
            {
                Dictionary<string, object> temp = null;
                StringBuilder str = new StringBuilder();

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];

                    str.Append(",{");
                    str.Append("\"id\":\"");
                    str.Append(temp["roleId"].ToString());
                    str.Append("\",");
                    str.Append("\"text\":\"");
                    str.Append(temp["roleName"].ToString());
                    str.Append("\"}");
                }

                return "[" + str.ToString().Substring(1) + "]";
            }
            else
            {
                return "[]";
            }
        }
    }
}
