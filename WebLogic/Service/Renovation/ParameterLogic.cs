using System.Collections.Generic;
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
