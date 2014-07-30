using System;
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
            Dictionary<string, object> one = this.dao.GetOne(raId);

            one["content"] = one["content"].ToString().Replace('\"', '\'');

            return one;
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
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, processId, msg);

            List<Dictionary<string, object>> list = pr.PageResult;

            if (list != null && list.Count > 0)
            {
                foreach (Dictionary<string, object> item in list)
                {
                    if (Boolean.Parse(item["isTop"].ToString()))
                    {
                        item.Add("topStr", "<span style='color:red'>是</span>");
                    }
                    else
                    {
                        item.Add("topStr", "否");
                    }

                    if (Boolean.Parse(item["isChecked"].ToString()))
                    {
                        item.Add("checkStr", "是");
                    }
                    else
                    {
                        item.Add("checkStr", "<span style='color:red'>否</span>");
                    }
                }
            }

            return pr.PageJSON;
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

        public bool SetCheck(string raIds)
        {
            return this.dao.SetCheck(raIds);
        }
    }
}
