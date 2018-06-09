using Legends.Core.Protocol.Enum;
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
        public MeleeBasicAttack(AIUnit unit, AIUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, critical, first, slot)
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
                Unit.TryAutoattack(Target);
                return;
            }

            base.Update(deltaTime);

            if (Cancelled == false && !Hit)
            {
                HitTimeCurrent -= deltaTime;

                if (HitTimeCurrent <= 0)
                {
                    InflictDamages();
                }
            }
        }
    }
}
