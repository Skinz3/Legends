using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Records;
using Legends.World.Buildings;
using Legends.World.Entities;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public class AITurret : AIUnit
    {
        public override string Name => MapObjectRecord.Name;

        public override float PerceptionBubbleRadius => ((TurretStats)Stats).PerceptionBubbleRadius.Total;

        private MapObjectRecord MapObjectRecord
        {
            get;
            set;
        }
        private string Suffix
        {
            get;
            set;
        }
        private AIUnitRecord AIUnitRecord
        {
            get;
            set;
        }
        public AITurret(int netId, MapObjectRecord mapObject, string suffix)
        {
            this.NetId = netId;
            this.MapObjectRecord = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);
            this.Suffix = suffix;
        }
        public override void Initialize()
        {
            AIUnitRecord = BuildingManager.Instance.GetAIUnitRecord(this);
            Stats = new TurretStats(AIUnitRecord);
            base.Initialize();
        }
        public override void OnUnitEnterVision(Unit unit)
        {
            var t = unit as AIHero;
            t.AttentionPing(unit.Position, unit.NetId, PingTypeEnum.Ping_Missing);
        }
        public string GetClientName()
        {
            return Name + Suffix;
        }
        public override void OnUnitLeaveVision(Unit unit)
        {

        }

        public override void UpdateStats(bool partial)
        {
            Game.Send(new UpdateStatsMessage(0, NetId, ((TurretStats)Stats).ReplicationManager.Values, partial));
        }
    }
}
