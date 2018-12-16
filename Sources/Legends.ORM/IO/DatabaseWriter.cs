using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Legends;
using Legends.ORM.Interfaces;
using Legends.ORM.Attributes;
using Legends.Core;

namespace Legends.ORM.IO
{
    public class DatabaseWriter<T>
       where T : ITable
    {
        private const short MAX_ADDING_LINES = 250;

        private const string ADD_ELEMENTS = "INSERT INTO `{0}` VALUES\n{1}";
        private const string UPDATE_ELEMENTS = "UPDATE `{0}` SET {1} WHERE `{2}` = {3}";
        private const string REMOVE_ELEMENTS = "DELETE FROM `{0}` WHERE `{1}` = {2}";

        private const string LIST_SPLITTER = ",";
        private const string DICTIONARY_SPLITTER = ";";

        private string m_tableName;
        private MySqlCommand m_command;

        private List<PropertyInfo> m_properties;

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
                this.m_properties = GetAddProperties(typeof(T));
            if (action == DatabaseAction.Update)
                this.m_properties = GetUpdateProperties(typeof(T));

            this.m_tableName = (typeof(T).GetCustomAttribute(typeof(TableAttribute)) as TableAttribute).tableName;

            if (action != DatabaseAction.Add)
                this.GetPrimaryProperty();

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


                this.m_command = new MySqlCommand(command, DatabaseManager.Instance.UseProvider());
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
                    var values = this.m_properties.ConvertAll<string>(property => string.Format("{0} = {1}", property.Name, this.GetPropertyValue(property, element)));
                    var command = string.Format(UPDATE_ELEMENTS, this.m_tableName, string.Join(", ", values), this.GetPrimaryProperty().Name, this.GetPrimaryProperty().GetValue(element));

                    this.m_command = new MySqlCommand(command, DatabaseManager.Instance.UseProvider());
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
                    var command = string.Format(REMOVE_ELEMENTS, this.m_tableName, this.GetPrimaryProperty().Name, this.GetPrimaryProperty().GetValue(element));
                    this.m_command = new MySqlCommand(command, DatabaseManager.Instance.UseProvider());
                    this.m_command.ExecuteNonQuery();
                }
            }
        }

        private string CreateElement(ITable element)
        {

            var values = this.m_properties.ConvertAll<string>(property => this.GetPropertyValue(property, element));
            return string.Join(", ", values);
        }

        private string GetPropertyValue(PropertyInfo property, ITable element)
        {
            var value = property.GetValue(element);
            if (property.PropertyType == typeof(DateTime))
            {
                value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (property.GetCustomAttribute(typeof(JsonAttribute)) != null)
            {
                value = property.GetValue(element).JsonSerialize();
            }
            else
            {
                if (property.PropertyType.IsGenericType)
                {
                    var arguments = property.PropertyType.GetGenericArguments();

                    switch (arguments.Length)
                    {
                        case 1: // List
                            var values = (IList)property.GetValue(element);

                            if (property.GetCustomAttribute(typeof(JsonAttribute)) != null)
                            {
                                var newValues = new List<string>();

                                foreach (var ele in values)
                                {
                                    newValues.Add(ele.JsonSerialize());
                                }

                                value = string.Join(LIST_SPLITTER, newValues);
                                break;
                            }

                            value = string.Join(LIST_SPLITTER, values);
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

            if (value != null)
                value = value.ToString().Replace("'", "''");

            return string.Format("'{0}'", value);
        }

        private PropertyInfo GetPrimaryProperty()
        {
            var properties = typeof(T).GetProperties().Where(property => property.GetCustomAttribute(typeof(PrimaryAttribute), false) != null);

            if (properties.Count() != 1)
            {
                if (properties.Count() == 0)
                    throw new Exception(string.Format("The Table '{0}' hasn't got a primary property", typeof(T).FullName));

                if (properties.Count() > 1)
                    throw new Exception(string.Format("The Table '{0}' has too much primary properties", typeof(T).FullName));
            }
            return properties.First();
        }
        public static List<PropertyInfo> GetUpdateProperties(Type type)
        {
            return type.GetProperties().Where(property => property.GetCustomAttribute(typeof(IgnoreAttribute)) == null && property.GetCustomAttribute(typeof(UpdateAttribute), false) != null).OrderBy(x => x.MetadataToken).ToList();
        }
        public static List<PropertyInfo> GetAddProperties(Type type)
        {
            return type.GetProperties().Where(property => property.GetCustomAttribute(typeof(IgnoreAttribute)) == null).OrderBy(x => x.MetadataToken).ToList();
        }

        public static void Update(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Update, new ITable[] { item });
        }

        public static void Update(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Update, items.ToArray() as ITable[]);
        }

        public static void Insert(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Add, new ITable[] { item });
        }
        public static void Insert(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Add, items.ToArray() as ITable[]);
        }

        public static void Remove(T item)
        {
            new DatabaseWriter<T>(DatabaseAction.Remove, new ITable[] { item });
        }
        public static void Remove(IEnumerable<T> items)
        {
            new DatabaseWriter<T>(DatabaseAction.Remove, items.ToArray() as ITable[]);
        }


        public static void CreateTable()
        {
            DatabaseManager.Instance.CreateTable(typeof(T));
        }

    }

    public enum DatabaseAction
    {
        Add,
        Update,
        Remove
    }
}
