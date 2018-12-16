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
      
        public static SpellRecord GetSpell(string spellName)
        {
            return Spells.Find(x => x.Name == spellName);
        }
      
    }
}
