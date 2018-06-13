using Legends.Core.IO;
using Legends.Core.IO.MOB;
using Legends.Core.IO.NavGrid;
using Legends.Core.Utils;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartORM;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer.CustomSyncs
{
    public class MapSynchronizer
    {
        static Logger logger = new Logger();

        public static void Synchronize(RafManager manager)
        {
            var navGrids = manager.GetFiles("AIPath.aimesh_ngrid");

            List<MapRecord> records = new List<MapRecord>();

            List<int> ids = new List<int>();
            foreach (var navGrid in navGrids)
            {
                NavGridFile grid = NavGridReader.ReadBinary(navGrid.GetContent(true));
                MapRecord record = new MapRecord();
                record.Name = navGrid.Path.Split('/')[1];
                record.Id = Helper.GetMapId(navGrid.Path);
                record.MiddleOfMap = grid.MiddleOfMap;
                record.Width = grid.MapWidth;
                record.Height = grid.MapHeight;

                var file = manager.GetUpToDateFile("LEVELS/" + record.Name + "/Scene/MapObjects.mob");

                if (file != null) // Map do not use .mob file format, we use SCO from room.dsc
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
                else
                {
                    var room = manager.GetFiles("room.dsc").FirstOrDefault(x => x.Path.Contains(record.Name)).GetContent(true);
                    var r = Encoding.ASCII.GetString(room);

                    if (Helper.GetMapId(navGrid.Path) == 12)
                    {

                    }
                }

                if (ids.Contains(record.Id) == false)
                {
                    records.Add(record);
                    ids.Add(record.Id);
                }

            }

            records.AddElements();
            logger.Write("Map synchronized");

        }
    }
}
