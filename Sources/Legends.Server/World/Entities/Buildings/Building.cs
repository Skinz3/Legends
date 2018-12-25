using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
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
        public const float GOLD_GIVEN_AT_DEATH = 50f;

        public override string Name => BuildingRecord.Name;

        public override float PerceptionBubbleRadius => Stats.PerceptionBubbleRadius.TotalSafe;

        public override float SelectionRadius => BuildingRecord.SelectionRadius;

        public override float PathfindingCollisionRadius => BuildingRecord.PathfindingCollisionRadius;
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
        public override VisibilityData GetVisibilityData()
        {
            return new VisibilityDataBuilding();
        }
        public override void OnUnitEnterVision(Unit unit)
        {
        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }
        protected override void ApplyExperienceLoot(AttackableUnit source)
        {
           
        }
        protected override void ApplyGoldLoot(AttackableUnit source)
        {
            source.AddGold(GOLD_GIVEN_AT_DEATH, true);
        }
    }
}
