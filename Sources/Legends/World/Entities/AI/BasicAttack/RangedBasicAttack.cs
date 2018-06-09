using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.Protocol.Enum;
using Legends.Core.DesignPattern;

namespace Legends.World.Entities.AI.BasicAttack
{
    [InDeveloppement(InDeveloppementState.TODO,"just todo")]
    public class RangedBasicAttack : BasicAttack
    {
        
        public RangedBasicAttack(AIUnit unit, AIUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, critical, first, slot)
        {
            
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
               
            }

        }
    }
}
