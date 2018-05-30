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
    [Table("maps")]
    public class MapRecord : ITable
    {
        public static List<MapRecord> Maps = new List<MapRecord>();

        [Primary]
        public int Id;

        public string Name;

        public Vector2 MiddleOfMap;

        public float Width;

        public float Height;

        public MapRecord()
        {

        }
        public MapRecord(int id, string name, Vector2 middleOfMap, float width, float height)
        {
            this.Id = id;
            this.Name = name;
            this.MiddleOfMap = middleOfMap;
            this.Width = width;
            this.Height = height;
        }
        public static MapRecord GetMap(int id)
        {
            return Maps.Find(x => x.Id == id);
        }
    }

}
