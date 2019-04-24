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
using MySql.Data.MySqlClient;
using Legends.DatabaseSynchronizer.Other;

namespace Legends.DatabaseSynchronizer
{
    /// <summary>
    /// League Of Legends, Version 4.20
    /// </summary>
    class Program
    {
        public const string LeagueOfLegendsPath = @"C:\Emulateur LoL\League of Legends 4.20\League of Legends\";

        public const string ExtractedRAFOutputPath = @"C:\Emulateur LoL\RAF 4.20\";

        static Logger logger = new Logger();


        static void Main(string[] args)
        {
            logger.OnStartup();

            DatabaseManager.Instance.Initialize(Assembly.GetEntryAssembly(), "127.0.0.1", "legends", "root", "");

            RafManager manager = new RafManager(LeagueOfLegendsPath);

            TroyListBuilder.GenerateTroybinlist("troybin.txt", ExtractedRAFOutputPath);

            DatabaseManager.Instance.DropTables(Assembly.GetAssembly(typeof(AIUnitRecord)));

            //  JSONHashes hashes = new JSONHashes(Environment.CurrentDirectory + "/items.json","ITEMS");  

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
