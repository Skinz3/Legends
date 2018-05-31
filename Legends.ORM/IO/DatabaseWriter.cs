using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Legends;
using Legends.Core;
using Legends.ORM.Interfaces;
using Legends.ORM.Attributes;
using System.Numerics;
using Legends.ORM.Addon;

namespace Legends.ORM.IO
{
    public class DatabaseWriter<T>
       where T : ITable
    {
        // FIELDS
        private const short MAX_ADDING_LINES = 250;

        private const string ADD_ELEMENTS = "INSERT INTO `{0}` VALUES\n{1}";
        private const string UPDATE_ELEMENTS = "UPDATE `{0}` SET {1} WHERE `{2}` = {3}";
        private const string REMOVE_ELEMENTS = "DELETE FROM `{0}` WHERE `{1}` = {2}";

        private const string LIST_SPLITTER = ",";
        private const string DICTIONARY_SPLITTER = ";";

        private string m_tableName;
        private MySqlCommand m_command;

        private List<FieldInfo> m_fields;
        private List<MethodInfo> m_methods;

        // PROPERTIES

        // CONSTRUCTORS
        public DatabaseWriter(DatabaseAction action, params ITable[] elements)
        {
            this.Initialize(action);

            switch (action)
            {

                case DatabaseAction.Add:
                    this.AddElements(elements);
                    return;

                case DatabaseAction.Update:
                    this.UpdateElements(elements);
                    return;
                case DatabaseAction.Remove:
                    this.DeleteElements(elements);
                    return;


            }
        }
        private void Initialize(DatabaseAction action)
        {
            if (action == DatabaseAction.Add)
                this.m_fields = GetAddFields(typeof(T));
            if (action == DatabaseAction.Update)
                this.m_fields = GetUpdateFields(typeof(T));

            this.m_methods = typeof(T).GetMethods().Where(method => method.GetCustomAttribute(typeof(TranslationAttribute), false) != null &&
                !(method.GetCustomAttribute(typeof(TranslationAttribute), false) as TranslationAttribute).readingMode).ToList();

            this.m_tableName = (typeof(T).GetCustomAttribute(typeof(TableAttribute)) as TableAttribute).tableName;

            if (action != DatabaseAction.Add)
                this.GetPrimaryField();

        }

        private void AddElements(ITable[] elements)
        {
            var values = new List<string>();

            var str = string.Empty;

            for (var i = 0; i < elements.Length / MAX_ADDING_LINES + 1; i++)
            {
                str = string.Empty;

                for (var j = i * MAX_ADDING_LINES; j < (i + 1) * MAX_ADDING_LINES; j++)
                {
                    if (str != string.Empty && elements.Length > j)
                        str += ",\n";

                    if (elements.Length <= j)
                        break;
                    str += string.Format("({0})", this.CreateElement(elements[j]));
                }

                if (str != string.Empty)
                {
                    values.Add(string.Format("{0};", str));
                }
            }

            foreach (var element in values)
            {
                var command = string.Format(ADD_ELEMENTS, this.m_tableName, element);
                this.m_command = new MySqlCommand(command, DatabaseManager.GetInstance().UseProvider());
                try
                {
                    this.m_command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to add element to database (" + m_tableName + ") " +
                         ex.Message);
                }
            }
        }
        private void UpdateElements(ITable[] elements)
        {
            foreach (var element in elements)
            {
                lock (element)
                {
                    var values = this.m_fields.ConvertAll<string>(field => string.Format("{0} = {1}", field.Name, this.GetFieldValue(field, element)));
                    var command = string.Format(UPDATE_ELEMENTS, this.m_tableName, string.Join(", ", values), this.GetPrimaryField().Name, this.GetPrimaryField().GetValue(element));

                    this.m_command = new MySqlCommand(command, DatabaseManager.GetInstance().UseProvider());
                    this.m_command.ExecuteNonQuery();
                }
            }
        }
        private void DeleteElements(ITable[] elements)
        {
            foreach (var element in elements)
            {
                lock (element)
                {
                    var command = string.Format(REMOVE_ELEMENTS, this.m_tableName, this.GetPrimaryField().Name, this.GetPrimaryField().GetValue(element));
                    this.m_command = new MySqlCommand(command, DatabaseManager.GetInstance().UseProvider());
                    this.m_command.ExecuteNonQuery();
                }
            }
        }

        private string CreateElement(ITable element)
        {

            var values = this.m_fields.ConvertAll<string>(field => this.GetFieldValue(field, element));
            return string.Join(", ", values);
        }

        private string GetFieldValue(FieldInfo field, ITable element)
        {
            var value = field.GetValue(element);
            if (field.GetCustomAttribute(typeof(TranslationAttribute), false) != null)
            {
                var method = field.FieldType.GetMethod("ToString");

                value = method.Invoke(field.GetValue(element), new object[] { });
            }
            else if (field.FieldType == typeof(DateTime))
            {
                value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (this.m_methods.FirstOrDefault(x => x.IsStatic && x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == field.FieldType) != null)
            {
                var method = this.m_methods.FirstOrDefault(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == field.FieldType);

                value = method.Invoke(null, new object[] { field.GetValue(element) });
            }
            else if (field.GetCustomAttribute(typeof(XmlAttribute)) != null)
            {
                value = field.GetValue(element).XMLSerialize();
            }
            if (field.FieldType == typeof(Vector2))
            {
                value = new SQLVector2((Vector2)field.GetValue(element)).ToString();
            }
            if (field.FieldType == typeof(Vector3))
            {
                value = new SQLVector3((Vector3)field.GetValue(element)).ToString();
            }
            else
            {
                if (field.FieldType.IsGenericType)
                {
                    var arguments = field.FieldType.GetGenericArguments();

                    switch (arguments.Length)
                    {
                        case 1: // List
                            var values = (IList)field.GetValue(element);

                            if (field.GetCustomAttribute(typeof(XmlAttribute)) != null)
                            {
                                var newValues = new List<string>();

                                foreach (var ele in values)
                                {
                                    newValues.Add(ele.XMLSerialize());
                                }
                                value = string.Join(LIST_SPLITTER, newValues);
                                break;
                            }

                            if (arguments[0].GetMethod("Deserialize") != null)
                            {
                                var method = arguments[0].GetMethod("ToString");

                                var newValues = new List<string>();

                                foreach (var ele in values)
                                    newValues.Add((string)method.Invoke(ele, new object[] { }));

                                value = string.Join(LIST_SPLITTER, newValues);
                                break;
                            }

                            value = "";

                            var array = new List<string>();

                            foreach (var ele in values)
                                array.Add(ele.ToString());

                            value = string.Join(LIST_SPLITTER, array);
                            break;

                        case 2:
                            var list = new List<string>();

                            IDictionary dic = (IDictionary)value;

                            foreach (DictionaryEntry entry in dic)
                            {
                                list.Add(string.Format("{0},{1}", entry.Key, entry.Value));
                            }
                            value = string.Join(DICTIONARY_SPLITTER, list);
                            break;
                    }
                }
            }
            value = value.ToString().Replace("'", "''");
            return string.Format("'{0}'", value);
        }

        private FieldInfo GetPrimaryField()
        {
            var fields = typeof(T).GetFields().Where(field => field.GetCustomAttribute(typeof(PrimaryAttribute), false) != null);

            if (fields.Count() != 1)
            {
                if (fields.Count() == 0)
                    throw new Exception(string.Format("The Table '{0}' hasn't got a primary field", typeof(T).FullName));

                if (fields.Count() > 1)
                    throw new Exception(string.Format("The Table '{0}' has too much primary fields", typeof(T).FullName));
            }
            return fields.First();
        }
        public static List<FieldInfo> GetUpdateFields(Type type)
        {
            return type.GetFields().Where(field => !field.IsStatic && field.GetCustomAttribute(typeof(IgnoreAttribute), false) == null && field.GetCustomAttribute(typeof(UpdateAttribute), false) != null).OrderBy(x => x.MetadataToken).ToList();
        }
        public static List<FieldInfo> GetAddFields(Type type)
        {
            return type.GetFields().Where(field => !field.IsStatic && field.GetCustomAttribute(typeof(IgnoreAttribute), false) == null).OrderBy(x => x.MetadataToken).ToList();
        }

        public static void InstantUpdate(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Update, new ITable[] { item });
        }

        public static void InstantUpdate(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Update, items.ToArray() as ITable[]);
        }

        public static void InstantInsert(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Add, new ITable[] { item });
        }
        public static void InstantInsert(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Add, items.ToArray() as ITable[]);
        }

        public static void InstantRemove(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Remove, new ITable[] { item });
        }
        public static void InstantRemove(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Remove, items.ToArray() as ITable[]);
        }


        public static void CreateTable()
        {
            DatabaseManager.GetInstance().CreateTable(typeof(T));
        }

    }

    public enum DatabaseAction
    {
        Add,
        Update,
        Remove
    }
}
