using System.Collections.Generic;
using System.Text;
using WebDao.Dao.Renovation;
using System;

namespace WebLogic.Service.Renovation
{
    public class ProcessLogic
    {
        private ProcessDao dao = null;

        public ProcessLogic()
        {
            this.dao = new ProcessDao();
        }

        public Dictionary<string, object> GetOne(int processId)
        {
            return this.dao.GetOne(processId);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            return this.dao.GetList(parentNo);
        }

        public string GetTree(string parentNo)
        {
            List<Dictionary<string, object>> list = this.GetList(parentNo);

            if (list != null && list.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    s.Append(",{\"processNo\":");
                    s.Append(Int32.Parse(list[i]["processNo"].ToString()));
                    s.Append(",\"processName\":\"");
                    s.Append(list[i]["processName"].ToString());
                    s.Append("\",\"processId\":");
                    s.Append(list[i]["processId"].ToString());
                    s.Append(",\"parentNo\":");
                    s.Append(Int32.Parse(list[i]["parentNo"].ToString()));

                    if (string.CompareOrdinal(parentNo, list[i]["parentNo"].ToString()) != 0)
                    {
                        s.Append(",\"_parentId\":");
                        s.Append(Int32.Parse(list[i]["parentNo"].ToString()));
                    }

                    if (!Boolean.Parse(list[i]["isLeaf"].ToString()))
                    {
                        if (string.CompareOrdinal(parentNo, list[i]["parentNo"].ToString()) != 0)
                        {
                            s.Append(",\"state\": \"closed\"");
                        }
                    }

                    s.Append("}");
                }

                return "{\"total\":" + list.Count.ToString() + ",\"rows\":[" + s.ToString().Substring(1) + "]}";
            }
            else
            {
                return "{\"total\":0,\"rows\":[]}";
            }
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public bool Delete(string processNo)
        {
            return this.dao.Delete(processNo);
        }
    }
}
