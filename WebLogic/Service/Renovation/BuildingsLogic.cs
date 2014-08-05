using System.Collections.Generic;
using System.Text;
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

        public string GetTree(string msg, int locationId)
        {
            List<Dictionary<string, object>> list = this.GetList(msg, locationId);

            if (list != null && list.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (Dictionary<string, object> item in list)
                {
                    s.Append(",{\"id\":\"");
                    s.Append(item["buildingsId"].ToString());
                    s.Append("\",\"text\":\"");
                    s.Append(item["buildingsName"].ToString());
                    s.Append("\"}");
                }

                return "[" + s.ToString().Substring(1) + "]";
            }
            else
            {
                return "[]";
            }
        }

        public PageRecords GetPage(int pageSize, int pageNo, int locationId, string msg)
        {
            return this.dao.GetPage(pageSize, pageNo, locationId, msg);
        }

        public string GetPageJson(int pageSize, int pageNo, int locationId, string msg)
        {
            return this.GetPage(pageSize, pageNo, locationId, msg).PageJSON;
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
