using System;
using System.Collections.Generic;
using System.Text;
using WebDao.Dao.System;

namespace WebLogic.Service.System
{
    public class FunctionLogic
    {
        private FunctionDao dao = null;

        public FunctionLogic()
        {
            this.dao = new FunctionDao();
        }

        public String GetTree(String parentNo, int userId)
        {
            List<Dictionary<string, object>> list = null;
            if (userId > 0)
            {
                list = this.dao.GetListByUserId(parentNo, userId);
            }
            else
            {
                list = this.dao.GetList(parentNo);
            }
            Dictionary<string, object> temp = null;
            Dictionary<string, List<Dictionary<string, object>>> lists = new Dictionary<string, List<Dictionary<string, object>>>();
            string key = "";

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
            string substr = string.Empty;

            if (list != null && list.Count > 0)
            {
                for (int i = 0, j = list.Count; i < j; i++)
                {
                    temp = list[i];

                    str.Append(",{");
                    str.Append("\"id\":\"");
                    str.Append(temp["funcId"].ToString());
                    str.Append("\",");
                    str.Append("\"text\":\"");
                    str.Append(temp["funcName"].ToString());
                    str.Append("\"");

                    substr = this.GetSubTree(lists, temp["funcNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append(",\"attributes\":{");
                        str.Append("\"url\":\"");
                        str.Append(temp["funcUrl"].ToString());
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
                    str.Append("\"funcId\":");
                    str.Append(temp["funcId"].ToString());
                    str.Append(",\"funcName\":\"");
                    str.Append(temp["funcName"].ToString());
                    str.Append("\",\"funcNo\":\"");
                    str.Append(temp["funcNo"].ToString());
                    str.Append("\",\"funcUrl\":\"");
                    str.Append(temp["funcUrl"].ToString());
                    str.Append("\",\"parentNo\":\"");
                    str.Append(temp["parentNo"].ToString());

                    if (!Boolean.Parse(list[i]["isLeaf"].ToString()))
                    {
                        str.Append("\",\"state\":\"closed");
                    }

                    str.Append("\"");

                    substr = this.GetSubTreeGrid(tlist, temp["funcNo"].ToString());
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

        public Dictionary<string, object> GetOne(int funcId)
        {
            return this.dao.GetOne(funcId);
        }

        public List<Dictionary<string, object>> GetList(string parentNo)
        {
            return this.dao.GetList(parentNo);
        }

        public long Insert(Dictionary<string, object> content)
        {
            return this.dao.Insert(content);
        }

        public bool Update(Dictionary<string, object> content)
        {
            return this.dao.Update(content);
        }

        public bool Delete(string funcNo)
        {
            return this.dao.Delete(funcNo);
        }
    }
}
