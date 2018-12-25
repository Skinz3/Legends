using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum NetNodeEnum : byte
    {
        Spawned = 0x40,
        Map = 0xFF,
    }
}
