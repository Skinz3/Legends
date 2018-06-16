using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class RangedManager : AttackManager
    {
        public RangedManager(AIUnit unit) : base(unit)
        {
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
        }
        public override void BeginAttackTarget(AttackableUnit target)
        {
            bool critical = target.Stats.IsCriticalImmune ? false : Unit.Stats.CriticalStrike();
            CurrentAutoattack = CreateBasicAttack(Unit, target, critical);
            CurrentAutoattack.Notify();
            Unit.OnTargetSet(target);
        }
        public override BasicAttack CreateBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1)
        {
            return new RangedBasicAttack(unit, target, critical, first, slot);
        }
    }
}
