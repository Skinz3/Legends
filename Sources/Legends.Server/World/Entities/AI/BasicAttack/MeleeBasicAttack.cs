using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class MeleeBasicAttack : BasicAttack
    {
        /// <summary>
        /// This constant represent the time frame where to auto hit.
        /// </summary>
        public const float HIT_TIME_MULTIPLIER = 0.25f;

        private float HitTime
        {
            get
            {
                return AnimationTime * HIT_TIME_MULTIPLIER;
            }
        }
        private float HitTimeCurrent
        {
            get;
            set;
        }
        public MeleeBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, critical, first, slot)
        {
            HitTimeCurrent = HitTime;
        }
        private float GetAutocancelDistance()
        {
            return (float)Unit.GetAutoattackRange(Target) + 120f;
        }
        public override void Update(long deltaTime)
        {
            if (Hit && Unit.GetDistanceTo(Target) > GetAutocancelDistance() && !Cancelled)
            {
                Unit.AttackManager.StopAttackTarget();
                Unit.AttackManager.DestroyAutoattack();
                Unit.TryBasicAttack(Target);
                return;
            }

            DeltaAnimationTime -= deltaTime;

            if (DeltaAnimationTime <= 0)
            {
                if (OnBasicAttackEnded != null)
                {
                    if (OnBasicAttackEnded(this))
                    {
                        return;
                    }
                }

                if (Cancelled == false)
                {
                    if (Target.Alive)
                    {
                        if (Unit.GetDistanceTo(Target) <= Unit.GetAutoattackRange(Target))
                        {
                            Unit.AttackManager.NextAutoattack();
                        }
                        else
                        {
                            Unit.AttackManager.StopAttackTarget();
                            Unit.AttackManager.DestroyAutoattack();
                            Unit.TryBasicAttack(Target);
                        }
                    }
                    else
                    {
                        Unit.AttackManager.StopAttackTarget();
                        Unit.AttackManager.DestroyAutoattack();
                    }
                }
                else
                {
                    Unit.AttackManager.DestroyAutoattack();
                }
            }

            if (Cancelled == false && !Hit)
            {
                HitTimeCurrent -= deltaTime;

                if (HitTimeCurrent <= 0)
                {
                    InflictDamages();
                }
            }
        }

        public override void OnCancel()
        {
           // nothing special
        }
    }
}
