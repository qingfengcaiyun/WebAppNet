using System.Collections.Generic;
using System.Text;
using WebDao.Dao.Renovation;

namespace WebLogic.Service.Renovation
{
    public class ParameterLogic
    {
        private ParameterDao dao = null;

        public ParameterLogic()
        {
            this.dao = new ParameterDao();
        }

        public Dictionary<string, object> GetOne(int paramId)
        {
            return this.dao.GetOne(paramId);
        }

        public List<Dictionary<string, object>> GetList(string paramKey)
        {
            return this.dao.GetList(paramKey);
        }

        public List<Dictionary<string, object>> GetParamTypeList()
        {
            return this.dao.GetParamTypeList();
        }

        public string GetParamTypeJson()
        {
            List<Dictionary<string, object>> list = this.dao.GetParamTypeList();

            if (list != null && list.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (Dictionary<string, object> item in list)
                {
                    s.Append(",{\"id\":\"");
                    s.Append(item["paramKey"].ToString());
                    s.Append("\",\"text\":\"");
                    s.Append(item["paramName"].ToString());
                    s.Append("\"}");
                }

                return s.Length > 0 ? "[" + s.ToString().Substring(1) + "]" : "[]";
            }
            else
            {
                return "[]";
            }
        }

        public bool Delete(int paramId)
        {
            return this.dao.Delete(paramId);
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
