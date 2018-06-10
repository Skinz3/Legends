

using Legends.Core.IO.MOB;
using Newtonsoft.Json;
using SmartORM;
using SmartORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("/Database/Maps/")] 
    public class MapRecord : ITable
    {
        [JsonCache]
        private static List<MapRecord> Maps = new List<MapRecord>();

        public int Id
        {
            get;
            set;
        }

        [JsonFileName]
        public string Name
        {
            get;
            set;
        }

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

        public MapObjectRecord[] Objects
        {
            get;
            set;
        }

        public MapRecord()
        {

        }
        public MapRecord(int id, string name, Vector2 middleOfMap, float width, float height, MapObjectRecord[] objects)
        {
            this.Id = id;
            this.Name = name;
            this.MiddleOfMap = middleOfMap;
            this.Width = width;
            this.Height = height;
            this.Objects = objects;
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
