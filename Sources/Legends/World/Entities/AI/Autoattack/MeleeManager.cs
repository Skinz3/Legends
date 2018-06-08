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
                CurrentAutoattack = new MeleeAutoattack(Unit, target);
                CurrentAutoattack.Notify();
                Unit.OnTargetSet(target);
            }
        }

        public override void NextAutoattack()
        {
            Unit.AttackManager.CurrentAutoattack = new MeleeAutoattack(Unit, CurrentAutoattack.Target, false, DetermineNextSlot());
            CurrentAutoattack.Notify();
        }
    }
}
