using Legends.Core.Attributes;
using Legends.Core.IO.Inibin;
using SmartORM;
using SmartORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("/Database/Spells")]
    public class SpellRecord : ITable
    {
        [JsonCache]
        private static List<SpellRecord> Spells = new List<SpellRecord>();

        [InibinFieldFileName]
        [JsonFileName]
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
