using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum HitResultEnum : byte
    {
        Normal = 0x0,
        Critical = 0x1,
        Dodge = 0x2,
        Miss = 0x3,
    }
}
