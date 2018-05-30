using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.JSON
{
    public class JsonSerializer<T>
    {
        JsonSerializer m_serializer;

        public JsonSerializer()
        {
            m_serializer = new JsonSerializer(typeof(T));
        }
        public void Serialize(T obj, string filePath)
        {
            m_serializer.Serialize(obj, filePath);
        }
        public T Deserialize(string filePath)
        {
            return (T)m_serializer.Deserialize(filePath);
        }
    }
    public class JsonSerializer
    {
        DataContractJsonSerializer m_serializer;

        public JsonSerializer(Type type)
        {
            m_serializer = new DataContractJsonSerializer(type);
        }

        public void Serialize(Object obj, string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            m_serializer.WriteObject(stream, obj);
            stream.Close();
        }

        public object Deserialize(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Object obj = m_serializer.ReadObject(stream);
            stream.Close();
            return obj;
        }
    }
}
