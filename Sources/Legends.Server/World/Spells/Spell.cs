using Legends.Core.DesignPattern;
using Legends.Records;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class Spell : IUpdatable
    {
        public const byte NORMAL_SPELL_LEVELS = 5;

        public const byte ULTIMATE_SPELL_LEVELS = 3;

        public SpellRecord Record
        {
            get;
            private set;
        }
        public AIUnit Owner
        {
            get;
            private set;
        }
        public byte Level
        {
            get;
            private set;
        }
        public Spell(AIUnit owner, SpellRecord record)
        {
            this.Owner = owner;
            this.Record = record;
        }
        [InDevelopment(InDevelopmentState.STARTED, "Slot max")]
        public bool Upgrade(byte id)
        {
            // 3 = Ultimate Spell.
            byte maxLevel = id == 3 ? ULTIMATE_SPELL_LEVELS : NORMAL_SPELL_LEVELS;

            if (Level + 1 <= maxLevel)
            {
                Level++;
                this.Owner.Stats.SkillPoints--;
                return true;
            }
            return false;
        }
        public void Update(long deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
