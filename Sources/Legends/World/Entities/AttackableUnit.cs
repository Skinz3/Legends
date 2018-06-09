using ENet;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.Messages.Extended;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using Legends.World.Games;
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
       
        public Stats Stats
        {
            get;
            protected set;
        }
        public override bool IsMoving => false;

        public AttackableUnit()
        {
             
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// todo ? for attackable unit only?
        /// </summary>
        public abstract void UpdateStats(bool partial = true);

        public virtual void InflictDamages(Damages damages)
        {
            damages.Apply();
            
            Stats.Health.Current -= damages.Delta;
            Game.Send(new DamageDoneMessage(damages.Result, damages.Type, damages.Delta, NetId, damages.Source.NetId));
            UpdateStats();

            if (Stats.Health.Current <= 0)
            {
                OnDead(damages.Source);
            }
        }
        /// <summary>
        /// Envoit un message a tous les joueurs possédant la vision sur ce joueur.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        public void SendVision(Message message, Channel channel = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            Team.Send(message, channel, flags);

            Team oposedTeam = GetOposedTeam();

            if (oposedTeam.HasVision(this))
            {
                oposedTeam.Send(message);
            }
        }
        [InDeveloppement(InDeveloppementState.TODO, "The parametters should be DeathDescription.cs for assits")]
        public virtual void OnDead(AttackableUnit source)
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
