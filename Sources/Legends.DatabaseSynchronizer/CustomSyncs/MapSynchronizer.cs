using Legends.Core.IO;
using Legends.Core.IO.MOB;
using Legends.Core.IO.NavGrid;
using Legends.Core.Utils;
using Legends.ORM;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
                record.XCellCount = grid.XCellCount;
                record.YCellCount = grid.YCellCount;
                record.MaxGridPos = new Vector3(grid.MaxGridPos.X, grid.MaxGridPos.Y, grid.MaxGridPos.Z);
                record.MinGridPos = new Vector3(grid.MinGridPos.X, grid.MinGridPos.Y, grid.MinGridPos.Z);
                List<MapCellRecord> cells = new List<MapCellRecord>();

                foreach (var cell in grid.Cells)
                {
                    cells.Add(new MapCellRecord()
                    {
                        ActorList = cell.ActorList,
                        AdditionalCost = cell.AdditionalCost,
                        ArrivalCost = cell.ArrivalCost,
                        AdditionalCostRefCount = cell.AdditionalCostRefCount,
                        ArrivalDirection = cell.ArrivalDirection,
                        HintAsGoodCell = cell.HintAsGoodCell,
                        CenterHeight = cell.CenterHeight,
                        GoodCellSessionId = cell.GoodCellSessionId,
                        Heuristic = cell.Heuristic,
                        Id = cell.Id,
                        IsOpen = cell.IsOpen,
                        RefHintNode = cell.RefHintNode,
                        RefHintWeight = cell.RefHintWeight,
                        SessionId = cell.SessionId,
                        X = cell.X,
                        Y = cell.Y,
                    });
                }
                record.Cells = cells.ToArray();

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


                    record.Cells = new MapCellRecord[0];
                    record.Objects = new MapObjectRecord[0];

                  
                }

                if (ids.Contains(record.Id) == false)
                {
                    records.Add(record);
                    ids.Add(record.Id);
                }

            }
            DatabaseManager.Instance.CreateTable(typeof(MapRecord));
            records.AddInstantElements(typeof(MapRecord));
            logger.Write("Map synchronized");

        }
    }
}
