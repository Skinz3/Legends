using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum DamageResultEnum : Byte
    {
        DAMAGE_TEXT_INVULNERABLE = 0x0,
        DAMAGE_TEXT_DODGE = 0x2,
        DAMAGE_TEXT_CRITICAL = 0x3,
        DAMAGE_TEXT_NORMAL = 0x4,
        DAMAGE_TEXT_MISS = 0x5
    }
}
