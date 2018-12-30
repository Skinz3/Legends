using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Legends.ORM.Interfaces;
using Legends.ORM.Attributes;
using Legends;
using Legends.Core;
using System.Globalization;

namespace Legends.ORM.IO
{
    public class DatabaseReader<T>
      where T : ITable
    {
        private const char LIST_SPLITER = ',';
        private const char DICTIONARY_SPLITER = ';';

        private List<T> m_elements;
        private MySqlDataReader m_reader;
        private PropertyInfo[] m_properties;
        private MethodInfo[] m_methods;

        private string m_tableName;

        // PROPERTIES
        public string TableName { get { return this.m_tableName; } }
        public List<T> Elements { get { return this.m_elements; } }

        // CONSTRUCTORS
        public DatabaseReader()
        {
            this.m_elements = new List<T>();

            this.Initialize();
        }

        // METHODS
        private void Initialize()
        {
            this.m_properties = typeof(T).GetProperties().Where(property =>
                property.GetCustomAttribute(typeof(IgnoreAttribute), false) == null).OrderBy(x => x.MetadataToken).ToArray();


            if (typeof(T).GetCustomAttribute(typeof(TableAttribute)) == null)
                throw new Exception(string.Empty);

            this.m_tableName = (typeof(T).GetCustomAttribute(typeof(TableAttribute)) as TableAttribute).tableName;
        }

        private void ReadTable(MySqlConnection connection, string parameter)
        {
            var command = new MySqlCommand(parameter, connection);

            try
            {
                this.m_reader = command.ExecuteReader();
            }
            catch
            {
                DatabaseManager.Instance.CreateTable(typeof(T));
                ReadTable(connection, parameter);
                return;
            }
            while (this.m_reader.Read())
            {
                var obj = new object[this.m_properties.Length];
                for (var i = 0; i < this.m_properties.Length; i++)
                    obj[i] = this.m_reader[i];

                this.VerifyPropertiesTypes(obj);

                var typeObj = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < m_properties.Length; i++)
                {
                    m_properties[i].SetValue(typeObj, obj[i]);
                }

                this.m_elements.Add(typeObj);


            }
            this.m_reader.Close();
        }

        public void Read(MySqlConnection connection)
        {
            this.ReadTable(connection, string.Format("SELECT * FROM `{0}` WHERE 1", this.m_tableName));
        }
        /// <param name="condition">WHERE {0}</param>
        public void Read(MySqlConnection connection, string condition)
        {
            this.ReadTable(connection, string.Format("SELECT * FROM `{0}` WHERE {1}", this.m_tableName, condition));
        }
        public long Count(MySqlConnection connection, string condition)
        {
            MySqlCommand cmd = new MySqlCommand(string.Format("SELECT COUNT(*) FROM `{0}` WHERE {1}", this.m_tableName, condition), connection);
            return (long)cmd.ExecuteScalar();
        }
        private void VerifyPropertiesTypes(object[] obj)
        {
            for (var i = 0; i < this.m_properties.Length; i++)
            {
                if (obj[i].GetType() == this.m_properties[i].PropertyType)
                    continue;

                JsonAttribute jsonAttribute;

                MethodInfo method = null;

                if (this.m_properties[i].PropertyType.IsGenericType)
                {

                    var parameters = this.m_properties[i].PropertyType.GetGenericArguments();

                    switch (parameters.Length)
                    {
                        case 1: // List

                            var elements = (obj[i].ToString()).Split(new char[] { LIST_SPLITER }, StringSplitOptions.RemoveEmptyEntries);

                            var newList = Activator.CreateInstance(typeof(List<>).MakeGenericType(parameters));
                            method = newList.GetType().GetMethod("Add");

                            jsonAttribute = this.m_properties[i].GetCustomAttribute<JsonAttribute>();

                            if (jsonAttribute != null)
                            {
                                var jsonObject = obj[i].ToString().JsonDeserialize(this.m_properties[i].PropertyType);
                                obj[i] = Convert.ChangeType(jsonObject, this.m_properties[i].PropertyType);
                                continue;
                            }

                            foreach (var element in elements)
                                method.Invoke(newList, new object[] { Convert.ChangeType(element, parameters[0]) });

                            obj[i] = newList;
                            continue;

                        case 2: // Dictionary
                            elements = (obj[i] as string).Split(new char[] { DICTIONARY_SPLITER }, StringSplitOptions.RemoveEmptyEntries);

                            var newDictionary = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(parameters));

                            method = newDictionary.GetType().GetMethod("Add");

                            foreach (var pair in elements)
                            {
                                string[] split = pair.Split(',');
                                object key = null;

                                if (parameters[0].BaseType == typeof(Enum))
                                {
                                    key = Enum.Parse(parameters[0], split[0]);
                                }
                                else
                                    key = Convert.ChangeType(split[0], parameters[0]);

                                object value = Convert.ChangeType(split[1], parameters[1]);
                                method.Invoke(newDictionary, new object[2] { key, value });
                            }
                            obj[i] = newDictionary;
                            continue;
                    }
                }

                jsonAttribute = this.m_properties[i].GetCustomAttribute<JsonAttribute>();

                if (jsonAttribute != null)
                {
                    var jsonObject = obj[i].ToString().JsonDeserialize(this.m_properties[i].PropertyType);
                    obj[i] = Convert.ChangeType(jsonObject, this.m_properties[i].PropertyType);
                    continue;
                }
                if (this.m_properties[i].PropertyType.BaseType == typeof(Enum))
                {
                    obj[i] = Enum.Parse(m_properties[i].PropertyType, obj[i].ToString());
                }
                try { obj[i] = Convert.ChangeType(obj[i], this.m_properties[i].PropertyType,CultureInfo.InvariantCulture); }
                catch
                {
                    string exception = string.Format("Unknown constructor for '{0}', ({1}) if its an Json property, PropertyType must got empty constructor.", this.m_properties[i].PropertyType.Name, this.m_properties[i].Name);
                    Console.WriteLine(exception);
                    throw new Exception(exception);
                }
            }

        }
        /// <summary>
        /// Using DataManager.UseProvider(); for connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<T> Read(string condition)
        {

            DatabaseReader<T> reader = new DatabaseReader<T>();
            reader.ReadTable(DatabaseManager.Instance.UseProvider(), string.Format("SELECT * FROM `{0}` WHERE {1}", reader.m_tableName, condition));
            return reader.Elements;

        }
        public static T ReadFirst(string condition)
        {
            List<T> elements = Read(condition);
            if (elements.Count > 0)
                return elements.First();
            else
                return default(T);
        }
        public static long Count(string condition)
        {
            return new DatabaseReader<T>().Count(DatabaseManager.Instance.UseProvider(), condition);

        }
    }
}

