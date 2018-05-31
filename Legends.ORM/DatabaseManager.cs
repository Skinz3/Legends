using Legends.Core.Utils;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM
{
    public class DatabaseManager : Singleton<DatabaseManager>
    {
        public const string FILE_EXTENSION = ".json";

        private string BasePath
        {
            get;
            set;
        }
        private Assembly RecordAssembly
        {
            get;
            set;
        }

        public string GetFilename(ITable table)
        {
            return BasePath + table.GetType().GetCustomAttribute<TableAttribute>().Path + GetFilenameAttribute(table) + FILE_EXTENSION;
        }
        public void CreateDirectories()
        {
            foreach (Type type in RecordAssembly.GetTypes())
            {

                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();

                if (attribute != null)
                {
                    string path = BasePath + attribute.Path;

                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
        public void RemoveFromList<T>(T table) where T : ITable
        {
            var field = GetCache(table.GetType());
            var method = field.FieldType.GetMethod("Remove");
            method.Invoke(field.GetValue(null), new object[] { table });
        }

        private string GetFilenameAttribute(ITable table)
        {
            var field = table.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<JsonFileNameAttribute>() != null);
            return field.GetValue(table).ToString();
        }
        public void Initialize(string basePath, Assembly recordAssembly)
        {
            this.RecordAssembly = recordAssembly;
            this.BasePath = basePath;
            this.CreateDirectories();
        }
        public void LoadTables()
        {
            foreach (Type type in RecordAssembly.GetTypes())
            {
                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();

                if (attribute != null)
                {
                    IList tables = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                    string path = BasePath + attribute.Path;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    foreach (var file in Directory.GetFiles(path))
                    {
                        tables.Add((ITable)JsonConvert.DeserializeObject(File.ReadAllText(file), type));
                    }

                    if (tables.Count > 0)
                    {
                        var field = GetCache(type);
                        field.SetValue(null, tables);
                    }
                }


            }
        }
        public void AddToList(ITable element)
        {
            var field = GetCache(element.GetType());
            var method = field.FieldType.GetMethod("Add");
            method.Invoke(field.GetValue(null), new object[] { element });
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
