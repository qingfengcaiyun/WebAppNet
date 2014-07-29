using System.Collections.Generic;
using System.Text;
using Glibs.Util;

namespace Glibs.Sql
{
    public class JsonDo
    {
        public static string Message(string msg)
        {
            return "{\"msg\":\"" + msg + "\"}";
        }

        public static string DictionaryToJSON(Dictionary<string, object> item)
        {
            if (item != null && item.Count > 0)
            {
                StringBuilder str = new StringBuilder();

                foreach (KeyValuePair<string, object> kv in item)
                {
                    str.Append(",\"");
                    str.Append(kv.Key);
                    str.Append("\":\"");
                    str.Append(kv.Value.ToString().Replace("\n", "\\n").Replace("\r", "\\r"));
                    str.Append("\"");
                }

                return "{" + str.ToString().Substring(1) + "}";
            }
            else
            {
                return "{}";
            }
        }

        public static string ListToJSON(List<Dictionary<string, object>> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder str = new StringBuilder();

                foreach (Dictionary<string, object> item in list)
                {
                    str.Append(",");
                    str.Append(DictionaryToJSON(item));
                }

                return "[" + str.ToString().Substring(1) + "]";
            }
            else
            {
                return "[]";
            }
        }

        public static string ListToTreeNodes(List<Dictionary<string, object>> list, string parentNo)
        {
            if (list != null && list.Count > 0)
            {
                if (!RegexDo.IsNumber(parentNo))
                {
                    parentNo = "0";
                }

                Dictionary<string, List<Dictionary<string, object>>> lists = new Dictionary<string, List<Dictionary<string, object>>>();

                Dictionary<string, object> temp = null;
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

                return "[" + GetSubTreeNodes(lists, parentNo) + "]";
            }
            else
            {
                return "[]";
            }
        }

        private static string GetSubTreeNodes(Dictionary<string, List<Dictionary<string, object>>> lists, string parentNo)
        {
            List<Dictionary<string, object>> list = lists[parentNo];
            if (list != null && list.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                string substr = string.Empty;
                foreach (Dictionary<string, object> item in list)
                {
                    str.Append(",{");
                    str.Append("\"id\":\"");
                    str.Append(item["funcNo"].ToString());
                    str.Append("\",");
                    str.Append("\"text\":\"");
                    str.Append(item["funcName"].ToString());
                    str.Append("\"");

                    substr = GetSubTreeNodes(lists, item["funcNo"].ToString());
                    if (string.IsNullOrEmpty(substr))
                    {
                        str.Append(",\"attributes\":{");
                        str.Append("\"url\":\"");
                        str.Append(item["funcUrl"].ToString());
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
