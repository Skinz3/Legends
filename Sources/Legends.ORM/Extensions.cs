using Legends.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM
{
    public static class Extensions
    {
        public static void AddElement<T>(this T table) where T : ITable
        {
            DatabaseManager.Instance.Add(table);
        }
        public static void RemoveElement<T>(this T table) where T : ITable
        {
            DatabaseManager.Instance.Remove(table);
        }
    }
}
