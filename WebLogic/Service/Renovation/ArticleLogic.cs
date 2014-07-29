using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Renovation;

namespace WebLogic.Service.Renovation
{
    public class ArticleLogic
    {
        private ArticleDao dao = null;

        public ArticleLogic()
        {
            this.dao = new ArticleDao();
        }

        public Dictionary<string, object> GetOne(int raId)
        {
            return this.dao.GetOne(raId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int processId)
        {
            return this.dao.GetList(msg, processId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int processId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, processId, msg);
        }

        public string GetPageJson(int pageSize, int pageNo, int processId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, processId, msg).PageJSON;
        }

        public bool Delete(int raId)
        {
            return this.dao.Delete(raId);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public int SetReadCount(int raId)
        {
            return this.dao.SetReadCount(raId);
        }
    }
}
