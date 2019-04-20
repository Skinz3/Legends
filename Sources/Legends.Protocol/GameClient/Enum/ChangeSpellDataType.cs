using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum ChangeSlotSpellDataType : byte
    {
        TargetingType = 0x0,
        SpellName = 0x1,
        Range = 0x2,
        MaxGrowthRange = 0x3,
        RangeDisplay = 0x4,
        IconIndex = 0x5,
        OffsetTarget = 0x6,
    }
}
