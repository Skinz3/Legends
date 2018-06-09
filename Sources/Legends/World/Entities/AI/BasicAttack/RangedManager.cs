using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Autoattack
{
    public class RangedManager : AttackManager
    {
        public RangedManager(AIUnit unit, bool auto) : base(unit, auto)
        {
        }

        public override void BeginAttackTarget(AIUnit target)
        {
            if (IsAttacking == false)
            {
                CurrentAutoattack = new RangedBasicAttack(Unit, target, Unit.AIStats.CriticalStrike());
                CurrentAutoattack.Notify();
                Unit.OnTargetSet(target);
            }
            else if (IsAttacking == true && CurrentAutoattack.Cancelled && CurrentAutoattack.Hit)
            {
                CurrentAutoattack.RequiredNew = true;
            }
        }

        public override void NextAutoattack()
        {
            bool critical = Unit.AIStats.CriticalStrike();
            Unit.AttackManager.CurrentAutoattack = new RangedBasicAttack(Unit, CurrentAutoattack.Target, critical, false, DetermineNextSlot(critical));
            CurrentAutoattack.Notify();
        }
    }
}
