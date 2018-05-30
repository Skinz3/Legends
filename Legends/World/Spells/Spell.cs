using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class Spell
    {
        private SpellRecord Record
        {
            get;
            set;
        }
        public Spell(SpellRecord spellRecord)
        {
            this.Record = spellRecord;
        }
    }
}
