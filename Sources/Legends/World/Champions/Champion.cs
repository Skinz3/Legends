using Legends.Protocol.GameClient.Enum;
using Legends.Network;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Champions
{
    public abstract class Champion
    {
        public AIHero Player
        {
            get;
            private set;
        }
        protected Spell Q
        {
            get;
            private set;
        }
        protected Spell W
        {
            get;
            private set;
        }
        protected Spell E
        {
            get;
            private set;
        }
        protected Spell R
        {
            get;
            private set;
        }

            
        public Champion(AIHero player)
        {
            this.Player = player;
        }
       

        public abstract void ApplyQ();

        public abstract void ApplyW();

        public abstract void ApplyE();

        public abstract void ApplyR();

    }
}
