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
            throw new NotImplementedException();
        }

        public override void NextAutoattack()
        {
            throw new NotImplementedException();
        }
    }
}
