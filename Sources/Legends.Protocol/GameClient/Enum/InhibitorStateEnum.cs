using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum InhibitorStateEnum : byte
    {
        Dead = 0x00,
        Alive = 0x01
    }
}
