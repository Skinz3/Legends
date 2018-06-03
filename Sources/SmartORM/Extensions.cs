using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM
{
    public static class Extensions
    {
        public static void AddElement(this ITable table, bool addtolist = true)
        {
            DatabaseManager.Instance.Add(table);

            if (addtolist)
            {
                ITableManager.Instance.AddToCache(table);
            }
        }
        public static void RemoveElement(this ITable table, bool removefromList = true)
        {
            DatabaseManager.Instance.Remove(table);

            if (removefromList)
            {
                ITableManager.Instance.RemoveFromCache(table);
            }
        }
        public static void UpdateElement(this ITable table)
        {
            DatabaseManager.Instance.Update(table);
        }
        public static void RemoveElements(this IEnumerable<ITable> tables, bool removefromList = true)
        {
            foreach (var table in tables)
            {
                RemoveElement(table, removefromList);
            }
        }
        public static void AddElements(this IEnumerable<ITable> tables, bool addtoList = true)
        {
            foreach (var table in tables)
            {
                AddElement(table, addtoList);
            }
        }
        public static void UpdateElements(this IEnumerable<ITable> tables)
        {
            foreach (var table in tables)
            {
                UpdateElement(table);
            }
        }
    }
}
