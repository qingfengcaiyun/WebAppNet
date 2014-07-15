using System.Collections.Generic;
using Glibs.Sql;
using WedDao.Dao.Info;

namespace WebLogic.Service.Info
{
    public class NewsLogic
    {
        private NewsDao dao = null;
        private RelationshipDao rdao = null;

        public NewsLogic()
        {
            this.dao = new NewsDao();
            this.rdao = new RelationshipDao();
        }

        public Dictionary<string, object> GetOne(int newsId)
        {
            Dictionary<string, object> one = this.dao.GetOne(newsId);
            one.Add("cateList", this.rdao.GetCateList(newsId));
            return one;
        }

        public bool Delete(int newsId)
        {
            return this.dao.Delete(newsId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool SetReadCount(int newsId)
        {
            return this.dao.SetReadCount(newsId);
        }

        public bool SetRelationship(int[] cateIds, int newsId)
        {
            return this.rdao.SaveList(cateIds, newsId);
        }

        public string GetPageJson(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, cateId, cityId, msg);
            return pr.PageJSON;
        }

        public PageRecords GetPage(int pageSize, int pageNo, int cateId, string cityId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, cateId, cityId, msg);
        }
    }
}
