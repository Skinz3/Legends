using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.Enum
{
    public enum MovementType : uint
    {
        EMOTE = 1,
        MOVE = 2,
        ATTACK = 3,
        ATTACKMOVE = 7,
        STOP = 10,
    }
}
