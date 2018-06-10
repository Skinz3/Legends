using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum DamageType : byte
    {
        DAMAGE_TYPE_PHYSICAL = 0x0,
        DAMAGE_TYPE_MAGICAL = 0x1,
        DAMAGE_TYPE_TRUE = 0x2
    }
}
