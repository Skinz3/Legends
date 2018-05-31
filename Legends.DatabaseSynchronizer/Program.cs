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
using Legends.ORM;
using Legends.Core.Utils;
using Legends.Core.JSON;
using System.Numerics;
using Legends.Core.IO.NavGrid;
using System.Diagnostics;
using Legends.Core.IO.RAF;
using Legends.Core.IO;
using Legends.Core.IO.MOB;
using Legends.ORM.Addon;

namespace Legends.DatabaseSynchronizer
{
    /// <summary>
    /// League Of Legends, Version 4.20
    /// </summary>
    class Program
    {
        public const string LeagueOfLegendsPath = @"C:\Users\Skinz\Desktop\Emulateur LoL\League of Legends 4.20\League of Legends\";

        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            RafManager test = new RafManager(LeagueOfLegendsPath);
            var aa = test.GetFiles("objects.cfg");
            /* JSONHashes hashes = new JSONHashes(Environment.CurrentDirectory + "/skins.json","SKINS");  */
            logger.OnStartup();
            var recordAssembly = Assembly.GetAssembly(typeof(ChampionRecord));
            DatabaseManager manager = new DatabaseManager(recordAssembly, "127.0.0.1", "legends", "root", "");
            manager.UseProvider();

            SynchronizeMaps();
            SynchronizeMapObjects();

            InibinSynchronizer synchronizer = new InibinSynchronizer(LeagueOfLegendsPath, recordAssembly);
            synchronizer.Sync();

            Console.Read();

        }

        private static void SynchronizeMapObjects()
        {
            List<MapObjectRecord> objects = new List<MapObjectRecord>();

            RafManager manager = new RafManager(LeagueOfLegendsPath);

            var mobFiles = manager.GetFiles(".mob");



            foreach (var file in mobFiles)
            {
                var mob = new MOBFile(new MemoryStream(file.GetContent(true)));
                int mapId = int.Parse(new string(file.Path.Split('/')[1].Skip(3).ToArray()));

                foreach (var obj in mob.Objects)
                {
                    MapObjectRecord record = new MapObjectRecord(mapId, obj.Name, obj.Position, obj.Type,
                        obj.Scale, obj.Rotation);
                    objects.Add(record);

                }
            }
            DatabaseManager.GetInstance().CreateTable(typeof(MapObjectRecord));
            objects.AddInstantElements(typeof(MapObjectRecord));

            logger.Write("Map objects synchronized");
            manager.Dispose();
        }
        /// <summary>
        /// LEVELS/map11
        /// </summary>
        private static void SynchronizeMaps()
        {
            RafManager manager = new RafManager(LeagueOfLegendsPath);
            var navGrids = manager.GetFiles("AIPath.aimesh_ngrid");

            List<MapRecord> records = new List<MapRecord>();

            List<int> ids = new List<int>();
            foreach (var navGrid in navGrids)
            {
                NavGridFile grid = NavGridReader.ReadBinary(navGrid.GetContent(true));

                MapRecord record = new MapRecord();
                record.Name = navGrid.Path.Split('/')[1];
                record.Id = int.Parse(new string(record.Name.Skip(3).ToArray()));
                record.MiddleOfMap = grid.MiddleOfMap;
                record.Width = grid.MapWidth;
                record.Height = grid.MapHeight;

                if (ids.Contains(record.Id) == false)
                {
                    records.Add(record);
                    ids.Add(record.Id);
                }
            }

            DatabaseManager.GetInstance().CreateTable(typeof(MapRecord));
            records.AddInstantElements(typeof(MapRecord));
            manager.Dispose();
            logger.Write("Map synchronized");

        }

    }
}
