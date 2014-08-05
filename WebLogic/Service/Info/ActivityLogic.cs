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
            return this.dao.GetOne(actId);
        }

        public List<Dictionary<string, object>> GetListOnIndex(int locationId)
        {
            return this.dao.GetListOnIndex(locationId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId)
        {
            return this.dao.GetList(msg, locationId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, locationId, msg);

            if (pr.PageResult != null && pr.PageResult.Count > 0)
            {
                for (int i = 0, j = pr.PageResult.Count; i < j; i++)
                {
                    if (Boolean.Parse(pr.PageResult[i]["isClosed"].ToString()))
                    {
                        pr.PageResult[i].Add("closedStr", "是");
                    }
                    else
                    {
                        pr.PageResult[i].Add("closedStr", "否");
                    }

                    if (Boolean.Parse(pr.PageResult[i]["isChecked"].ToString()))
                    {
                        pr.PageResult[i].Add("checkStr", "是");
                    }
                    else
                    {
                        pr.PageResult[i].Add("checkStr", "否");
                    }

                    if (Boolean.Parse(pr.PageResult[i]["onIndex"].ToString()))
                    {
                        pr.PageResult[i].Add("indexStr", "是");
                    }
                    else
                    {
                        pr.PageResult[i].Add("indexStr", "否");
                    }
                }
            }

            return pr;
        }

        public string GetPageJson(int pageSize, int pageNo, int locationId, string msg)
        {
            return this.GetPage(pageSize, pageNo, locationId, msg).PageJSON;
        }

        public bool Delete(int actId)
        {
            return this.dao.Delete(actId);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public int SetReadcount(int actId)
        {
            return this.dao.SetReadcount(actId);
        }

        public bool SetOnIndex(int actId)
        {
            return this.dao.SetOnIndex(actId);
        }
    }
}
