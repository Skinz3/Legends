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
using System.Numerics;
using Legends.Core.IO.NavGrid;
using System.Diagnostics;
using Legends.Core.IO.RAF;
using Legends.Core.IO;
using Legends.Core.IO.MOB;

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
            RafManager manager = new RafManager(LeagueOfLegendsPath);

            var test = manager.GetFiles("ExpCurve.inibin");


            /* JSONHashes hashes = new JSONHashes(Environment.CurrentDirectory + "/skins.json","SKINS");  */
            logger.OnStartup();
            var recordAssembly = Assembly.GetAssembly(typeof(AIUnitRecord));

            DatabaseManager.Instance.Initialize(Environment.CurrentDirectory, recordAssembly);
            DatabaseManager.Instance.LoadTables();

            SynchronizeMaps();
            SynchronizeExperience();
            InibinSynchronizer synchronizer = new InibinSynchronizer(LeagueOfLegendsPath, recordAssembly);
            synchronizer.Sync();

            Console.Read();

        }
        private static void SynchronizeExperience()
        {
            float[] cumulativeExps = new float[]
            {
                0f,
                280f,
                660f,
                1140f,
                1720f,
                2400f,
                3180f,
                4060f,
                5040f,
                6120f,
                7300f,
                8580f,
                9960f,
                11440f,
                13020f,
                14700f,
                16480f,
                18360f,
            };
            List<ExperienceRecord> records = new List<ExperienceRecord>();

            int level = 1;
            for (int i = 0; i < cumulativeExps.Length; i++)
            {
                records.Add(new ExperienceRecord(level, cumulativeExps[i]));
                level++;
            }
            records.AddInstantElements();

            logger.Write("Experiences synchronized");
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

                var file = manager.GetFile("LEVELS/" + record.Name + "/Scene/MapObjects.mob");

                if (file != null)
                {
                    List<MapObjectRecord> objects = new List<MapObjectRecord>();
                    var mob = new MOBFile(new MemoryStream(file.GetContent(true)));
                    int mapId = int.Parse(new string(file.Path.Split('/')[1].Skip(3).ToArray()));
                    foreach (var obj in mob.Objects)
                    {
                        objects.Add(new MapObjectRecord(obj.Name, obj.Position, obj.Type,
                            obj.Scale, obj.Rotation));

                    }

                    record.Objects = objects.ToArray();
                }


                if (ids.Contains(record.Id) == false)
                {
                    records.Add(record);
                    ids.Add(record.Id);
                }


            }

            records.AddInstantElements();
            manager.Dispose();
            logger.Write("Map synchronized");

        }

    }
}
