using Legends.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using Legends.World.Entities.AI;

namespace Legends.World.Spells
{
    public class SpellProvider : Singleton<SpellProvider>
    {
        [InDevelopment(InDevelopmentState.TODO, "Add spell script")]
        public Spell GetSpell(AIUnit owner, string spellName)
        {
            SpellRecord record = owner.Record.GetSpellRecord(spellName);

            if (record != null)
            {
                return new Spell(owner, record);
            }
            else
            {
                return null;
            }
        }
    }
}
