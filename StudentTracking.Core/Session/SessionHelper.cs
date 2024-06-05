using Microsoft.AspNetCore.Http;
using System.Text;

namespace StudentTracking.Core.Session
{
    public static class SessionContext
    {
        public static string GetString(string key)
        {
            return AppHttpContext.Current.Session.GetString(key);
        }

        public static void SetString(string key, string value)
        {
            AppHttpContext.Current.Session.SetString(key, value);
            SaveChanges();
        }

        public static int GetInt(string key)
        {
            return AppHttpContext.Current.Session.GetInt32(key) ?? 0;
        }

        public static void SetInt(string key, int value)
        {
            AppHttpContext.Current.Session.SetInt32(key, value);
            SaveChanges();
        }

        public static void Remove(string key)
        {
            AppHttpContext.Current.Session.Remove(key);
            SaveChanges();
        }

        public static void Clear()
        {
            AppHttpContext.Current.Session.Clear();
            SaveChanges();
        }

        private static void SaveChanges()
        {
            AppHttpContext.Current.Session.CommitAsync().GetAwaiter().GetResult();
        }
    }
}