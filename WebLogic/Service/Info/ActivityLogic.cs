using System;
using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Info;

namespace WebLogic.Service.Info
{
    public class ActivityLogic
    {
        private ActivityDao dao = null;

        public ActivityLogic()
        {
            this.dao = new ActivityDao();
        }

        public Dictionary<string, object> GetOne(int actId)
        {
            Dictionary<string, object> one = this.dao.GetOne(actId);

            one["content"] = one["content"].ToString().Replace('\"', '\'');

            return one;
        }

        public string GetPageJson(int pageNo, int pageSize, int locationId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, locationId.ToString(), msg);
            return pr.PageJSON;
        }

        public PageRecords GetPage(int pageNo, int pageSize, int locationId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, locationId.ToString(), msg);
        }

        public bool Delete(int actId)
        {
            return this.dao.Delete(actId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }


    }
}
