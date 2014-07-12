using System;
using System.Collections.Generic;
using System.Text;
using WedDao.Dao.System;

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
                    str.Append(temp["funcNo"].ToString());
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
    }
}
