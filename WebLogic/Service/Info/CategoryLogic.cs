using System;
using System.Collections.Generic;
using System.Text;
using WedDao.Dao.Info;

namespace WebLogic.Service.Info
{
    public class CategoryLogic
    {
        private CategoryDao dao = null;

        public CategoryLogic()
        {
            this.dao = new CategoryDao();
        }

        public Dictionary<string, object> getOne(int cateId)
        {
            return this.dao.GetOne(cateId);
        }

        public List<Dictionary<string, object>> getList(string parentNo)
        {
            return this.dao.GetList(parentNo);
        }

        public bool Delete(int cateId)
        {
            return this.dao.Delete(cateId);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public string GetTree(string parentNo)
        {
            List<Dictionary<string, object>> list = this.dao.GetList(parentNo);

            Dictionary<string, object> temp = null;
            Dictionary<string, List<Dictionary<string, object>>> lists = new Dictionary<string, List<Dictionary<string, object>>>();
            String key = "";

            if (list.Count > 0)
            {
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];
                    key = temp["parentNo"].ToString();
                    if (lists.ContainsKey(key))
                    {
                        lists[key].Add(temp);
                    }
                    else
                    {
                        lists.Add(key, new List<Dictionary<string, object>>());
                        lists[key].Add(temp);
                    }
                }
            }

            StringBuilder str = new StringBuilder();
            str.Append("[");
            str.Append(this.GetSubTree(lists, parentNo));
            str.Append("]");
            return str.ToString();
        }

        private string GetSubTree(Dictionary<string, List<Dictionary<string, object>>> lists, string parentNo)
        {
            StringBuilder str = new StringBuilder();
            List<Dictionary<string, object>> list = lists.ContainsKey(parentNo) ? lists[parentNo] : null;
            Dictionary<string, object> temp = null;
            string substr = "";

            if (list != null && list.Count > 0)
            {
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];

                    str.Append(",{");
                    str.Append("\"id\":\"");
                    str.Append(temp["cateNo"].ToString());
                    str.Append("\",");
                    str.Append("\"text\":\"");
                    str.Append(temp["cateName"].ToString());
                    str.Append("\"");

                    substr = this.GetSubTree(lists, temp["cateNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append(",\"attributes\":{");
                        str.Append("\"cateId\":\"");
                        str.Append(temp["cateId"].ToString());
                        str.Append("\",\"url\":\"");
                        str.Append("info/ArticleAction?cateId="
                            + temp["cateId"].ToString());
                        str.Append("\"}}");
                    }
                    else
                    {
                        str.Append(",\"state\":\"closed\",");
                        str.Append("\"children\":[");
                        str.Append(substr);
                        str.Append("]}");
                    }
                }

                return str.ToString().Substring(1);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
