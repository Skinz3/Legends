using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.Messages.Extended;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using Legends.World.Spells;
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

        public virtual void InflictDamages(Damages damages)
        {
            Stats.Health.Current -= damages.Amount;
            Game.Send(new DamageDoneMessage(damages.Result, damages.Type, damages.Amount, NetId, damages.Source.NetId));
            UpdateHeath();

            if (Stats.Health.Current <= 0)
            {
                OnDead(damages.Source);
            }
        }
        public virtual void OnDead(Unit source)
        {
            Alive = false;
            Game.Send(new DieMessage(source.NetId, NetId));
        }
        public void UpdateHeath()
        {
            Game.Send(new SetHealthMessage(NetId, 0, Stats.Health.Total, Stats.Health.Current));
        }
    }
}
