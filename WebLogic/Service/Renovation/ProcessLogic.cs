using System.Collections.Generic;
using System.Text;
using WebDao.Dao.Renovation;

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
                    s.Append(",{\"processNo\":\"");
                    s.Append(list[i]["processNo"].ToString());
                    s.Append("\",\"processName\":\"");
                    s.Append(list[i]["processName"].ToString());
                    s.Append("\",\"processId\":\"");
                    s.Append(list[i]["processId"].ToString());
                    s.Append("\",\"_parentId\":");
                    s.Append(list[i]["parentNo"].ToString());
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

        public bool Delete(int processId)
        {
            return this.dao.Delete(processId);
        }
    }
}
