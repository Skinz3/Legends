

using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.IO.MOB;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
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
            var vector = TranslateToNavGrid(new Vector3(position, 0));
            var cell = GetCell((short)vector.X, (short)vector.Y);
            if (cell != null)
            {
                return cell.CenterHeight;
            }
            return float.MinValue;
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
        public Vector3 TranslateToNavGrid(Vector3 vector)
        {
            vector.ForceSize(2);
            vector.X = (vector.X - MinGridPos.X) * TranslationMaxGridPos.X;
            vector.Y = (vector.Y - MinGridPos.Z) * TranslationMaxGridPos.Z;
            return vector;
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

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var map in MapRecord.Maps)
            {
                if (map.TranslationMaxGridPos == null)
                {
                    map.TranslationMaxGridPos = new Vector3
                    {
                        X = map.XCellCount / map.MaxGridPos.X,
                        Z = map.YCellCount / map.MaxGridPos.Z
                    };
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
        public bool IsOpen
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
