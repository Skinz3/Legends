using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.Enum
{
    public enum UnitAnnounceEnum : byte
    {
        Death = 0x04,
        InhibitorDestroyed = 0x1F,
        InhibitorAboutToSpawn = 0x20,
        InhibitorSpawned = 0x21,
        TurretDestroyed = 0x24,
        SummonerLeft = 0x46,
        SummonerQuit = 0x47,
        SummonerReconnected = 0x48
    }
}
