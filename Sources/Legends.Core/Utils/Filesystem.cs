using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Utils
{
    public class FileSystem
    {
        private static List<string> GetDirectoriesRecursively_REC(string path, List<string> results)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(path);

            foreach (string subdirectory in subdirectoryEntries)
            {
                results.Add(subdirectory);
                GetDirectoriesRecursively_REC(subdirectory, results);
            }

            return results;
        }
        public static string[] GetDirectoriesRecursively(string path)
        {
            return GetDirectoriesRecursively_REC(path, new List<string>()).ToArray();
        }


        private static List<string> GetFilesRecursively_REC(string path, List<string> results)
        {
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                results.Add(fileName);
            }

            string[] subdirectoryEntries = Directory.GetDirectories(path);

            foreach (string subdirectory in subdirectoryEntries)
            {
                GetFilesRecursively_REC(subdirectory, results);
            }
            return results;

        }
        public static string[] GetFilesRecursively(string path)
        {
            return GetFilesRecursively_REC(path, new List<string>()).ToArray();
        }



    }
}
