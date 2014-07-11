using System;
using System.Collections.Generic;
using System.Text;
using WedDao.Dao.System;

namespace WebLogic.Service.System
{
    public class LocationLogic
    {
        private LocationDao dao = null;

        public LocationLogic()
        {
            this.dao = new LocationDao();
        }

        public Dictionary<string, object> GetOne(int locationId) {
            return this.dao.GetOne(locationId);
        }

        public string GetSubIdArray(int nodeId)
        {
            List<Dictionary<string, object>> list;
            StringBuilder s = new StringBuilder();

            if (nodeId > 0)
            {
                list = this.dao.GetList(this.dao.GetOne(nodeId)["levelNo"].ToString());
            }
            else
            {
                list = this.dao.GetList("0");
            }

            s.Append(nodeId);

            if (list.Count > 0)
            {
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    s.Append(",");
                    s.Append(list[i]["locationId"].ToString());
                }
            }

            return s.ToString();
        }

        public string GetParentIdString(int nodeId)
        {
            Dictionary<string, object> node = this.dao.GetOne(nodeId);
            StringBuilder s = new StringBuilder();

            s.Append(node["locationId"].ToString());
            if (string.CompareOrdinal(node["parentNo"].ToString(), "0") != 0)
            {
                s.Append(",");
                s.Append(GetParentIdString(Int32.Parse(node["locationId"].ToString())));
            }
            return s.ToString();
        }
    }
}
