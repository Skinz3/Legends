using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum StatSecondaryId : uint
    {
        FM1_Gold = 0,
        FM1_Gold_Total = 1,
        FM1_Spells_Enabled = 0x00000004, // Lower bits. Bits: 0-3 -> Q-R, 4-9 -> Items, 10 -> Trinket
        FM1_Spells_Enabled2 = 0x00000008, // Upper bits
        FM1_SummonerSpells_Enabled = 0x00000010, // Lower bits. Bits: 0 -> D, 1 -> F
        FM1_SummonerSpells_Enabled2 = 0x00000020, // Upper bits
        FM1_EvolvePoints = 0x00000040,
        FM1_EvolveFlags = 0x00000080,
        FM1_ManaCost0 = 0x00000100,
        FM1_ManaCost1 = 0x00000200,
        FM1_ManaCost2 = 0x00000400,
        FM1_ManaCost3 = 0x00000800,
        FM1_ManaCostEx0 = 0x00001000,
        FM1_ManaCostEx1 = 0x00002000,
        FM1_ManaCostEx2 = 0x00004000,
        FM1_ManaCostEx3 = 0x00008000,
        FM1_ManaCostEx4 = 0x00010000,
        FM1_ManaCostEx5 = 0x00020000,
        FM1_ManaCostEx6 = 0x00040000,
        FM1_ManaCostEx7 = 0x00080000,
        FM1_ManaCostEx8 = 0x00100000,
        FM1_ManaCostEx9 = 0x00200000,
        FM1_ManaCostEx10 = 0x00400000,
        FM1_ManaCostEx11 = 0x00800000,
        FM1_ManaCostEx12 = 0x01000000,
        FM1_ManaCostEx13 = 0x02000000,
        FM1_ManaCostEx14 = 0x04000000,
        FM1_ManaCostEx15 = 0x08000000,
        FM2_Base_Ad = 0x00000020, // champ's base ad that increase every level. No item bonus should be added here
        FM2_Base_Ap = 0x00000040,
        FM2_Crit_Chance = 0x00000100, // 0.5 = 50%
        FM2_Armor = 0x00000200,
        FM2_Magic_Armor = 0x00000400,
        FM2_HpRegen = 0x00000800,
        FM2_MpRegen = 0x00001000,
        FM2_Range = 0x00002000,
        FM2_Bonus_Ad_Flat = 0x00004000, // AD flat bonuses
        FM2_Bonus_Ad_Pct = 0x00008000, // AD percentage bonuses. 0.5 = 50%
        FM2_Bonus_Ap_Flat = 0x00010000, // AP flat bonuses
        FM2_Bonus_Ap_Pct = 0x00020000, // AP flat bonuses
        FM2_Atks_multiplier = 0x00080000, // Attack speed multiplier. If set to 2 and champ's base attack speed is 0.600, then his new AtkSpeed becomes 1.200
        FM2_cdr = 0x00400000, // Cooldown reduction. 0.5 = 50%
        FM2_Armor_Pen_Flat = 0x01000000,
        FM2_Armor_Pen_Pct = 0x02000000, // Armor pen. 0.5 = 50%
        FM2_Magic_Pen_Flat = 0x04000000,
        FM2_Magic_Pen_Pct = 0x08000000,
        FM2_LifeSteal = 0x10000000, //Life Steal. 0.5 = 50%
        FM2_SpellVamp = 0x20000000, //Spell Vamp. 0.5 = 50%
        FM2_Tenacity = 0x40000000, //Tenacity. 0.5 = 50%

        FM3_Armor_Pen_Pct = 0x00000001, //Armor pen. 1.7 = 30% -- These are probably consequence of some bit ops
        FM3_Magic_Pen_Pct = 0x00000002, //Magic pen. 1.7 = 30%

        FM4_CurrentHp = 0x00000001,
        FM4_CurrentMana = 0x00000002,
        FM4_MaxHp = 0x00000004,
        FM4_MaxMp = 0x00000008,
        FM4_exp = 0x00000010,
        FM4_Vision1 = 0x00000100,
        FM4_Vision2 = 0x00000200,
        FM4_Speed = 0x00000400,
        FM4_ModelSize = 0x00000800,
        FM4_Level = 0x00004000, //Champion Level
        FM4_Unk = 0x00010000 //Unk -> Transparent-ish Life bar when changed :: MAYBE IF UNIT IS TARGETABLE
    }
}
