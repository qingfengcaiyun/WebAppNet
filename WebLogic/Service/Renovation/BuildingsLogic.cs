using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Renovation;

namespace WebLogic.Service.Renovation
{
    public class BuildingsLogic
    {
        private BuildingsDao dao;

        public BuildingsLogic()
        {
            this.dao = new BuildingsDao();
        }

        public Dictionary<string, object> GetOne(long buildingId)
        {
            return this.dao.GetOne(buildingId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            return this.dao.GetList(msg, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, locationId, msg);
        }

        public string GetPageJson(int pageSize, int pageNo, int locationId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, locationId, msg);
            return pr.PageJSON;
        }

        public bool Delete(long buildingsId)
        {
            return this.dao.Delete(buildingsId);
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
