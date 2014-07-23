using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.SessionState;

namespace Glibs.Util
{
    /// <summary>
    ///WebConfig 只能被继承
    /// </summary>
    public class WebPageCore
    {
        /** Web.Config **/

        public static string GetAppSetting(string key)
        {
            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count; i++)
            {
                if (string.CompareOrdinal(key, ConfigurationManager.AppSettings.Keys[i]) == 0)
                {
                    return ConfigurationManager.AppSettings.Get(key);
                }
            }
            return string.Empty;
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /** Request **/

        public static string GetRequest(string key)
        {
            HttpRequest request = HttpContext.Current.Request;
            string r = string.Empty;

            if (!string.IsNullOrEmpty(request.Form[key]))
            {
                r = request.Form[key];
            }
            if (!string.IsNullOrEmpty(request.QueryString[key]))
            {
                r = request.QueryString[key];
            }
            return r;
        }

        public static string GetRequestUrl()
        {
            return HttpContext.Current.Request.UrlReferrer.ToString();
        }

        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUserIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        /** Cookies **/

        public static string GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
            string value = string.Empty;
            if (cookie != null)
            {
                value = cookie.Value;
            }
            cookie = null;
            return value;
        }

        public static Dictionary<string, string> GetCookies()
        {
            Dictionary<string, string> cookies = null;
            int count = HttpContext.Current.Request.Cookies.Count;
            string key = string.Empty;
            if (count > 0)
            {
                cookies = new Dictionary<string, string>();
                for (int i = 0; i < count; i++)
                {
                    key = HttpContext.Current.Request.Cookies.Keys[i];
                    cookies.Add(key, HttpContext.Current.Request.Cookies.Get(key).Value);
                }
            }
            return cookies;
        }

        public static void SetCookie(string key, string value, DateTime dateTime)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Value = value;
            cookie.Expires = dateTime;

            if (HttpContext.Current.Request.Cookies.Get(key) == null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public static void RemoveCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["key"];
            if (cookie != null)
            {
                HttpContext.Current.Response.Cookies.Remove(key);
            }
        }

        public static void ClearCookies()
        {
            int count = HttpContext.Current.Request.Cookies.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Cookies.Remove(HttpContext.Current.Request.Cookies.Keys[i]);
                }
            }
        }

        /** Application **/

        public static object GetApplication(string key)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            NameObjectCollectionBase.KeysCollection keys = application.Keys;

            if (keys != null && keys.Count > 0)
            {
                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        return application[key];
                    }
                }
            }

            return null;
        }

        public static Dictionary<string, object> GetApplications()
        {
            Dictionary<string, object> applications = null;
            HttpApplicationState application = HttpContext.Current.Application;
            NameObjectCollectionBase.KeysCollection keys = application.Keys;

            if (keys != null && keys.Count > 0)
            {
                applications = new Dictionary<string, object>();

                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    applications.Add(keys[i], application[keys[i]]);
                }
            }

            return applications;
        }

        public static void SetApplication(string key, object value)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            NameObjectCollectionBase.KeysCollection keys = application.Keys;

            if (keys != null && keys.Count > 0)
            {
                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        application.Remove(key);
                    }
                }
            }

            application.Add(key, value);
        }

        public static void RemoveApplication(string key)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            NameObjectCollectionBase.KeysCollection keys = application.Keys;

            if (keys != null && keys.Count > 0)
            {
                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        application.Remove(key);
                    }
                }
            }
        }

        public static void ClearApplications()
        {
            HttpApplicationState application = HttpContext.Current.Application;

            if (application.Count > 0)
            {
                application.RemoveAll();
            }
        }

        /** Session **/



        public static object GetSession(string key)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Keys.Count > 0)
            {
                NameObjectCollectionBase.KeysCollection keys = HttpContext.Current.Session.Keys;

                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        return HttpContext.Current.Session[key];
                    }
                }
            }

            return null;
        }

        public static Dictionary<string, object> GetSessions()
        {
            Dictionary<string, object> sessions = null;

            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Keys.Count > 0)
            {
                HttpSessionState session = HttpContext.Current.Session;
                NameObjectCollectionBase.KeysCollection keys = session.Keys;

                sessions = new Dictionary<string, object>();

                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    sessions.Add(keys[i], session[keys[i]]);
                }
            }

            return sessions;
        }

        public static void SetSession(string key, object value)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Keys.Count > 0)
            {
                HttpSessionState session = HttpContext.Current.Session;
                NameObjectCollectionBase.KeysCollection keys = session.Keys;

                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        session.Remove(key);
                    }
                }

                session.Add(key, value);
            }
        }

        public static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Keys.Count > 0)
            {
                HttpSessionState session = HttpContext.Current.Session;
                NameObjectCollectionBase.KeysCollection keys = session.Keys;

                for (int i = 0, j = keys.Count; i < j; i++)
                {
                    if (string.CompareOrdinal(key, keys.Get(i)) == 0)
                    {
                        session.Remove(key);
                    }
                }
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        /** Server **/

        public static string GetMapPath(string vfile)
        {
            return HttpContext.Current.Server.MapPath(vfile);
        }

        public static Dictionary<string, object> GetParameters()
        {
            string paramStr = GetRequest("paramStr");

            if (paramStr != null && !string.IsNullOrEmpty(paramStr))
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                string[] paramList = paramStr.Split(',');
                if (paramList.Length > 0)
                {
                    for (int i = 0, j = paramList.Length; i < j; i++)
                    {
                        content.Add(paramList[i], GetRequest(paramList[i]));
                    }
                }
                return content;
            }
            else
            {
                return null;
            }
        }
    }
}