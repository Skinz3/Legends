using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum AnnounceEnum : byte
    {
        WelcomeToSR = 0x77,
        ThirySecondsToMinionsSpawn = 0x78,
        MinionsHaveSpawned = 0x7F,
        MinionsHaveSpawned2 = 0x76
    }
}
