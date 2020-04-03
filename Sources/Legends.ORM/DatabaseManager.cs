using Legends.Core;
using Legends.Core.DesignPattern;
using Legends.Core.IO;
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
        int UnknownIncr = 0;

        static Logger logger = new Logger();

        private Assembly TargetAssembly
        {
            get;
            set;
        }
        private string TargetPath
        {
            get;
            set;
        }
        private List<Type> TableTypes
        {
            get;
            set;
        }

        public bool Initialize(Assembly recordAssembly, string path)
        {
            TargetPath = Path.GetFullPath(path);
            TargetAssembly = recordAssembly;
            TableTypes = new List<Type>();

            if (!Directory.Exists(TargetPath))
            {
                Directory.CreateDirectory(TargetPath);
            }


            foreach (var type in TargetAssembly.GetTypes())
            {
                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();

                if (attribute != null)
                {
                    if (!type.HasInterface(typeof(ITable)))
                    {
                        logger.Write(type.FullName + " has no ITable interface.", MessageState.ERROR);
                        return false;
                    }
                }
                else
                {
                    continue;
                }

                FieldInfo cache = GetCache(type);

                if (cache == null)
                {
                    logger.Write("Unable to find cache for type " + type.FullName);
                    return false;
                }

                PropertyInfo primaryProperty = GetPrimaryProperty(type);

                if (primaryProperty == null)
                {
                    logger.Write("Unable to find primary key for type " + type.FullName);
                    return false;
                }
                TableTypes.Add(type);

            }
            return true;
        }
        public void LoadTables()
        {
            foreach (var type in TableTypes)
            {
                TableAttribute attribute = type.GetCustomAttribute<TableAttribute>();

                FieldInfo cache = GetCache(type);

                string dir = Path.Combine(TargetPath, attribute.path);

                var newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));

                foreach (var file in Directory.GetFiles(dir))
                {
                    ITable element = (ITable)Json.Deserialize(File.ReadAllText(file), type);
                    newList.Add(element);
                }

                cache.SetValue(null, newList);
            }
        }

        public void DropAll()
        {
            DirectoryInfo di = new DirectoryInfo(TargetPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        private PropertyInfo GetPrimaryProperty(Type type)
        {
            return type.GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PrimaryAttribute>() != null);
        }
        private FieldInfo GetCache(Type type)
        {
            var attribute = type.GetCustomAttribute(typeof(TableAttribute), false);

            if (attribute == null)
                return null;

            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.FieldType.IsGenericType);

            if (field == null || !field.IsStatic || !field.FieldType.IsGenericType)
            {
                return null;
            }

            return field;
        }

        public void Add<T>(T table) where T : ITable
        {
            string path = table.GetType().GetCustomAttribute<TableAttribute>().path;

            string dir = Path.Combine(TargetPath, path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            PropertyInfo primary = table.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PrimaryAttribute>() != null);

            var fileName = primary.GetValue(table, new object[] { }).ToString();

            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                fileName = "Unknown" + (++UnknownIncr);
            }

            string json = Json.Serialize(table);

            File.WriteAllText(Path.Combine(dir, fileName + ".json"), json);

        }
        public void Remove<T>(T table) where T : ITable
        {
            throw new NotImplementedException();
        }
    }
}
