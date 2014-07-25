using System;
using System.Collections.Generic;
using System.Text;
using WebDao.Dao.Info;

namespace WebLogic.Service.Info
{
    public class CategoryLogic
    {
        private CategoryDao dao = null;

        public CategoryLogic()
        {
            this.dao = new CategoryDao();
        }

        public Dictionary<string, object> GetOne(int cateId)
        {
            return this.dao.GetOne(cateId);
        }

        public string GetCateIds(string cateNos)
        {
            string[] cs = cateNos.Split(',');

            StringBuilder str = new StringBuilder();

            foreach (string c in cs)
            {
                str.Append(",");
                str.Append(this.dao.GetCateId(c));
            }

            return str.ToString().Substring(1);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            return this.dao.GetList(parentNo);
        }

        public bool Delete(string cateNo)
        {
            return this.dao.Delete(cateNo);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public Int64 Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public string GetTreeGrid(string parentNo)
        {
            List<Dictionary<string, object>> list = this.dao.GetList(parentNo);

            Dictionary<string, List<Dictionary<string, object>>> tlist = new Dictionary<string, List<Dictionary<string, object>>>();

            if (list != null && list.Count > 0)
            {
                string keyName = "";

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    keyName = list[i]["parentNo"].ToString();

                    if (!Boolean.Parse(list[i]["isLeaf"].ToString()) && string.CompareOrdinal(parentNo, list[i]["parentNo"].ToString()) != 0)
                    {
                        list[i].Add("state", "closed");
                    }

                    if (tlist.ContainsKey(keyName))
                    {
                        tlist[keyName].Add(list[i]);
                    }
                    else
                    {
                        tlist.Add(keyName, new List<Dictionary<string, object>>());
                        tlist[keyName].Add(list[i]);
                    }
                }
            }

            return "{\"total\":" + list.Count + ", \"rows\":[" + this.GetSubTreeGrid(tlist, parentNo) + "]}";
        }

        private string GetSubTreeGrid(Dictionary<string, List<Dictionary<string, object>>> tlist, string parentNo)
        {
            List<Dictionary<string, object>> list = tlist.ContainsKey(parentNo) ? tlist[parentNo] : null;

            if (list != null && list.Count > 0)
            {
                Dictionary<string, object> temp = null;
                string substr = "";

                StringBuilder str = new StringBuilder();

                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];

                    str.Append(",{");
                    str.Append("\"cateId\":");
                    str.Append(temp["cateId"].ToString());
                    str.Append(",\"cateName\":\"");
                    str.Append(temp["cateName"].ToString());
                    str.Append("\",\"cateNo\":\"");
                    str.Append(temp["cateNo"].ToString());
                    str.Append("\",\"parentNo\":\"");
                    str.Append(temp["parentNo"].ToString());

                    if (temp.ContainsKey("state"))
                    {
                        str.Append("\",\"state\":\"closed");
                    }

                    str.Append("\"");

                    substr = this.GetSubTreeGrid(tlist, temp["cateNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append("}");
                    }
                    else
                    {
                        str.Append(",\"children\":[");
                        str.Append(substr);
                        str.Append("]}");
                    }
                }

                return str.ToString().Substring(1);
            }
            else
            {
                return "";
            }
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
                    str.Append(temp["cateId"].ToString());
                    str.Append("\",");
                    str.Append("\"text\":\"");
                    str.Append(temp["cateName"].ToString());
                    str.Append("\"");

                    substr = this.GetSubTree(lists, temp["cateNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append("}");
                    }
                    else
                    {
                        str.Append(",\"children\":[");
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
