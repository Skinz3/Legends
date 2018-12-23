using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum TeamId : int
    {
        UNKNOWN = 0x00,
        BLUE = 0x64,
        PURPLE = 0xC8,
        NEUTRAL = 0x12C,
    };
}
