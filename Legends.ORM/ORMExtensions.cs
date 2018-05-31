using Legends.Core.Utils;
using Legends.ORM.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM
{
    public static class ORMExtensions
    {
        static Logger logger = new Logger();

        public static void AddInstantElement<T>(this T table, bool addtolist = true) where T : ITable
        {
            string path = DatabaseManager.Instance.GetFilename(table);

            if (File.Exists(path))
            {
                logger.Write("We override " + path, MessageState.WARNING);
            }
            string dir = Path.GetDirectoryName(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(table));
            DatabaseManager.Instance.AddToList(table);
        }
        public static void RemoveInstantElement<T>(this T table, bool removefromList = true) where T : ITable
        {
            File.Delete(DatabaseManager.Instance.GetFilename(table));
            DatabaseManager.Instance.RemoveFromList(table);
        }
        public static void RemoveInstantElements(this IEnumerable<ITable> tables, bool removefromList = true)
        {
            foreach (var element in tables)
            {
                RemoveInstantElement(element, removefromList);
            }
        }
        public static void AddInstantElements(this IEnumerable<ITable> tables, bool addtoList = true)
        {
            foreach (var element in tables)
            {
                AddInstantElement(element, addtoList);
            }
        }
    }
}
