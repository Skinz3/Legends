using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    [Flags]
    public enum IsTargetableToTeamFlags : uint
    {
        NonTargetableAlly = 1 << 23,
        NonTargetableEnemy = 1 << 24,
        TargetableToAll = 1 << 22
    }
}
