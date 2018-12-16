using Legends.Core.Utils;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM
{
    public class ITableManager
    {
        private static Logger logger = new Logger();

        private static Dictionary<Type, FieldInfo> m_caches = new Dictionary<Type, FieldInfo>();

        public static void RemoveFromCache(ITable element)
        {
            Type elementType = element.GetType();
            var field = GetCache(elementType);
            field.FieldType.GetMethod("Remove").Invoke(field.GetValue(null), new object[] { element });
        }
        public static FieldInfo GetCache(Type type)
        {
            if (m_caches.ContainsKey(type))
            {
                return m_caches[type];
            }
            else
            {
                var attribute = type.GetCustomAttribute(typeof(TableAttribute), false);
                if (attribute == null)
                    return null;

                var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name.ToLower() == (attribute as TableAttribute).tableName.ToLower());

                if (field == null || !field.IsStatic || !field.FieldType.IsGenericType)
                {
                    throw new Exception("Unable to find cache for record : " + type.Name);
                }
                m_caches.Add(type, field);
                return field;
            }
        }
        public static void AddToCache(ITable element)
        {
            var field = GetCache(element.GetType());
            field.FieldType.GetMethod("Add").Invoke(field.GetValue(null), new object[] { element });
        }
    }
}
