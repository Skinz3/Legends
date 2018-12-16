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
      
        public MeleeBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, critical, first, slot)
        {
          
        }

        public override bool UseCastTime => true;

        protected override float GetAutocancelDistance()
        {
            return (float)Unit.GetAutoattackRange(Target) + 120f;
        }
       

        protected override void OnCancel()
        {
           // nothing special
        }

        protected override void OnCastTimeReach()
        {
            InflictDamages();
        }
    }
}
