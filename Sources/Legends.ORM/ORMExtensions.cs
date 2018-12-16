using Legends.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Legends.ORM.Interfaces;
using Legends.ORM.IO;

namespace Legends.ORM
{
    public static class ORMExtensions
    {
        public static object Locker = new object();

        public static void AddInstantElement<T>(this T table) where T : ITable
        {
            lock (Locker)
            {
                DatabaseWriter<T>.Insert(table);
                ITableManager.AddToCache(table);
            }
        }
        public static void UpdateInstantElement<T>(this T table) where T : ITable
        {
            lock (Locker)
                DatabaseWriter<T>.Update(table);
        }
        public static void RemoveInstantElement<T>(this T table) where T : ITable
        {
            lock (Locker)
            {
                DatabaseWriter<T>.Remove(table);
                ITableManager.RemoveFromCache(table);
            }

        }
        public static void RemoveInstantElements(this IEnumerable<ITable> tables, Type type)
        {
            DatabaseManager.Instance.WriterInstance(type, DatabaseAction.Remove, tables.ToArray());

            foreach (var table in tables)
            {
                ITableManager.RemoveFromCache(table);
            }
        }
        public static void AddInstantElements(this IEnumerable<ITable> tables, Type type)
        {
            DatabaseManager.Instance.WriterInstance(type, DatabaseAction.Add, tables.ToArray());

            foreach (var table in tables)
            {
                ITableManager.AddToCache(table);
            }
        }
        public static void UpdateInstantElements(this IEnumerable<ITable> tables, Type type)
        {
            DatabaseManager.Instance.WriterInstance(type, DatabaseAction.Update, tables.ToArray());
        }
    }
}
