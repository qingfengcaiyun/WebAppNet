using System;
using System.Collections;
using System.Collections.Generic;
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
            string request = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[key]))
            {
                request = HttpContext.Current.Request.Form[key];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[key]))
            {
                request = HttpContext.Current.Request.QueryString[key];
            }
            return request;
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
            if (application[key] != null)
            {
                return application[key];
            }
            else
            {
                return null;
            }
        }

        public static Hashtable GetApplications()
        {
            Hashtable applications = null;
            string key = string.Empty;
            HttpApplicationState application = HttpContext.Current.Application;
            int count = application.Count;
            if (count > 0)
            {
                applications = new Hashtable();
                for (int i = 0; i < count; i++)
                {
                    key = application.Keys[i];
                    applications.Add(key, application[key]);
                }
            }
            return applications;
        }

        public static void SetApplication(string key, object value)
        {
            if (HttpContext.Current.Application[key] == null)
            {
                HttpContext.Current.Application.Add(key, value);
            }
            else
            {
                HttpContext.Current.Application.Set(key, value);
            }
        }

        public static void RemoveApplication(string key)
        {
            if (HttpContext.Current.Application[key] != null)
            {
                HttpContext.Current.Application.Remove(key);
            }
        }

        public static void ClearApplications()
        {
            if (HttpContext.Current.Application.Count > 0)
            {
                HttpContext.Current.Application.RemoveAll();
            }
        }

        /** Session **/

        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static Hashtable GetSessions()
        {
            HttpSessionState session = HttpContext.Current.Session;

            int count = session.Count;

            if (count > 0)
            {
                Hashtable sessions = new Hashtable();
                string key = string.Empty;
                for (int i = 0; i < count; i++)
                {
                    key = session.Keys.Get(i);
                    sessions.Add(key, session[key]);
                }
                return sessions;
            }
            else
            {
                return null;
            }
        }

        public static void SetSession(string key, object value)
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session[key] != null)
            {
                session.Remove(key);
            }
            session.Add(key, value);
        }

        public static void RemoveSession(string key)
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session[key] != null)
            {
                session.Remove(key);
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