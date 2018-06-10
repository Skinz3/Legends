using SmartORM;
using SmartORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Records
{
    /// <summary>
    /// Synchronized from ObjectsCFG.cgf file.
    /// </summary>
    [Table("/Database/Buildings/")]
    public class BuildingRecord : ITable
    {
        [JsonCache]
        private static List<BuildingRecord> Buildings = new List<BuildingRecord>();

        [JsonFileName]
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
        public BuildingRecord(string name, int mapId, int skinId, string rot, string move, float maxHp, float baseStaticHpRegen,
            float mana, float selectionHeight, float selectionRadius, float perceptionBubbleRadius,
            string skinName1, string skinName2, float collisionRadius, float collisionHeight, float pathfidingCollisionRadius)
        {
            this.Name = name;
            this.MapId = mapId;
            this.SkinId = skinId;
            this.Rot = rot;
            this.Move = move;
            this.Health = maxHp;
            this.BaseStaticHpRegen = baseStaticHpRegen;
            this.Mana = mana;
            this.SelectionHeight = selectionHeight;
            this.SelectionRadius = selectionRadius;
            this.PerceptionBubbleRadius = perceptionBubbleRadius;
            this.SkinName1 = skinName1;
            this.SkinName2 = skinName2;
            this.CollisionRadius = collisionRadius;
            this.CollisionHeight = collisionHeight;
            this.PathfindingCollisionRadius = pathfidingCollisionRadius;
        }

        public static BuildingRecord GetBuildingRecord(MapIdEnum id, string name)
        {
            return Buildings.FirstOrDefault(x => x.MapId == (int)id && x.Name == name);
        }
    }
}
