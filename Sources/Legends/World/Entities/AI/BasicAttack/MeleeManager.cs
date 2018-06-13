using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class MeleeManager : AttackManager
    {
        public MeleeManager(AIUnit unit) : base(unit)
        {
        }

        public override BasicAttack CreateBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1)
        {
            return new MeleeBasicAttack(unit, target, critical, first, slot);
        }
    }
}
