using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Collections;

namespace Pharmazer.Base.Tools
{
    public class Caching
    {
        public static void SetCache<T>(string key, T data)
            where T : class
        {
            if (data != null)
            {
                Cache Cache = System.Web.HttpContext.Current.Cache;
                Cache.Remove(key);
                Cache.Insert(key, data, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
        }

        public static bool CheckCache<T>(string Key)
        where T : class
        {
            Cache Cache = System.Web.HttpContext.Current.Cache;
            if (Cache[Key] != null && Cache[Key] is T)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T GetCache<T>(string Key)
             where T : class
        {
            Cache Cache = System.Web.HttpContext.Current.Cache;
            T Data = null;
            if (Cache[Key] is T)
            {
                Data = (T)Cache[Key];
            }
            return Data;
        }
    }
}