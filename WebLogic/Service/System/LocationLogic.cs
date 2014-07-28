using System;
using System.Collections.Generic;
using System.Text;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class LocationLogic
    {
        private LocationDao dao = null;

        public LocationLogic()
        {
            this.dao = new LocationDao();
        }

        public Dictionary<string, object> GetOne(int locationId)
        {
            return this.dao.GetOne(locationId);
        }

        public Dictionary<string, object> GetOne(string levelNo)
        {
            return this.dao.GetOne(levelNo);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            return this.dao.GetList(parentNo);
        }

        public string GetTreeGrid(string parentNo)
        {
            List<Dictionary<string, object>> list = this.dao.GetList(parentNo);

            Dictionary<string, List<Dictionary<string, object>>> tlist = new Dictionary<string, List<Dictionary<string, object>>>();

            if (list != null && list.Count > 0)
            {
                string keyName = "";

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    keyName = list[i]["parentNo"].ToString();

                    if (tlist.ContainsKey(keyName))
                    {
                        tlist[keyName].Add(list[i]);
                    }
                    else
                    {
                        tlist.Add(keyName, new List<Dictionary<string, object>>());
                        tlist[keyName].Add(list[i]);
                    }
                }
            }

            return "{\"total\":" + list.Count + ", \"rows\":[" + this.GetSubTreeGrid(tlist, parentNo) + "]}";
        }

        private string GetSubTreeGrid(Dictionary<string, List<Dictionary<string, object>>> tlist, string parentNo)
        {
            List<Dictionary<string, object>> list = tlist.ContainsKey(parentNo) ? tlist[parentNo] : null;

            if (list != null && list.Count > 0)
            {
                Dictionary<string, object> temp = null;
                string substr = "";

                StringBuilder str = new StringBuilder();

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];

                    str.Append(",{");
                    str.Append("\"locationId\":");
                    str.Append(temp["locationId"].ToString());
                    str.Append(",\"cnName\":\"");
                    str.Append(temp["cnName"].ToString());
                    str.Append("\",\"enName\":\"");
                    str.Append(temp["enName"].ToString());
                    str.Append("\",\"levelNo\":\"");
                    str.Append(temp["levelNo"].ToString());
                    str.Append("\",\"parentNo\":\"");
                    str.Append(temp["parentNo"].ToString());
                    str.Append("\",\"levelCnName\":\"");
                    str.Append(temp["levelCnName"].ToString());
                    str.Append("\",\"levelEnName\":\"");
                    str.Append(temp["levelEnName"].ToString());
                    str.Append("\"");

                    substr = this.GetSubTreeGrid(tlist, temp["levelNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append("}");
                    }
                    else
                    {
                        str.Append(",\"children\":[");
                        str.Append(substr);
                        str.Append("]}");
                    }
                }

                return str.ToString().Substring(1);
            }
            else
            {
                return "";
            }
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

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public bool Delete(string levelNo)
        {
            return this.dao.Delete(levelNo);
        }
    }
}
