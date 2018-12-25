using ENet;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.World.Entities.AI.Deaths;
using Legends.World.Entities.Loot;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using Legends.World.Games;
using Legends.World.Items;
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
        /// <summary>
        /// (AttackableUnit = Me,Unit (should be DeathCauses)  = source)
        /// </summary>
        public event Action<AttackableUnit, Unit> OnDeadEvent;
        /// <summary>
        /// (AttackableUnit = Me,Unit = source)
        /// </summary>
        public event Action<AttackableUnit, Unit> OnReviveEvent;

        public Stats Stats
        {
            get;
            set;
        }
        public ShieldValues Shields
        {
            get;
            private set;
        }
        public Inventory Inventory
        {
            get;
            private set;
        }
        public override bool IsMoving => false;

        public abstract float SelectionRadius
        {
            get;
        }

        public abstract float PathfindingCollisionRadius
        {
            get;
        }

        public AttackableUnit(uint netId) : base(netId)
        {
            this.Inventory = new Inventory(this);
            this.Shields = new ShieldValues();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public virtual int GetGoldFromLastHit
        {
            get
            {
                return 0;
            }
        }
        public virtual void OnItemAdded(Item item)
        {

        }
        public virtual void OnItemRemoved(Item item)
        {

        }
        public virtual void AddGold(float value, bool floatingText = false)
        {
            Stats.Gold += value;
            Stats.GoldTotal += value;
        }
        public virtual void RemoveGold(float value)
        {
            Stats.Gold -= value;
        }
        /// <summary>
        /// todo ? for attackable unit only?
        /// </summary>
        public virtual void UpdateStats(bool partial = true)
        {
            ReplicationStats stats = Stats as ReplicationStats;

            if (stats == null)
            {
                throw new Exception("Try to update stats from non replicable object.");
            }

            stats.UpdateReplication(partial);
            Game.Send(new UpdateStatsMessage(0, NetId, stats.ReplicationManager.Values, partial));

            if (partial)
            {
                foreach (var x in stats.ReplicationManager.Values)
                {
                    if (x != null)
                    {
                        x.Changed = false;
                    }
                }
            }
        }
        public virtual void OnShieldModified(bool magical, bool physical, float value)
        {
            SendVision(new ModifyShieldMessage(NetId, physical, magical, true, value));
        }
        protected ShieldValues GetShieldValues()
        {
            return Shields.Ignorable() ? null : Shields;
        }

        public virtual void InflictDamages(Damages damages)
        {
            damages.Apply();

            if (!Stats.IsLifeStealImmune)
                damages.Source.Stats.Health.Heal(damages.Delta * damages.Source.Stats.LifeSteal.TotalSafe);

            if (Shields.Magical > 0 && damages.Type == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                float value = Shields.UseMagicalShield(damages.Delta);
                Stats.Health.Current -= value;
                float shieldLoss = -(damages.Delta - value);
                OnShieldModified(true, false, shieldLoss);
            }
            else if (Shields.Physical > 0 && damages.Type == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                float value = Shields.UsePhysicalShield(damages.Delta);
                Stats.Health.Current -= value;
                float shieldLoss = -(damages.Delta - value);
                OnShieldModified(false, true, shieldLoss);
            }
            else if (Shields.MagicalAndPhysical > 0)
            {

                float value = Shields.UseMagicalAndPhysicalShield(damages.Delta);
                Stats.Health.Current -= value;
                float shieldLoss = -(damages.Delta - value);
                OnShieldModified(true, true, shieldLoss);
            }
            else
            {
                Stats.Health.Current -= damages.Delta;
                Game.Send(new DamageDoneMessage(damages.Result, damages.Type, damages.Delta, NetId, damages.Source.NetId));
            }



            UpdateStats();
            damages.Source.UpdateStats();
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

            foreach (var oposedTeam in Team.GetOposedTeams())
            {
                if (oposedTeam.HasVision(this))
                {
                    oposedTeam.Send(message);
                }
            }


        }
        [InDevelopment(InDevelopmentState.TODO, "The parametters should be DeathDescription.cs for assits")]
        public virtual void OnDead(AttackableUnit source)
        {
            Alive = false;
            ApplyGoldLoot(source);
            ApplyExperienceLoot(source);
            OnDeadEvent?.Invoke(this, source);
        }

        protected abstract void ApplyGoldLoot(AttackableUnit source);
        protected abstract void ApplyExperienceLoot(AttackableUnit source);

        public virtual void OnRevive(AttackableUnit source)
        {
            Alive = true;
            OnReviveEvent?.Invoke(this, source);
        }
        public virtual void UpdateHeath()
        {
            Game.Send(new OnEnterLocalVisiblityClient(NetId, Stats.Health.TotalSafe, Stats.Health.Current));
        }
    }
}
