using System;
using System.IO;

namespace FutureArbitrage.Util
{
    public static class ConfigHelper
    {
        private static readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static void SaveConfig<T>(string key, T value) where T : class
        {
            string json = JSONHelper.Serialize(value);
            string path = GetFilePath(key);
            File.WriteAllText(path, json);
        }

        public static T GetConfig<T>(string key) where T : class
        {
            string path = GetFilePath(key);
            if (!File.Exists(path))
            {
                return null;
            }
            string json = File.ReadAllText(path);
            T value = JSONHelper.Deserialize<T>(json);
            return value;
        }

        private static string GetFilePath(string key)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, key);
            return path;
        }
    }
}
