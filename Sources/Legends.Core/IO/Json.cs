using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.IO
{
    public class Json
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        public static object Deserialize(string content, Type type)
        {
            return JsonConvert.DeserializeObject(content, type);
        }
        public static T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
