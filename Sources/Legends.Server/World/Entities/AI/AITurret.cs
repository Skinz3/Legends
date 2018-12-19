using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
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
        public override string Name => BuildingRecord.Name;

        public override float PerceptionBubbleRadius => ((TurretStats)Stats).PerceptionBubbleRadius.TotalSafe;

        public override bool DefaultAutoattackActivated => true;

        public override bool AddFogUpdate => true;

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
        public BuildingRecord BuildingRecord
        {
            get;
            private set;
        }
        public AITurret(uint netId, AIUnitRecord record, MapObjectRecord mapObject, BuildingRecord buildingRecord, string suffix) : base(netId, record)
        {
            this.NetId = netId;
            this.MapObjectRecord = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);
            this.Suffix = suffix;
            this.BuildingRecord = buildingRecord;
        }
        public override void Initialize()
        {
            Stats = new TurretStats(Record, BuildingRecord);
            base.Initialize();
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
        public override float GetAutoattackRangeWhileChasing(AttackableUnit target)
        {
            return Record.AttackRange + (float)Record.SelectionRadius;
        }
        public override void OnDead(AttackableUnit source)
        {
            base.OnDead(source);
            Game.Send(new DieMessage(source.NetId, NetId));
            Game.UnitAnnounce(UnitAnnounceEnum.TurretDestroyed, NetId, source.NetId, new uint[0]);
        }
        public string GetClientName()
        {
            return Name + Suffix;
        }

        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }

        public override void OnSpellUpgraded(byte spellId, Spell targetSpell)
        {
            throw new NotImplementedException();
        }
    }
}
