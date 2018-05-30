using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Legends.Core;
using System.Threading.Tasks;
using Legends.ORM.Interfaces;
using Legends.ORM.Attributes;
using System.Numerics;
using Legends.ORM.Addon;

namespace Legends.ORM.IO
{
    public class DatabaseReader<T>
      where T : ITable
    {
        // FIELDS
        private const char LIST_SPLITER = ',';
        private const char DICTIONARY_SPLITER = ';';

        private List<T> m_elements;
        private MySqlDataReader m_reader;
        private FieldInfo[] m_fields;
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
            this.m_fields = typeof(T).GetFields().Where(field =>
                field.GetCustomAttribute(typeof(IgnoreAttribute), false) == null &&
                !field.IsStatic).OrderBy(x => x.MetadataToken).ToArray();


            this.m_methods = typeof(T).GetMethods().Where(method => method.IsStatic &&
                method.GetParameters().Length == 1 &&
                method.GetParameters()[0].ParameterType == typeof(string)).ToArray();

            if (typeof(T).GetCustomAttribute(typeof(TableAttribute)) == null)
                throw new Exception(string.Empty);

            this.m_tableName = (typeof(T).GetCustomAttribute(typeof(TableAttribute)) as TableAttribute).tableName;
        }

        private void ReadTable(MySqlConnection connection, string parameter)
        {
            var command = new MySqlCommand(parameter, connection);
            this.m_reader = command.ExecuteReader();
            while (this.m_reader.Read())
            {
                var obj = new object[this.m_fields.Length];
                for (var i = 0; i < this.m_fields.Length; i++)
                    obj[i] = this.m_reader[i];

                this.VerifyFieldsType(obj);


                this.m_elements.Add((T)Activator.CreateInstance(typeof(T), obj));


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
        private void VerifyFieldsType(object[] obj)
        {
            for (var i = 0; i < this.m_fields.Length; i++)
            {
                if (obj[i].GetType() == this.m_fields[i].FieldType)
                    continue;

                XmlAttribute xmlAttribute;

                MethodInfo method = null;

                if (this.m_fields[i].FieldType.IsGenericType)
                {

                    var parameters = this.m_fields[i].FieldType.GetGenericArguments();

                    switch (parameters.Length)
                    {
                        case 1: // List

                            var elements = (obj[i].ToString()).Split(new char[] { LIST_SPLITER }, StringSplitOptions.RemoveEmptyEntries);

                            var newList = Activator.CreateInstance(typeof(List<>).MakeGenericType(parameters));
                            method = newList.GetType().GetMethod("Add");

                            xmlAttribute = this.m_fields[i].GetCustomAttribute<XmlAttribute>();

                            if (xmlAttribute != null)
                            {
                                var xmlObject = obj[i].ToString().XMLDeserialize(this.m_fields[i].FieldType);
                                obj[i] = Convert.ChangeType(xmlObject, this.m_fields[i].FieldType);
                                continue;
                            }

                            var desezializeMethod = parameters[0].GetMethod("Deserialize");

                            if (desezializeMethod != null)
                            {

                                elements = (obj[i] as string).Split(new char[] { LIST_SPLITER }, StringSplitOptions.RemoveEmptyEntries);

                                foreach (var element in elements)
                                    method.Invoke(newList, new object[] { desezializeMethod.Invoke(null, new object[] { element }) });
                            }
                            else
                            {
                                foreach (var element in elements)
                                    method.Invoke(newList, new object[] { Convert.ChangeType(element, parameters[0]) });
                            }

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

                xmlAttribute = this.m_fields[i].GetCustomAttribute<XmlAttribute>();

                if (xmlAttribute != null)
                {
                    var xmlObject = obj[i].ToString().XMLDeserialize(this.m_fields[i].FieldType);
                    obj[i] = Convert.ChangeType(xmlObject, this.m_fields[i].FieldType);
                    continue;
                }
                method = this.m_fields[i].FieldType.GetMethod("Deserialize");

                if (method != null)
                {
                    obj[i] = method.Invoke(null, new object[] { obj[i] });
                    continue;
                }
                if (this.m_fields[i].FieldType.BaseType == typeof(Enum))
                {
                    obj[i] = Enum.Parse(m_fields[i].FieldType, obj[i].ToString());
                }
                if (this.m_fields[i].FieldType == typeof(Vector2))
                {
                    obj[i] = SQLVector2.Deserialize(obj[i].ToString()).ToVector2();
                }
                try { obj[i] = Convert.ChangeType(obj[i], this.m_fields[i].FieldType); }
                catch
                {
                    string exception = string.Format("Unknown constructor for '{0}', ({1}) if its an XmlField, FieldType must got empty constructor.", this.m_fields[i].FieldType.Name, this.m_fields[i].Name);
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
            reader.ReadTable(DatabaseManager.GetInstance().UseProvider(), string.Format("SELECT * FROM `{0}` WHERE {1}", reader.m_tableName, condition));
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
            return new DatabaseReader<T>().Count(DatabaseManager.GetInstance().UseProvider(), condition);

        }
    }
}

