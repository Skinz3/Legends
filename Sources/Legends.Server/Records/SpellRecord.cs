using Legends.Core.Attributes;
using Legends.Core.DesignPattern;
using Legends.Core.IO.Inibin;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("spells")]
    public class SpellRecord : ITable
    {
        private static List<SpellRecord> Spells = new List<SpellRecord>();

        [InibinFieldFileName]
        public string Name
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_AlternateName)]
        public string AlternateName
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_AnimationName)]
        public string AnimationName
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_MissileSpeed)]
        public float MissileSpeed
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_LineWidth)]
        public float LineWidth
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.SPELLS_CastFrame)]
        public float CastFrame
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_AIRange)]
        public float AIRange
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_CastRange)]
        public float CastRange
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_CastRadius)]
        public float CastRadius
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown)]
        public float Cooldown
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown1)]
        public float Cooldown1
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown2)]
        public float Cooldown2
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown3)]
        public float Cooldown3
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown4)]
        public float Cooldown4
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown5)]
        public float Cooldown5
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Cooldown6)]
        public float Cooldown6
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_DelayCastOffsetPercent)]
        public float DelayCastOffsetPercent
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_Flags)]
        public uint SpellFlags
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_TextFlags)]
        public string TextFlags
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.SPELLS_HitEffectName)]
        public string HitEffectName
        {
            get;
            set;
        }
        [Ignore]
        public SpellFlags Flags
        {
            get
            {
                return (SpellFlags)SpellFlags;
            }
        }
        public float GetCastTime()
        {
            if (DelayCastOffsetPercent == 0)
            {
                return 0f;
            }
            return (1.0f + DelayCastOffsetPercent) / 2.0f;
        }
        public float GetCooldown(byte level)
        {
            if (Cooldown != 0f)
            {
                return Cooldown;
            }
            switch (level)
            {
                case 1:
                    return Cooldown1;
                case 2:
                    return Cooldown2;
                case 3:
                    return Cooldown3;
                case 4:
                    return Cooldown4;
                case 5:
                    return Cooldown5;
                case 6:
                    return Cooldown6;
            }
            return 0f;
        }
        public static SpellRecord GetSpell(string spellName)
        {
            return Spells.Find(x => x.Name == spellName);
        }
        public static SpellRecord GetSpellCaseInsensitive(string spellName)
        {
            return Spells.Find(x => x.Name.ToLower() == spellName.ToLower());
        }
        public static SpellRecord[] GetSpells()
        {
            return Spells.ToArray();
        }
    }
}
