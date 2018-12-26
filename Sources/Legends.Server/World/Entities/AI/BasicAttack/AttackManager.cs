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

            foreach (var team in Unit.Team.GetOposedTeams())
            {
                foreach (var unit in team.AliveUnits.OfType<AIUnit>())
                {
                    float distance = Unit.GetDistanceTo(unit);
                    if (distance <= Unit.GetAutoattackRangeWhileChasing(unit)) // <= vs <
                    {
                        results.Add((AIUnit)unit, distance);
                    }
                }

            }
            return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        public virtual void Update(float deltaTime)
        {
            if (Unit.Alive == false)
            {
                if (IsAttacking)
                    DestroyAutoattack();

                return;
            }
            if (Auto && Unit.IsMoving == false && !IsAttacking && !Unit.SpellManager.IsChanneling())
            {
                var unitsInRange = GetUnitsInAttackRange();
                if (unitsInRange.Count > 0)
                    BeginAttackTarget(unitsInRange.Last().Key); // the closest one is last
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
            var slot = AttackSlotEnum.BASE_ATTACK_1;

            if (CurrentAutoattack != null && CurrentAutoattack.Slot == AttackSlotEnum.BASE_ATTACK_1 && (Unit is AIHero))
            {
                slot = AttackSlotEnum.BASE_ATTACK_2;
            }
            return slot;
        }

        public virtual void StopAttackTarget()
        {
            if (IsAttacking)
            {
                CurrentAutoattack.Cancel();
                Unit.OnTargetUnset(CurrentAutoattack.Target);

            }
        }
        public AttackableUnit GetTarget()
        {
            return CurrentAutoattack?.Target;
        }
        public void DestroyAutoattack()
        {
            CurrentAutoattack = null;
        }

        public abstract BasicAttack CreateBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASE_ATTACK_1);

        public virtual void BeginAttackTarget(AttackableUnit target)
        {
            if (IsAttacking && target != CurrentAutoattack.Target) // Si la cible que l'on attaque est differente de target
            {
                StopAttackTarget(); // alors on cancel l'anim quoi qu'il arrive

                if (CurrentAutoattack.Casted) // Si l'attaque précédante a touchée
                {
                    CurrentAutoattack.OnBasicAttackEnded = new Func<BasicAttack, bool>((BasicAttack attack) => // a la fin du delai l'attaque, on changera de cible (pas de cancel d'anim sur l'anim de l'auto).
                    {
                        DestroyAutoattack();
                        Unit.TryBasicAttack(target);
                        return true;
                    });
                }
                else // Sinon on peut directment passer a la nouvelle auto
                {
                    DestroyAutoattack();
                    Unit.TryBasicAttack(target);
                }
            }
            if (IsAttacking == false) // osef, facile
            {
                bool critical = target.Stats.IsCriticalImmune ? false : Unit.Stats.CriticalStrike();
                CurrentAutoattack = CreateBasicAttack(Unit, target, critical);
                CurrentAutoattack.Notify();
                Unit.OnTargetSet(target);
            }
            else if (IsAttacking == true && CurrentAutoattack.Casted && target == CurrentAutoattack.Target)
            { // Si l'on attaquait la cible, que l'ancienne auto a été cancel mais qu'elle a touchée, et que la cible n'a pas changée
                CurrentAutoattack.OnBasicAttackEnded = new Func<BasicAttack, bool>((BasicAttack attack) =>
                {
                    // Unit.AttackManager.StopAttackTarget();
                    Unit.AttackManager.DestroyAutoattack();
                    Unit.TryBasicAttack(target);

                    return true;

                });

            }
        }

        public virtual void NextAutoattack()
        {
            bool critical = CurrentAutoattack.Target.Stats.IsCriticalImmune ? false : Unit.Stats.CriticalStrike();
            CurrentAutoattack = CreateBasicAttack(Unit, CurrentAutoattack.Target, critical, false, DetermineNextSlot(critical));
            CurrentAutoattack.Notify();
        }

    }
}

