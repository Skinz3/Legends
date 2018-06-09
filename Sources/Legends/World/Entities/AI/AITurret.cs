using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Extended;
using Legends.Core.Protocol.Messages.Game;
using Legends.Network;
using Legends.Records;
using Legends.World.Buildings;
using Legends.World.Entities;
using Legends.World.Entities.Statistics;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public class AITurret : AIUnit
    {
        public override string Name => MapObjectRecord.Name;

        public override float PerceptionBubbleRadius => ((TurretStats)Stats).PerceptionBubbleRadius.Total;

        public override bool DefaultAutoattackActivated => true;

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

        public AITurret(int netId, AIUnitRecord record, MapObjectRecord mapObject, string suffix)
        {
            this.NetId = netId;
            this.Record = record;
            this.MapObjectRecord = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);

            this.Suffix = suffix;
        }
        public override void Initialize()
        {
            Stats = new TurretStats(Record);
            base.Initialize();
        }
        protected void SetTarget(AttackableUnit unit)
        {

        }
        public override void OnTargetSet(AttackableUnit target)
        {
            Game.Send(new SetTargetMessage(NetId, target.NetId));

        }
        public override void OnTargetUnset(AttackableUnit target)
        {
            Game.Send(new SetTargetMessage(NetId, 0));
        }
        public override void InflictDamages(Damages damages)
        {
            base.InflictDamages(damages);
            this.UpdateHeath();
        }
        public override void OnDead(AttackableUnit source)
        {
            base.OnDead(source);
            Game.UnitAnnounce(UnitAnnounceEnum.TurretDestroyed, NetId, source.NetId, new int[0]);
        }
        public string GetClientName()
        {
            return Name + Suffix;
        }

        public override void UpdateStats(bool partial)
        {
            TurretStats stats = ((TurretStats)Stats);
            stats.UpdateReplication(partial);
            Game.Send(new UpdateStatsMessage(0, NetId, stats.ReplicationManager.Values, partial));

            if (partial)
            {
                foreach (var x in stats.ReplicationManager.Values)
                {
                    if (x != null)
                    {
                        x.Changed = false;
                    }
                }
            }
        }

        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }
    }
}
