using Legends.Records;
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
            SetSpells(Owner.Record);
        }
        public void Update(float deltaTime)
        {
            Spell1?.Update(deltaTime);
            Spell2?.Update(deltaTime);
            Spell3?.Update(deltaTime);
            Spell4?.Update(deltaTime);
        }
        public void SetSpells(AIUnitRecord record)
        {
            this.Spell1 = SpellProvider.Instance.GetSpell(Owner,0,record.Spell1);
            this.Spell2 = SpellProvider.Instance.GetSpell(Owner,1, record.Spell2);
            this.Spell3 = SpellProvider.Instance.GetSpell(Owner,2, record.Spell3);
            this.Spell4 = SpellProvider.Instance.GetSpell(Owner,3, record.Spell4);
        }
        public void UpgradeSpell(byte spellId)
        {
            Spell targetSpell = GetSpell(spellId);
            targetSpell.Upgrade(spellId);
            Owner.OnSpellUpgraded(spellId, targetSpell);
        }
        public Spell GetSpell(byte slot)
        {
            switch (slot)
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