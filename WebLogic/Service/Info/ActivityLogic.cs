using System.Collections.Generic;
using Glibs.Sql;
using WedDao.Dao.Info;

namespace WebLogic.Service.Info
{
    public class ActivityLogic
    {
        private ActivityDao dao = null;

        public ActivityLogic()
        {
            this.dao = new ActivityDao();
        }

        public Dictionary<string, object> getOne(int actId)
        {
            return this.dao.GetOne(actId);
        }

        public string GetPageJson(int pageNo, int pageSize, int locationId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, locationId, msg);
            return pr.PageJSON;
        }

        public PageRecords GetPage(int pageNo, int pageSize, int locationId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, locationId, msg);
        }

        public bool Delete(int actId)
        {
            return this.dao.Delete(actId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }
    }
}
