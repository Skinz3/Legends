using Legends.Core.Protocol.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Autoattack
{
    public class MeleeAutoattack : Autoattack
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
        public MeleeAutoattack(AIUnit unit, AIUnit target, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, first, slot)
        {
            HitTimeCurrent = HitTime;
        }
        public override void Update(long deltaTime)
        {
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
