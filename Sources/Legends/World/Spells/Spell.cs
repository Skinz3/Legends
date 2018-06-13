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
        public bool Upgrade()
        {
            if (Level <= 3)
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
