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
    public class MeleeManager : AttackManager
    {
        public MeleeManager(AIUnit unit, bool auto) : base(unit, auto)
        {


        }

        public override void BeginAttackTarget(AIUnit target)
        {
            if (IsAttacking == false)
            {
                CurrentAutoattack = new MeleeBasicAttack(Unit, target, Unit.AIStats.CriticalStrike());
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
            Unit.AttackManager.CurrentAutoattack = new MeleeBasicAttack(Unit, CurrentAutoattack.Target, critical, false, DetermineNextSlot(critical));
            CurrentAutoattack.Notify();
        }
    }
}
