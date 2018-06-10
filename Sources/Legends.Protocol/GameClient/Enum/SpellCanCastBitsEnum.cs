using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum SpellCanCastBitsEnum : uint
    {
        Spell0 =  1 << 1,
        Spell1 = 1 << 2,
        Spell2 = 1 << 3,
        Spell3 =  1 << 4,
    }
}
