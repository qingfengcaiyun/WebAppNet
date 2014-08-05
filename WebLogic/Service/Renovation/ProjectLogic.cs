using System;
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

        public List<Dictionary<string, object>> GetParams(long projectId)
        {
            return this.dao.GetParams(projectId);
        }

        public List<Dictionary<string, object>> GetPictures(long projectId)
        {
            return this.dao.GetPictures(projectId);
        }

        public List<Dictionary<string, object>> GetList(string msg, int locationId, long memberId, long designerId)
        {
            return this.dao.GetList(msg, locationId, memberId, designerId);
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, long memberId, long designerId, string msg)
        {
            PageRecords pr = this.dao.GetPage(pageSize, pageNo, locationId, memberId, designerId, msg);

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
                }
            }

            return pr;
        }

        public string GetPageJson(int pageSize, int pageNo, int locationId, long memberId, long designerId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, locationId, memberId, designerId, msg).PageJSON;
        }

        public bool Delete(long projectId)
        {
            return this.dao.Delete(projectId);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool SavePictures(long projectId, List<Dictionary<string, object>> projectPics)
        {
            return this.dao.SavePictures(projectId, projectPics);
        }

        public bool SaveParams(long projectId, int[] paramIds)
        {
            return this.dao.SaveParams(projectId, paramIds);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public int SetReadCount(long projectId)
        {
            return this.dao.SetReadCount(projectId);
        }

        public bool CloseProject(long projectId)
        {
            return this.dao.CloseProject(projectId);
        }

        public long SavePicture(long projectId, string picPath)
        {
            return this.dao.SavePicture(projectId, picPath);
        }

        public bool DelPicture(string ppcIds)
        {
            return this.dao.DelPicture(ppcIds);
        }

        public long SaveParam(long projectId, int paramId)
        {
            return this.dao.SaveParam(projectId, paramId);
        }

        public bool DelParam(long pptId)
        {
            return this.dao.DelParam(pptId);
        }
    }
}
