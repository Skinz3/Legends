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
using Legends.Core.Utils;
using Legends.Core.DesignPattern;

namespace Legends.ORM
{
    public class DatabaseManager : Singleton<DatabaseManager>
    {
        private static Logger logger = new Logger();

        private const string CREATE_TABLE = "CREATE TABLE if not exists {0} ({1})";

        private const string DROP_TABLE = "DROP TABLE IF EXISTS {0}";

        internal MySqlConnection m_provider;

        public Assembly RecordsAssembly;

        public void Initialize(Assembly recordsAssembly, string host, string database, string user, string password)
        {

            this.m_provider = new MySqlConnection(string.Format("Server={0};UserId={1};Password={2};Database={3}", host, user, password, database));
            this.RecordsAssembly = recordsAssembly;
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

                var field = ITableManager.GetCache(type);

                if (field != null)
                {
                    field.FieldType.GetMethod("AddRange").Invoke(field.GetValue(null), new object[] { elements }); ;
                }

            }
        }

        public void CloseProvider()
        {
            this.m_provider.Close();
        }

        public void DropTables(Assembly assembly)
        {
            var tables = assembly.GetTypes().Where(x => x.GetInterface("ITable") != null);

            foreach (var table in tables)
            {
                TableAttribute attribute = table.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
                DropTable(attribute.tableName);
            }
        }
        public void DropTable(string tableName)
        {
            Query(string.Format(DROP_TABLE, tableName), UseProvider());
        }
        public void Query(string query, MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                logger.Write("Unable to query (" + query + ")", MessageState.ERROR);
            }
        }
        /// <summary>
        /// Prevent: Use only ITable type
        /// </summary>
        /// <param name="type"></param>
        public void CreateTable(Type type)
        {
            string tableName = type.GetCustomAttribute<TableAttribute>().tableName;

            PropertyInfo primaryProperty = type.GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PrimaryAttribute>() != null);

            string str = string.Empty;

            foreach (var property in type.GetProperties().ToList().FindAll(x => x.GetCustomAttribute<IgnoreAttribute>() == null))
            {
                string propertyType = "mediumtext";

                if (property.GetCustomAttribute<JsonAttribute>() != null)
                {
                    propertyType = "longtext";
                }

                if (primaryProperty == property)
                {
                    propertyType = "int (40)";
                }
                str += property.Name + " " + propertyType + ",";
            }

            if (primaryProperty != null)
                str += "PRIMARY KEY (" + primaryProperty.Name + ")";
            else
                str = str.Remove(str.Length - 1, 1);

            this.Query(string.Format(CREATE_TABLE, tableName, str), UseProvider());
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
