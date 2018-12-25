

using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.IO.MOB;
using Legends.Core.IO.NavGrid;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.World.Entities.Movements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("maps")]
    public class MapRecord : ITable
    {
        private static List<MapRecord> Maps = new List<MapRecord>();

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [Json]
        public Vector2 MiddleOfMap
        {
            get;
            set;
        }

        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        [Json]
        public MapObjectRecord[] Objects
        {
            get;
            set;
        }
        [Json]
        public MapCellRecord[] Cells
        {
            get;
            set;
        }
        [Json]
        public ushort[] CellFlags
        {
            get;
            set;
        }
        public uint XCellCount
        {
            get;
            set;
        }
        public uint YCellCount
        {
            get;
            set;
        }
        [Json]
        public Vector3 MinGridPos
        {
            get;
            set;
        }
        [Json]
        public Vector3 MaxGridPos
        {
            get;
            set;
        }
        [Ignore]
        public Vector3 TranslationMaxGridPos
        {
            get;
            set;
        }
      

        public float GetZ(Vector2 position)
        {
            var vector = TranslateToNavGrid(position);
            var cell = GetCell((short)vector.X, (short)vector.Y);
            if (cell != null)
            {
                return cell.CenterHeight;
            }
            return float.MinValue;
        }
        public bool HasFlag(int index, NavigationGridCellFlags flag)
        {
            return (CellFlags[index] & (int)flag) == (int)flag;
        }
        public MapCellRecord GetCell(Vector2 position)
        {
            var relativePosition = TranslateToNavGrid(position);
            return GetCell((short)relativePosition.X, (short)relativePosition.Y);
        }
        public MapCellRecord GetCell(int index)
        {
            if (Cells.Length > index)
                return Cells[index];
            else
                return null;
        }
        public MapCellRecord GetCell(short x, short y)
        {
            var index = y * XCellCount + x;
            if (x < 0 || x > XCellCount || y < 0 || y > YCellCount || index >= Cells.Length)
            {
                return null;
            }

            return Cells[index];
        }

        public Vector2 GetClosestTerrainExit(Vector2 location)
        {
            if (IsWalkable(location))
            {
                return location;
            }

            var trueX = (double)location.X;
            var trueY = (double)location.Y;
            var angle = Math.PI / 4;
            var rr = (location.X - trueX) * (location.X - trueX) + (location.Y - trueY) * (location.Y - trueY);
            var r = Math.Sqrt(rr);
            // x = r * cos(angle)
            // y = r * sin(angle)
            // r = distance from center
            // Draws spirals until it finds a walkable spot
            while (!IsWalkable((float)trueX, (float)trueY))
            {
                trueX = location.X + r * Math.Cos(angle);
                trueY = location.Y + r * Math.Sin(angle);
                angle += Math.PI / 4;
                r += 1;
            }

            return new Vector2((float)trueX, (float)trueY);
        }
        public bool IsWalkable(float x, float y)
        {
            return IsWalkable(new Vector2(x, y));
        }
        public bool IsWalkable(Vector2 coords)
        {
            var vector = TranslateToNavGrid(coords);
            var cell = GetCell((short)vector.X, (short)vector.Y);
            return cell != null && !cell.HasFlag(this, NavigationGridCellFlags.NotPassable);
        }
        public Vector2 TranslateToNavGrid(Vector2 vector)
        {
            vector.X = (vector.X - MinGridPos.X) * TranslationMaxGridPos.X;
            vector.Y = (vector.Y - MinGridPos.Z) * TranslationMaxGridPos.Z;
            return new Vector2(vector.X, vector.Y);
        }
        public Vector2 TranslateFromNavGrid(short x, short y)
        {
            var ret = new Vector2()
            {
                X = x / TranslationMaxGridPos.X + MinGridPos.X,
                Y = y / TranslationMaxGridPos.Z + MinGridPos.Z
            };

            return ret;
        }
        public bool IsBrush(Vector2 coords)
        {
            var vector = TranslateToNavGrid(coords);
            var cell = GetCell((short)vector.X, (short)vector.Y);
            return cell != null && cell.HasFlag(this, NavigationGridCellFlags.HasGrass);
        }
        public MapRecord()
        {

        }
        public MapObjectRecord GetObject(string name)
        {
            return Objects.FirstOrDefault(x => x.Name == name);
        }
        public MapObjectRecord[] GetObjects(MOBObjectType type)
        {
            return Array.FindAll(Objects, x => x.Type == type);
        }

        public static MapRecord GetMap(int id)
        {
            return Maps.Find(x => x.Id == id);
        }

        [StartupInvoke(StartupInvokePriority.Ninth)]
        public static void Initialize()
        {
            foreach (var map in MapRecord.Maps)
            {
                map.TranslationMaxGridPos = new Vector3
                {
                    X = map.XCellCount / map.MaxGridPos.X,
                    Z = map.YCellCount / map.MaxGridPos.Z
                };

                foreach (var cell in map.Cells)
                {
                    cell.LoadAdjacentCells(map);
                }
                
            }

        }
    }
    public class MapCellRecord
    {
        public int Id
        {
            get;
            set;
        }
        public float CenterHeight
        {
            get;
            set;
        }
        public uint SessionId
        {
            get;
            set;
        }
        public float ArrivalCost
        {
            get;
            set;
        }
        public float Heuristic
        {
            get;
            set;
        }
        public uint ActorList
        {
            get;
            set;
        }
        public short X
        {
            get;
            set;
        }
        public short Y
        {
            get;
            set;
        }
        public float AdditionalCost
        {
            get;
            set;
        }
        public float HintAsGoodCell
        {
            get;
            set;
        }
        public uint AdditionalCostRefCount
        {
            get;
            set;
        }
        public uint GoodCellSessionId
        {
            get;
            set;
        }
        public float RefHintWeight
        {
            get;
            set;
        }
        public short ArrivalDirection
        {
            get;
            set;
        }
        public short[] RefHintNode
        {
            get;
            set;
        }
        public MapCellRecord[] Adjacents
        {
            get;
            set;
        }

        public void LoadAdjacentCells(MapRecord record)
        {
            MapCellRecord[] cells = new MapCellRecord[4];

            cells[0] = record.GetCell((short)(X + 1), Y);
            cells[1] = record.GetCell((short)(X - 1), Y);
            cells[2] = record.GetCell(X, (short)(Y + 1));
            cells[3] = record.GetCell(X, (short)(Y - 1));

            Adjacents = cells.Where(x => x != null).ToArray();
        }
        public bool HasFlag(MapRecord mapRecord, NavigationGridCellFlags flag)
        {
            return mapRecord.HasFlag(Id, flag);
        }
    }
    public class MapObjectRecord
    {
        public string Name
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public MOBObjectType Type
        {
            get;
            set;
        }

        public Vector3 Scale
        {
            get;
            set;
        }

        public Vector3 Rotation
        {
            get;
            set;
        }

        public MapObjectRecord(string name, Vector3 position, MOBObjectType type, Vector3 scale, Vector3 rotation)
        {
            this.Name = name;
            this.Position = position;
            this.Type = type;
            this.Scale = scale;
            this.Rotation = rotation;
        }
    }

}
