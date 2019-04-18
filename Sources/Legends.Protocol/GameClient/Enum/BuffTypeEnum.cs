using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum BuffTypeEnum : byte
    {
        Internal = 0x0,
        Aura = 0x1,
        CombatEnchancer = 0x2,
        CombatDehancer = 0x3,
        SpellShield = 0x4,
        Stun = 0x5,
        Invisibility = 0x6,
        Silence = 0x7,
        Taunt = 0x8,
        Polymorph = 0x9,
        Slow = 0xA,
        Snare = 0xB,
        Damage = 0xC,
        Heal = 0xD,
        Haste = 0xE,
        SpellImmunity = 0xF,
        PhysicalImmunity = 0x10,
        Invulnerability = 0x11,
        Sleep = 0x12,
        NearSight = 0x13,
        Frenzy = 0x14,
        Fear = 0x15,
        Charm = 0x16,
        Poison = 0x17,
        Suppression = 0x18,
        Blind = 0x19,
        Counter = 0x1A,
        Shred = 0x1B,
        Flee = 0x1C,
        Knockup = 0x1D,
        Knockback = 0x1E,
        Disarm = 0x1F,
    }
}
