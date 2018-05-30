using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections;
using Legends;
using System.Threading;
using Legends.ORM.IO;
using Legends.ORM.Interfaces;
using Legends.ORM.Attributes;
using Legends.ORM.Addon;
using Legends.Core;
using Legends.Core.Utils;

namespace Legends.ORM
{
    public class DatabaseManager
    {
        private Logger logger = new Logger();

        private const string CREATE_TABLE = "CREATE TABLE if not exists {0} ({1})";

        private const string DROP_TABLE = "DROP TABLE if exists {0}";

        private static DatabaseManager _self;

        internal MySqlConnection m_provider;

        private List<MethodInfo> m_removeMethods = new List<MethodInfo>();

        public Assembly RecordsAssembly;

        public DatabaseManager(Assembly recordsAssembly, string host, string database, string user, string password)
        {
            if (_self == null)
                _self = this;
            this.m_provider = new MySqlConnection(string.Format("Server={0};UserId={1};Password={2};Database={3}", host, user, password, database));
            this.RecordsAssembly = recordsAssembly;
            this.LoadRemoveMethods();
        }

        public MySqlConnection UseProvider()
        {
            return UseProvider(m_provider);
        }
        private MySqlConnection UseProvider(MySqlConnection connection)
        {
            if (!connection.Ping())
            {
                connection.Close();
                connection.Open();
            }

            return connection;
        }
        private void LoadRemoveMethods()
        {
            var tablesTypes = RecordsAssembly.GetTypes().Where(x => x.GetInterface("ITable") != null).Where(x => x.GetCustomAttribute(typeof(ResettableAttribute)) != null);

            foreach (var table in tablesTypes)
            {
                var tableName = table.GetCustomAttribute<TableAttribute>().tableName;

                var method = table.GetMethods().FirstOrDefault(x => x.GetCustomAttribute<RemoveWhereIdAttribute>() != null);

                if (method != null)
                {
                    m_removeMethods.Add(method);
                }
            }
        }
        public void LoadTables()
        {
            var tables = RecordsAssembly.GetTypes().Where(x => x.GetInterface("ITable") != null).ToArray();
            var orderedTables = new Type[tables.Length];
            var dontCatch = new List<Type>();

            foreach (var table in tables)
            {
                var attribute = (TableAttribute)table.GetCustomAttribute(typeof(TableAttribute), false);
                if (attribute == null)
                {
                    logger.Write(string.Format("Warning : the table type '{0}' hasn't got an attribute called 'TableAttribute'", table.Name), MessageState.WARNING);
                    continue;
                }

                if (attribute.catchAll)
                {
                    if (attribute.readingOrder >= 0)
                        orderedTables[attribute.readingOrder] = table;
                }
                else
                    dontCatch.Add(table);
            }
            foreach (var table in tables)
            {
                if (orderedTables.Contains(table) || dontCatch.Contains(table))
                    continue;

                for (var i = tables.Length - 1; i >= 0; i--)
                {
                    if (orderedTables[i] == null)
                    {
                        orderedTables[i] = table;
                        break;
                    }
                }
            }
            foreach (var type in orderedTables)
            {
                if (type == null)
                    continue;

                var reader = Activator.CreateInstance(typeof(DatabaseReader<>).MakeGenericType(type));
                var tableName = (string)reader.GetType().GetProperty("TableName").GetValue(reader);


                logger.Write("Loading " + tableName + " ...");
                var method = reader.GetType().GetMethods().FirstOrDefault(x => x.Name == "Read" && x.GetParameters().Length == 1);
                method.Invoke(reader, new object[] { this.UseProvider() });

                var elements = reader.GetType().GetProperty("Elements").GetValue(reader);

                var field = type.GetFields().FirstOrDefault(x => x.IsStatic && x.FieldType.IsGenericType && x.FieldType.GetGenericArguments()[0] == type);
                if (field != null)
                {
                    field.FieldType.GetMethod("AddRange").Invoke(field.GetValue(null), new object[] { elements }); ;
                }

            }
            logger.Write("MySQL Database loaded");
        }

        public void CloseProvider()
        {
            this.m_provider.Close();
        }

        public static DatabaseManager GetInstance()
        {
            return _self;
        }
        public void ResetTables(Assembly assembly)
        {
            var tables = assembly.GetTypes().Where(x => x.GetInterface("ITable") != null).Where(x => x.GetCustomAttribute(typeof(ResettableAttribute)) != null);

            foreach (var table in tables)
            {
                TableAttribute attribute = table.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
                Delete(attribute.tableName);
            }
        }
        private void Delete(string tableName)
        {
            Query(string.Format("DELETE from {0}", tableName), UseProvider());
        }
        public void Reload<T>() where T : ITable
        {
            DatabaseReader<T> reader = new DatabaseReader<T>();
            reader.Read(UseProvider());
            FieldInfo field = SaveTask.GetCache(typeof(T));
            field.FieldType.GetMethod("Clear").Invoke(field.GetValue(null), null);
            field.SetValue(null, reader.Elements);
        }
        public void Query(string query, MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Write("Unable to query (" + query + ")", MessageState.ERROR);
            }
        }
        /// <summary>
        /// Et supprimer dans la liste également
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        public void RemoveWhereIdMethod(long fieldValue)
        {
            Dictionary<Type, List<ITable>> tableTypes = new Dictionary<Type, List<ITable>>();

            foreach (var method in m_removeMethods)
            {
                var list = (method.Invoke(null, new object[] { fieldValue }) as IList).Cast<ITable>().ToList();
                tableTypes.Add(method.DeclaringType, list);
            }

            foreach (var key in tableTypes.Keys)
            {
                if (tableTypes[key].Count > 0)
                    tableTypes[key].RemoveInstantElements(key);
                logger.Write("Removed from " + key.Name);
            }

        }
        /// <summary>
        /// Prevent: Use only ITable type
        /// </summary>
        /// <param name="type"></param>
        public void CreateTable(Type type)
        {
            DropTable(type);

            string tableName = type.GetCustomAttribute<TableAttribute>().tableName;

            FieldInfo primaryField = type.GetFields().FirstOrDefault(x => x.GetCustomAttribute<PrimaryAttribute>() != null);

            string str = string.Empty;

            foreach (var field in type.GetFields().ToList().FindAll(x => x.GetCustomAttribute<IgnoreAttribute>() == null).FindAll(x => !x.IsStatic))
            {
                string fieldType = "mediumtext";

                if (primaryField == field)
                {
                    fieldType = "int (40)  AUTO_INCREMENT";
                }
                str += field.Name + " " + fieldType + ",";
            }

            if (primaryField != null)
                str += "PRIMARY KEY (" + primaryField.Name + ")";
            else
                str = str.Remove(str.Length - 1, 1);

            this.Query(string.Format(CREATE_TABLE, tableName, str), UseProvider());
        }
        public void DropTable(Type type)
        {
            string tableName = type.GetCustomAttribute<TableAttribute>().tableName;
            this.Query(string.Format(DROP_TABLE, tableName), UseProvider());
        }
        public void CreateTable(ITable table)
        {
            CreateTable(table.GetType());
        }
        public void WriterInstance(Type type, DatabaseAction action, ITable[] elements)
        {
            Activator.CreateInstance(typeof(DatabaseWriter<>).MakeGenericType(type), action, elements);
        }

    }
}
