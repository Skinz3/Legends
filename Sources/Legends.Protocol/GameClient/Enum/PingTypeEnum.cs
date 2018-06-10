using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum PingTypeEnum : byte
    {
        Ping_Default = 0,
        Ping_Danger = 2,
        Ping_Missing = 3,
        Ping_OnMyWay = 4,
        Ping_Assist = 6
    }
}
