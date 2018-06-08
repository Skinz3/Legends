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
        public Autoattack CurrentAutoattack
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
        public AttackManager(AIUnit unit, bool auto)
        {
            this.Unit = unit;

            // this.UnitsInRange = new List<Unit>();
        }
        #region UnitsInRange
        /*  private List<Unit> UnitsInRange
          {
              get;
              set;
          }

          private Dictionary<AIUnit, float> GetUnitsInAttackRange()
          {
              Dictionary<AIUnit, float> results = new Dictionary<AIUnit, float>();

              foreach (var unit in Unit.GetOposedTeam().AliveUnits)
              {
                  float distance = Unit.GetDistanceTo(unit);
                  if (distance <= Unit.AttackRange) // <= vs <
                  {
                      results.Add((AIUnit)unit, distance);
                  }
              }
              return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
          }
          */
        #endregion
        public float GetRequiredDistanceToAttack(AIUnit targetUnit)
        {
            return Unit.AIStats.AttackRange.Total +
               ((float)targetUnit.Record.SelectionRadius * targetUnit.AIStats.ModelSize.Total);
        }

        public void Update(long deltaTime)
        {
            if (Unit.Alive == false)
            {
                DestroyAutoattack();
            }
            if (CurrentAutoattack != null && CurrentAutoattack.Finished == false)
            {
                CurrentAutoattack.Update(deltaTime);
            }
        }
        protected AttackSlotEnum DetermineNextSlot()
        {
            var slot = AttackSlotEnum.BASIC_ATTACK_1;

            if (CurrentAutoattack != null && CurrentAutoattack.Slot == AttackSlotEnum.BASIC_ATTACK_1)
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

