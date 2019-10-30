using Legends.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using Legends.World.Entities.AI;
using Legends.Scripts.Spells;
using Legends.Protocol.GameClient.Enum;

namespace Legends.World.Spells
{
    public class SpellProvider : Singleton<SpellProvider>
    {
        public Spell GetSpell(AIUnit owner, byte slot, string spellName, bool isSummonerSpell = false)
        {
            SpellRecord record = null;

            record = SpellRecord.GetSpell(spellName);

            if (record != null)
            {
                return new Spell(owner, record, slot, SpellScriptManager.Instance.GetSpellScript(record, owner), isSummonerSpell);
            }
            else
            {
                return null;
            }
        }
    }
}
