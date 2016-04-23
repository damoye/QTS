using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Host.Common
{
    public static class JSONHelper
    {
        public static T Deserialize<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}