using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Autoattack
{
    public abstract class AttackManager : IUpdatable
    {
        public AIUnit Unit
        {
            get;
            private set;
        }
        public BasicAttack CurrentAutoattack
        {
            get;
            set;
        }
        public bool IsAttacking
        {
            get
            {
                return CurrentAutoattack != null;
            }
        }
        public bool Auto
        {
            get;
            private set;
        }
        public AttackManager(AIUnit unit, bool auto)
        {
            this.Unit = unit;
            this.Auto = auto;
            this.UnitsInRange = new List<Unit>();
        }
        #region UnitsInRange
        private List<Unit> UnitsInRange
        {
            get;
            set;
        }

        private Dictionary<AIUnit, float> GetUnitsInAttackRange()
        {
            Dictionary<AIUnit, float> results = new Dictionary<AIUnit, float>();

            foreach (var unit in Unit.GetOposedTeam().AliveUnits.OfType<AIUnit>())
            {
                float distance = Unit.GetDistanceTo(unit);
                if (distance <= Unit.GetAutoattackRangeWhileChasing(unit)) // <= vs <
                {
                    results.Add((AIUnit)unit, distance);
                }
            }
            return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion


        public void Update(long deltaTime)
        {
            if (Auto)
            {
                var a = GetUnitsInAttackRange();
                if (a.Count > 0)
                    BeginAttackTarget(a.Last().Key);
            }
            if (Unit.Alive == false)
            {
                DestroyAutoattack();
            }
            if (CurrentAutoattack != null && CurrentAutoattack.Finished == false)
            {
                CurrentAutoattack.Update(deltaTime);
            }
        }
        protected AttackSlotEnum DetermineNextSlot(bool critical)
        {
            if (critical)
            {
                return AttackSlotEnum.BASIC_ATTACK_CRITICAL;
            }
            var slot = AttackSlotEnum.BASIC_ATTACK_1;

            if (CurrentAutoattack != null && CurrentAutoattack.Slot == AttackSlotEnum.BASIC_ATTACK_1 && Unit.Record.IsMelee == true)
            {
                slot = AttackSlotEnum.BASIC_ATTACK_2;
            }
            return slot;
        }
        public abstract void BeginAttackTarget(AIUnit target);

        public void StopAttackTarget()
        {
            if (IsAttacking)
            {
                CurrentAutoattack.RequiredNew = false;
                CurrentAutoattack.Cancel();
                Unit.OnTargetUnset(CurrentAutoattack.Target);

                if (!CurrentAutoattack.Hit)
                {
                    DestroyAutoattack();
                }

            }
        }
        public void DestroyAutoattack()
        {
            CurrentAutoattack = null;
        }

        public abstract void NextAutoattack();

    }
}

