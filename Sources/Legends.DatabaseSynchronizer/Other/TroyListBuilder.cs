using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer.Other
{
    public class TroyListBuilder
    {
        static Logger logger = new Logger();

        public static void GenerateTroybinlist(string fileOutputPath, string extractedRafs)
        {
            if (!Directory.Exists(extractedRafs))
            {
                logger.Write("TroybinList is not generated. " + extractedRafs + " directory not founded. This wont affect database.", MessageState.WARNING);
                return;
            }

            if (File.Exists(fileOutputPath))
            {
                logger.Write("TroybinList is not generated. " + fileOutputPath + " already exists. . This wont affect database.", MessageState.WARNING);
                return;
            }
           
            List<string> results = new List<string>();
            var files = FileSystem.GetFilesRecursively(extractedRafs);

            foreach (var file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".troybin")
                {
                    results.Add(Path.GetFileName(file));
                }
            }

            logger.Write(Path.GetFileName(fileOutputPath) + " Generated");

            File.WriteAllLines(fileOutputPath, results.ToArray());
        }

    }
}
