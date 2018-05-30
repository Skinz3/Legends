using Legends.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Legends.ORM.Interfaces;
using Legends.ORM.Addon;
using Legends.ORM.IO;

namespace Legends
{
    public static class ORMExtensions
    {
        public static object Locker = new object();
        /// <summary>
        /// Using SaveTask.cs
        /// </summary>
        /// <param name="table"></param>
        /// <param name="addtolist"></param>
        public static void UpdateElement(this ITable table)
        {
            SaveTask.UpdateElement(table);
        }
        /// <summary>
        /// Using SaveTask.cs
        /// </summary>
        /// <param name="table"></param>
        /// <param name="addtolist"></param>
        public static void AddElement(this ITable table, bool addtolist = true)
        {
            SaveTask.AddElement(table, addtolist);
        }
        /// <summary>
        /// Using SaveTask.cs
        /// </summary>
        /// <param name="table"></param>
        /// <param name="addtolist"></param>
        public static void RemoveElement(this ITable table, bool removefromlist = true)
        {
            SaveTask.RemoveElement(table, removefromlist);
        }

        public static void AddInstantElement<T>(this T table, bool addtolist = true) where T : ITable
        {
            lock (Locker)
            {
                DatabaseWriter<T>.InstantInsert(table);
                if (addtolist)
                    SaveTask.AddToList(table);
            }
        }
        public static void UpdateInstantElement<T>(this T table) where T : ITable
        {
            lock (Locker)
                DatabaseWriter<T>.InstantUpdate(table);
        }
        public static void RemoveInstantElement<T>(this T table, bool removefromList = true) where T : ITable
        {
            lock (Locker)
            {
                DatabaseWriter<T>.InstantRemove(table);
                if (removefromList)
                    SaveTask.RemoveFromList(table);
            }

        }
        public static void RemoveInstantElements(this IEnumerable<ITable> tables, Type type, bool removefromList = true)
        {
            DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Remove, tables.ToArray());

            if (removefromList)
            {
                foreach (var table in tables)
                {
                    SaveTask.RemoveFromList(table);
                }
            }
        }
        public static void AddInstantElements(this IEnumerable<ITable> tables, Type type, bool addtoList = true)
        {

            DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Add, tables.ToArray());

            if (addtoList)
            {
                foreach (var table in tables)
                {
                    SaveTask.AddToList(table);
                }
            }
        }
        public static void UpdateInstantElements(this IEnumerable<ITable> tables, Type type)
        {
            DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Update, tables.ToArray());
        }
    }
}
