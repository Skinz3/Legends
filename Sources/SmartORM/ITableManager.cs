using SmartORM.Attributes;
using SmartORM.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SmartORM
{
    /// <summary>
    /// Todo : Optimize with Initialize() methods, (i dont want to use System.Reflection at runtime)
    /// </summary>
    public class ITableManager : Singleton<ITableManager>
    {
        /// <summary>
        /// Nom de la methode du framework .NET De IList.cs Add(T obj)
        /// </summary>
        public const string ILIST_ADD_METHOD = "Add";
        /// <summary>
        /// Nom de la methode du framework .NET De IList.cs Remove(T obj)
        /// </summary>
        public const string ILIST_REMOVE_METHOD = "Remove";
        /// <summary>
        /// Emplacement par default ou se trouve les fichiers.
        /// </summary>
        /// 
        public string GetFilePath(ITable table)
        {
            return GetTableAttribute(table).Path + GetFilename(table) + DatabaseManager.FILE_EXTENSION;
        }

        private string GetFilename(ITable table)
        {
            var field = table.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<JsonFileNameAttribute>() != null);
            return field.GetValue(table).ToString();
        }

        internal TableAttribute GetTableAttribute(ITable table)
        {
            return table.GetType().GetCustomAttribute<TableAttribute>();
        }

        public void AddToCache(ITable element)
        {
            var field = GetCache(element.GetType());
            var method = field.FieldType.GetMethod(ILIST_ADD_METHOD);
            method.Invoke(field.GetValue(null), new object[] { element });
        }
        public void RemoveFromCache(ITable table)
        {
            var field = GetCache(table.GetType());
            var method = field.FieldType.GetMethod(ILIST_REMOVE_METHOD);
            method.Invoke(field.GetValue(null), new object[] { table });
        }
        public FieldInfo GetCache(Type type)
        {
            var attribute = type.GetCustomAttribute(typeof(TableAttribute), false);
            if (attribute == null)
                return null;

            return type.GetFields(BindingFlags.Static | BindingFlags.NonPublic).FirstOrDefault(x => x.GetCustomAttribute<JsonCacheAttribute>() != null);
        }
    }
}
