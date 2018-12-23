using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum AttackSlotEnum : uint
    {
        BASE_ATTACK = 0u,
        EXTRA_ATTACK1 = 1u,
        EXTRA_ATTACK2 = 2u,
        EXTRA_ATTACK3 = 3u,
        EXTRA_ATTACK4 = 4u,
        EXTRA_ATTACK5 = 5u,
        EXTRA_ATTACK6 = 6u,
        EXTRA_ATTACK7 = 7u,
        EXTRA_ATTACK8 = 8u,
        CRIT_ATTACK = 9u,
        EXTRA_CRIT_ATTACK_1 = 0xAu,
        EXTRA_CRIT_ATTACK_2 = 0xBu,
        EXTRA_CRIT_ATTACK_3 = 0xCu,
        EXTRA_CRIT_ATTACK_4 = 0xDu,
        EXTRA_CRIT_ATTACK_5= 0xEu,
        EXTRA_CRIT_ATTACK_6= 0xFu,
        EXTRA_CRIT_ATTACK_7= 0x10u,
        EXTRA_CRIT_ATTACK_8= 0x11u,
        BASE_ATTACK_1 = 0x40u,
        BASE_ATTACK_2 = 0x41u,
        BASIC_ATTACK_CRITICAL = 0x49u,
    }
}
