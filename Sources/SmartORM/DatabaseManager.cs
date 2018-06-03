using Newtonsoft.Json;
using SmartORM.Attributes;
using SmartORM.DesignPattern;
using SmartORM.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM
{
    public class DatabaseManager : Singleton<DatabaseManager>
    {
        /// <summary>
        /// Extension des fichiers.
        /// </summary>
        public const string FILE_EXTENSION = ".json";

        private SmartFile SmartFile
        {
            get;
            set;
        }
        private Assembly RecordsAssembly
        {
            get;
            set;
        }
        public void Initialize(string smartFilePath, Assembly recordsAssembly)
        {
            this.RecordsAssembly = recordsAssembly;

            if (File.Exists(smartFilePath) == false)
            {
                SmartFile = new SmartFile(smartFilePath, new Dictionary<string, SmartTable>());
            }
            else
            {
                SmartFile = new SmartFile(smartFilePath);
            }
        }



        public void LoadTables()
        {
            foreach (Type type in RecordsAssembly.GetTypes())
            {
                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();

                if (attribute != null)
                {
                    IList tables = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));

                    var records = SmartFile.ConvertToRecord(attribute.Path, type);

                    foreach (var record in records)
                    {
                        tables.Add(record);
                    }

                    if (tables.Count > 0)
                    {
                        var field = ITableManager.Instance.GetCache(type);
                        field.SetValue(null, tables);
                    }
                }
            }
        }

        public void DropDatabase()
        {
            File.Delete(SmartFile.Path);
            SmartFile = new SmartFile(SmartFile.Path, new Dictionary<string, SmartTable>());
        }

        internal void Remove(ITable table)
        {
            SmartFile.RemoveTable(table);
        }
        internal void Update(ITable table)
        {
            SmartFile.UpdateTable(table);
        }

        internal void Add(ITable table)
        {
            SmartFile.AddTable(table);
        }

        public void Save()
        {
            SmartFile.Save();
        }
    }
}
