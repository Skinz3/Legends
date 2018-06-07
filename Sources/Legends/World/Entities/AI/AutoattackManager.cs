using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    [InDeveloppement(InDeveloppementState.STARTED,"This is not finished ^^ & find a better name, AutoattackManager is weird")]
    public class AutoattackManager : IUpdatable
    {
        public AIUnit Unit
        {
            get;
            private set;
        }
        private AttackableUnit TargetUnit
        {
            get;
            set;
        }
        private bool HaveTarget
        {
            get
            {
                return TargetUnit != null;
            }
        }
        public bool Auto
        {
            get;
            private set;
        }
        private bool IsAttacking
        {
            get;
            set;
        }
        public AutoattackManager(AIUnit unit, bool auto)
        {
            this.Unit = unit;
            this.Auto = auto;
            this.UnitsInRange = new List<Unit>();
            this.AttackDelay = (1 / Unit.AIStats.AttackSpeed.Total) * 1000;
        }
        private List<Unit> UnitsInRange
        {
            get;
            set;
        }
        private float AttackDelay
        {
            get;
            set;
        }

        private Dictionary<AttackableUnit, float> GetUnitsInAttackRange()
        {
            Dictionary<AttackableUnit, float> results = new Dictionary<AttackableUnit, float>();

            foreach (var unit in Unit.GetOposedTeam().AliveUnits)
            {
                float distance = Unit.GetDistanceTo(unit);
                if (distance <= Unit.AttackRange) // <= vs <
                {
                    results.Add(unit, distance);
                }
            }
            return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        private void UpdateTarget(long deltaTime)
        {
            if (TargetUnit != null) // La cible n'est plus en portée de la tourelle
            {
                if (Unit.GetDistanceTo(TargetUnit) > Unit.AttackRange)
                    UnsetTarget();
                else
                    return;
            }


            var unitsInRange = GetUnitsInAttackRange(); // on cherche les autres entitées a portée de la tourelle

            if (TargetUnit == null && unitsInRange.Count > 0)
            {
                DefineTarget(unitsInRange.Last().Key);

                OnTargetReach();
            }
        }
        bool swapp = false;
        public void Update(long deltaTime)
        {
            if (Auto)
            {
                UpdateTarget(deltaTime);
            }
            if (IsAttacking)
            {
                if (TargetUnit.Alive == false)
                {
                    UnsetTarget();
                    return;
                }

                AttackDelay -= deltaTime;

                if (AttackDelay <= 0)
                {
                    AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_2;

                    if (swapp)
                        slot = AttackSlotEnum.BASIC_ATTACK_1;

                    swapp = !swapp;
                    TargetUnit.InflictDamages(new Damages(Unit, TargetUnit,720, DamageType.DAMAGE_TYPE_PHYSICAL, DamageResultEnum.DAMAGE_TEXT_CRITICAL));
                    Unit.Game.Send(new NextAutoattackMessage(Unit.NetId, TargetUnit.NetId, Unit.Game.NetIdProvider.PopNextNetId(), slot, false));
                    this.AttackDelay = (1 / Unit.AIStats.AttackSpeed.Total) * 1000;
                }
            }
            else
            {
                this.AttackDelay = (1 / Unit.AIStats.AttackSpeed.Total) * 1000;
            }
        }
        public void OnTargetReach()
        {
            Unit.StopMove(false);
            /*  client.Hero.AttentionPing(targetPosition, target.NetId, PingTypeEnum.Ping_OnMyWay); */
            Unit.Game.Send(new BeginAutoAttackMessage(Unit.NetId, TargetUnit.NetId, 0x80, 0, false, TargetUnit.Position, Unit.Position, Unit.Game.Map.Record.MiddleOfMap));
            TargetUnit.InflictDamages(new Damages(Unit, TargetUnit, 12, DamageType.DAMAGE_TYPE_PHYSICAL, DamageResultEnum.DAMAGE_TEXT_CRITICAL));
            IsAttacking = true;

        }
        public void UnsetTarget()
        {
            Unit.Game.Send(new StopAutoAttackMessage(Unit.NetId));
            IsAttacking = false;
            Unit.OnTargetUnset(TargetUnit);
            TargetUnit = null;
        }
        public void DefineTarget(AttackableUnit target)
        {
            this.TargetUnit = target;
            Unit.OnTargetSet(target);
        }
    }
}
