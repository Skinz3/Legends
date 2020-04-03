using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public enum SpellCastResultEnum
    {
        OK,
        Failed_CastingOrChanneling,
        Failed_Dashing,
        Failed_Cooldown,
        Failed_NoScript,
        Failed_ScriptCriteria,
    }
}
