using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.Enum
{
    [Flags]
    public enum IsTargetableToTeamFlags : uint
    {
        NonTargetableAlly = 1 << 23,
        NonTargetableEnemy = 1 << 24,
        TargetableToAll = 1 << 22
    }
}
