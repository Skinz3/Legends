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

        private AttackableUnit Target
        {
            get;
            set;
        }
        private float AttackRange
        {
            get
            {
                return AIStats.AttackRange.Total + 150; // + 150?
            }
        }
        private List<Unit> UnitsInRange
        {
            get;
            set;
        }

        public override bool Autoattack => true;

        public AITurret(int netId, AIUnitRecord record, MapObjectRecord mapObject, string suffix)
        {
            this.NetId = netId;
            this.Record = record;
            this.MapObjectRecord = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);
            this.UnitsInRange = new List<Unit>();
            this.Suffix = suffix;
        }
        public override void Initialize()
        {
            Stats = new TurretStats(Record);
            base.Initialize();
        }
        protected void SetTarget(AttackableUnit unit)
        {
            Target = unit;
            Game.Send(new SetTargetMessage(NetId, unit.NetId));
        }
        protected void UnsetTarget()
        {
            Target = null;
            Game.Send(new SetTargetMessage(NetId, 0));
        }
        public override void InflictDamages(Damages damages)
        {
            base.InflictDamages(damages);
            this.UpdateHeath();
        }
        public override void OnDead(Unit source)
        {
            base.OnDead(source);
            Game.UnitAnnounce(UnitAnnounceEnum.TurretDestroyed, NetId, source.NetId, new int[0]);
        }
        public string GetClientName()
        {
            return Name + Suffix;
        }
        private Dictionary<AttackableUnit, float> GetUnitsInAttackRange()
        {
            Dictionary<AttackableUnit, float> results = new Dictionary<AttackableUnit, float>();

            foreach (var unit in GetOposedTeam().Units.Values.OfType<AttackableUnit>())
            {
                float distance = this.GetDistanceTo(unit);
                if (distance <= AttackRange) // <= vs <
                {
                    results.Add(unit, distance);
                }
            }
            return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        private void UpdateTarget(long deltaTime)
        {
            if (Target != null) // La cible n'est plus en portée de la tourelle
            {
                if (this.GetDistanceTo(Target) > AttackRange)
                    UnsetTarget();
                else
                    return;
            }


            var unitsInRange = GetUnitsInAttackRange(); // on cherche les autres entitées a portée de la tourelle

            if (Target == null && unitsInRange.Count > 0)
            {
             //   MoveToAutoattack(Target as AIUnit);
                SetTarget(unitsInRange.Last().Key);
               
              Game.Send(new BeginAutoAttackMessage(NetId, Target.NetId, 0x80, 0, false, Target.Position, Position, Game.Map.Record.MiddleOfMap));
            }
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            this.UpdateTarget(deltaTime);

        }
        public override void UpdateStats(bool partial)
        {
            Game.Send(new UpdateStatsMessage(0, NetId, ((TurretStats)Stats).ReplicationManager.Values, partial));
        }

        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }
    }
}
