using System;
using System.IO;

namespace Host.Common
{
    public static class ConfigHelper
    {
        public static void SaveConfig<T>(string key, T value) where T : class
        {
            string json = JSONHelper.Serialize(value);
            string path = GetFilePath(key);
            File.WriteAllText(path, json);
        }

        public static T GetConfig<T>(string key) where T : class
        {
            string path = GetFilePath(key);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JSONHelper.Deserialize<T>(json);
            }
            else
            {
                return null;
            }
        }

        private static string GetFilePath(string key)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, key);
        }
    }
}