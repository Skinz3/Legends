using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    [Flags]
    public enum StatActionStateEnum : uint
    {
        CanAttack = 1 << 0,
        CanCast = 1 << 1,
        CanMove = 1 << 2,
        Stealthed = 1 << 3,
        RevealSpecificUnit = 1 << 4,
        Taunted = 1 << 5,
        Feared = 1 << 6,
        Suppressed = 1 << 7,
        Sleeping = 1 << 8,
        NearSighted = 1 << 9,
        Ghosted = 1 << 10,
        GhostProof = 1 << 11,
        Charmed = 1 << 12,
        NoRender = 1 << 13,
        ForceRenderParticles = 1 << 14,
        DodgePiercing = 1 << 15,
        DisableAmbientGold = 1 << 16,
        BrushVisibilityFake = 1 << 17,
        Unknown = 1 << 23 // set to 1 by default
    }
}
