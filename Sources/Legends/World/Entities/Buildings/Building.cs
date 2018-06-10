using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Buildings
{
    public abstract class Building : AttackableUnit
    {
        public override string Name => BuildingRecord.Name;

        public override float PerceptionBubbleRadius => Stats.PerceptionBubbleRadius.TotalSafe;

        public override float SelectionRadius => 350; // BuildingRecord.PathfindingCollisionRadius;

        protected BuildingRecord BuildingRecord
        {
            get;
            set;
        }
        protected MapObjectRecord MapObjectRecord
        {
            get;
            set;
        }
        public Building(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId)
        {
            this.BuildingRecord = buildingRecord;
            this.MapObjectRecord = mapObjectRecord;
        }
        public override void Initialize()
        {
            Stats = new BuildingStats(BuildingRecord);
            base.Initialize();
        }
        public override void OnUnitEnterVision(Unit unit)
        {
        }

        public override void OnUnitLeaveVision(Unit unit)
        {
            
        }

        public override void UpdateStats(bool partial)
        {
            base.UpdateStats(partial);
        }
    }
}
