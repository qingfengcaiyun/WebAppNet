using System;
using System.Collections.Generic;
using System.Text;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class RoleFuncLogic
    {
        private RoleFuncDao dao = null;

        public RoleFuncLogic()
        {
            this.dao = new RoleFuncDao();
        }

        public string GetList(int roleId)
        {
            List<Dictionary<string, object>> list = this.dao.GetList(roleId);

            if (list != null && list.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (Dictionary<string, object> item in list)
                {
                    s.Append(",");
                    s.Append(item["funcId"].ToString());
                }

                return s.ToString().Substring(1);
            }
            else
            {
                return "";
            }
        }

        public bool SaveList(Int64[] funcIds, Int64 roleId)
        {
            return this.dao.SaveList(funcIds, roleId);
        }
    }
}
