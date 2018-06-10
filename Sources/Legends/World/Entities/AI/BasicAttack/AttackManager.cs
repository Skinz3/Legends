using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.BasicAttack
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
        public AttackManager(AIUnit unit)
        {
            this.Unit = unit;
            this.UnitsInRange = new List<Unit>();
        }
        public void SetAutoattackActivated(bool activated)
        {
            this.Auto = activated;
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
            if (Auto && Unit.IsMoving == false && !IsAttacking)
            {
                var unitsInRange = GetUnitsInAttackRange();
                if (unitsInRange.Count > 0)
                    BeginAttackTarget(unitsInRange.Last().Key); // the closest one is last
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
        public abstract void BeginAttackTarget(AttackableUnit target);

        public void StopAttackTarget()
        {
            if (IsAttacking)
            {
                CurrentAutoattack.Cancel();
                Unit.OnTargetUnset(CurrentAutoattack.Target);



            }
        }
        public void DestroyAutoattack()
        {
            CurrentAutoattack = null;
        }

        public abstract void NextAutoattack();

    }
}

