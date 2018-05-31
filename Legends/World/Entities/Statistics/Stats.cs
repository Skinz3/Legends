using Legends.Core.Protocol.Enum;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    /// <summary>
    /// Stats are used for all unit types.
    /// </summary>
    public abstract class Stats
    {
        public Health Health
        {
            get;
            private set;
        }
        public Health Mana
        {
            get;
            private set;
        }
        public bool IsInvulnerable
        {
            get;
            private set;
        }
        public bool IsPhysicalImmune
        {
            get;
            private set;
        }
        public bool IsMagicImmune
        {
            get;
            private set;
        }
        public bool IsTargetable
        {
            get;
            set;
        }
        public IsTargetableToTeamFlags TargetableToTeam
        {
            get;
            private set;
        }
        public Stat Armor
        {
            get;
            private set;
        }
        public Stat HpRegeneration
        {
            get;
            private set;
        }
        public Stats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor)
        {
            this.Health = new Health(baseHeath);
            this.Mana = new Health(baseMana);
            this.IsInvulnerable = false;
            this.IsPhysicalImmune = false;
            this.IsMagicImmune = false;
            this.IsTargetable = true;
            this.TargetableToTeam = IsTargetableToTeamFlags.TargetableToAll;
            this.Armor = new Stat(baseArmor);
            this.HpRegeneration = new Stat(baseHpRegen);
        }
    }
}
