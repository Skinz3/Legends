using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities
{
    public abstract class AttackableUnit : Unit
    {
        public bool Alive
        {
            get;
            private set;
        }
        public Stats Stats
        {
            get;
            protected set;
        }
        public override bool IsMoving => false;

        public AttackableUnit()
        {
            Alive = true;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// todo ? for attackable unit only?
        /// </summary>
        public abstract void UpdateStats(bool partial);

        public void UpdateHeath()
        {
            Game.Send(new SetHealthMessage(NetId, 0x0000, Stats.Health.Total, Stats.Health.Current));
        }
    }
}
