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
        public Spell GetSpell(AIUnit owner, byte slot, string spellName)
        {
            SpellRecord record = owner.Record.GetSpellRecord(spellName);

            if (record != null)
            {
                return new Spell(owner, record, slot, SpellScriptManager.Instance.GetSpellScript(record, owner), false);

            }
            else
            {
                return null;
            }
        }
        public Spell GetSpell(AIUnit owner, byte slot, SummonerSpellId summonerSpell)
        {
            SpellRecord record = SpellRecord.GetSpell(summonerSpell.ToString());

            if (record != null)
            {
                return new Spell(owner, record, slot, SpellScriptManager.Instance.GetSpellScript(record, owner), true);

            }
            else
            {
                return null;
            }
        }
    }
}
