using System;
using System.Collections.Generic;
using Glibs.Sql;
using WebDao.Dao.Info;
using WebLogic.Service.System;

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

        public Dictionary<string, object> GetOne(Int64 newsId)
        {
            Dictionary<string, object> one = this.dao.GetOne(newsId);
            one.Add("cateList", this.rdao.GetCateList(newsId));
            return one;
        }

        public bool Delete(int newsId)
        {
            Dictionary<string, object> item = this.dao.GetOne(newsId);

            string fileIds = item["fileIds"].ToString();

            if (fileIds.Length > 0)
            {
                string[] ids = fileIds.Split(',');

                foreach (string id in ids)
                {
                    new FileInfoLogic().Delete(Int64.Parse(id));
                }
            }

            return this.dao.Delete(newsId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            Dictionary<string, object> n = this.GetOne(Int64.Parse(content["newsId"].ToString()));

            if (n["fileIds"].ToString().Trim().Length > 0)
            {
                string s = "," + content["fileIds"] + ",";
                string[] ns = n["fileIds"].ToString().Split(',');

                foreach (string nid in ns)
                {
                    if (!s.Contains("," + nid + ","))
                    {
                        new FileInfoLogic().Delete(Int64.Parse(nid));
                    }
                }
            }

            return this.dao.Update(content);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public int SetReadCount(int newsId)
        {
            return this.dao.SetReadCount(newsId);
        }

        public bool SetRelationship(long[] cateIds, long newsId)
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
