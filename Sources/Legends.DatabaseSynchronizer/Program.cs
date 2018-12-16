using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Legends.Core.Geometry;
using Legends.Records;
using System.Reflection;
using Legends.Core;
using Legends.Core.Utils;
using System.Numerics;
using Legends.Core.IO.NavGrid;
using System.Diagnostics;
using Legends.Core.IO.RAF;
using Legends.Core.IO;
using Legends.Core.IO.MOB;
using System.Collections.Concurrent;
using Legends.Core.IO.CFG;
using Legends.DatabaseSynchronizer.CustomSyncs;
using Legends.Core.IO.Inibin;
using Legends.ORM;

namespace Legends.DatabaseSynchronizer
{
    /// <summary>
    /// League Of Legends, Version 4.20
    /// </summary>
    class Program
    {
        public const string LeagueOfLegendsPath = @"C:\Users\Skinz\Desktop\Emulateur LoL\League of Legends 4.20\League of Legends\";
        public const string SmartFileOutputPath = @"C:\Users\Skinz\Desktop\Emulateur LoL\Legends\Build\database.smart";

        static Logger logger = new Logger();


        static void Main(string[] args)
        {
            DatabaseManager.Instance.Initialize(Assembly.GetEntryAssembly(), "127.0.0.1", "legends", "root", "");

            RafManager manager = new RafManager(LeagueOfLegendsPath);

            var test = manager.GetFiles("ExpCurve.inibin");

          //  JSONHashes hashes = new JSONHashes(Environment.CurrentDirectory + "/items.json","ITEMS");  
            logger.OnStartup();
            var recordAssembly = Assembly.GetAssembly(typeof(AIUnitRecord));

            BuildingSynchronizer.Synchronize(manager);
            MapSynchronizer.Synchronize(manager);
            ExperienceSynchronizer.Synchronize(manager);

            manager.Dispose();
            InibinSynchronizer synchronizer = new InibinSynchronizer(LeagueOfLegendsPath, recordAssembly);
            synchronizer.Sync();

            Console.Read();

        }





    }
}
