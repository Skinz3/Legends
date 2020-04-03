using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Records
{
    /// <summary>
    /// Synchronized from ObjectsCFG.cgf file.
    /// </summary>
    [Table("Buildings/")]
    public class BuildingRecord : ITable
    {
        private static List<BuildingRecord> Buildings = new List<BuildingRecord>();

        [Primary]
        public string Name
        {
            get;
            set;
        }

        public int MapId
        {
            get;
            set;
        }

        public int SkinId
        {
            get;
            set;
        }

        public string Rot
        {
            get;
            set;
        }

        public string Move
        {
            get;
            set;
        }

        public float Health
        {
            get;
            set;
        }

        public float BaseStaticHpRegen
        {
            get;
            set;
        }

        public float Mana
        {
            get;
            set;
        }

        public float SelectionHeight
        {
            get;
            set;
        }

        public float SelectionRadius
        {
            get;
            set;
        }

        public float PerceptionBubbleRadius
        {
            get;
            set;
        }

        public string SkinName1
        {
            get;
            set;
        }

        public string SkinName2
        {
            get;
            set;
        }

        public float CollisionRadius
        {
            get;
            set;
        }

        public float CollisionHeight
        {
            get;
            set;
        }

        public float PathfindingCollisionRadius
        {
            get;
            set;
        }

        public BuildingRecord()
        {

        }
     

        public static BuildingRecord GetBuildingRecord(MapIdEnum id, string name)
        {
            return Buildings.FirstOrDefault(x => x.MapId == (int)id && x.Name == name);
        }
    }
}
