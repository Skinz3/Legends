using Legends.Core.Attributes;
using Legends.Core.IO.Inibin;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
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
        public float GetCastTime()
        {
            return (1.0f + DelayCastOffsetPercent) / 2.0f;
        }
        public static SpellRecord GetSpell(string spellName)
        {
            return Spells.Find(x => x.Name == spellName);
        }

    }
}
