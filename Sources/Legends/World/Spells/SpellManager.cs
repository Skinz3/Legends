using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class SpellManager
    {
        public AIUnit Owner
        {
            get;
            private set;
        }
        public Spell Spell1
        {
            get;
            private set;
        }
        public Spell Spell2
        {
            get;
            private set;
        }
        public Spell Spell3
        {
            get;
            private set;
        }
        public Spell Spell4
        {
            get;
            private set;
        }
        public SpellManager(AIUnit owner)
        {
            this.Owner = owner;
            this.Spell1 = SpellProvider.Instance.GetSpell(Owner, owner.Record.Spell1);
            this.Spell2 = SpellProvider.Instance.GetSpell(Owner, owner.Record.Spell2);
            this.Spell3 = SpellProvider.Instance.GetSpell(Owner, owner.Record.Spell3);
            this.Spell4 = SpellProvider.Instance.GetSpell(Owner, owner.Record.Spell4);
        }

        public void UpgradeSpell(byte spellId)
        {
            Spell targetSpell = GetSpell(spellId);
            targetSpell.Upgrade(spellId);
            Owner.OnSpellUpgraded(spellId, targetSpell);
        }
        public Spell GetSpell(byte spellId)
        {
            switch (spellId)
            {
                case 0:
                    return Spell1;
                case 1:
                    return Spell2;
                case 2:
                    return Spell3;
                case 3:
                    return Spell4;
            }
            return null;
        }
    }
}