using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class SpellManager
    {
        public event Action<Spell, Vector2, Vector2> OnSpellStartCast;

        public AIUnit Owner
        {
            get;
            private set;
        }
        private Dictionary<byte, SpellSlot> Spells
        {
            get;
            set;
        }
        public SpellManager(AIUnit owner)
        {
            this.Owner = owner;
            this.Spells = new Dictionary<byte, SpellSlot>();
            SetSpells(Owner.Record);
        }
        public void Update(float deltaTime)
        {
            foreach (var spell in Spells)
            {
                spell.Value.Update(deltaTime);
            }
        }
        public void AddSpell(byte slot, Spell spell)
        {
            if (spell != null)
            {
                if (Spells.ContainsKey(slot))
                {
                    Spells[slot].Push(spell);
                }
                else
                {
                    SpellSlot spellSlot = new SpellSlot();
                    spellSlot.Push(spell);
                    Spells.Add(slot, spellSlot);
                }
            }
        }
        public void SetSpells(AIUnitRecord record)
        {
            Spells.Clear();
            AddSpell(0, SpellProvider.Instance.GetSpell(Owner, 0, record.Spell1));
            AddSpell(1, SpellProvider.Instance.GetSpell(Owner, 1, record.Spell2));
            AddSpell(2, SpellProvider.Instance.GetSpell(Owner, 2, record.Spell3));
            AddSpell(3, SpellProvider.Instance.GetSpell(Owner, 3, record.Spell4));
        }
        public void LowerCooldowns(float value)
        {
            for (byte i = 0; i <= 3; i++)
            {
                var spell = Owner.SpellManager.GetSpell(i);

                if (spell.Level > 0)
                {
                    spell.LowerCooldown(value);
                }
            }
        }
        public void UpgradeSpell(byte spellId)
        {
            Spell targetSpell = GetSpell(spellId);
            targetSpell.Upgrade(spellId);
            Owner.OnSpellUpgraded(spellId, targetSpell);
        }
        public Spell GetSpell(string name)
        {
            foreach (var slot in Spells.Values)
            {
                var spell = slot.Find(name);

                if (spell != null)
                {
                    return spell;
                }
            }
            return null;
        }
        public Spell GetSpell(byte slot)
        {
            return Spells[slot].Current;
        }
        public bool IsChanneling()
        {
            foreach (var spell in Spells)
            {
                if (spell.Value.Current.State == SpellStateEnum.STATE_CHANNELING)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsAtSlot(byte slotId, string spellName)
        {
            return Spells[slotId].Find(spellName) != null;
        }

        public Spell Pop(byte slotId)
        {
            return Spells[slotId].Pop();
        }

        public Spell GetCurrent(byte slotId)
        {
            return Spells[slotId].Current;
        }
    }
    public class SpellSlot
    {
        private Stack<Spell> Spells
        {
            get;
            set;
        }
        public Spell Current
        {
            get
            {
                return Spells.First();
            }
        }
        public SpellSlot()
        {
            this.Spells = new Stack<Spell>();
        }
        public void Push(Spell spell)
        {
            Spells.Push(spell);
        }
        public void Update(float deltaTime)
        {
            foreach (var spell in Spells)
            {
                spell.Update(deltaTime);
            }
        }
        public Spell Pop()
        {
            return Spells.Pop();
        }
        public Spell Find(string name)
        {
            return Spells.FirstOrDefault(x => x.Record.Name == name);
        }
    }
}