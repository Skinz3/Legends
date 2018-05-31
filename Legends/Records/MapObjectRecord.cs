using Legends.Core.IO.MOB;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("mapobjects", 2)]
    public class MapObjectRecord : ITable
    {
        public static List<MapObjectRecord> MapObjects = new List<MapObjectRecord>();

        public int MapId;

        public string Name;

        public Vector3 Position;

        public MOBObjectType Type;

        public Vector3 Scale;

        public Vector3 Rotation;

        public MapObjectRecord(int mapId, string name, Vector3 position, MOBObjectType type, Vector3 scale, Vector3 rotation)
        {
            this.MapId = mapId;
            this.Name = name;
            this.Position = position;
            this.Type = type;
            this.Scale = scale;
            this.Rotation = rotation;
        }

        public static MapObjectRecord[] GetObjects(int id)
        {
            return MapObjects.FindAll(x => x.MapId == id).ToArray();
        }
    }
}
