using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Renovation;

namespace WebLogic.Service.Renovation
{
    public class ProjectLogic
    {
        private ProjectDao dao = null;

        public ProjectLogic()
        {
            this.dao = new ProjectDao();
        }

        public Dictionary<string, object> GetOne(long projectId)
        {
            return this.dao.GetOne(projectId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int cityId, int regionId)
        {
            return this.dao.GetList(msg, cityId, regionId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cityId, int regionId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, cityId, regionId, msg);
        }

        public bool Delete(long projectId)
        {
            return this.dao.Delete(projectId);
        }

        public long Insert(Dictionary<string, object> content, int[] paramIds, long[] fileIds)
        {
            return this.dao.Insert(content, paramIds, fileIds);
        }

        public bool Update(Dictionary<string, object> content, int[] paramIds, long[] fileIds)
        {
            return this.dao.Update(content, paramIds, fileIds);
        }

        public bool SetReadCount(long projectId)
        {
            return this.dao.SetReadCount(projectId);
        }
    }
}
