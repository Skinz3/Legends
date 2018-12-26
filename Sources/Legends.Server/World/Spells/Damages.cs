using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Enum;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class Damages
    {
        public AIUnit Source
        {
            get;
            private set;
        }
        public AttackableUnit Target
        {
            get;
            private set;
        }
        public float Delta
        {
            get;
            private set;
        }
        public DamageType Type
        {
            get;
            private set;
        }
        public DamageResultEnum Result
        {
            get;
            private set;
        }
        public bool Critical
        {
            get;
            private set;
        }
        public bool ApplyAutoAttack
        {
            get;
            private set;
        }
        public Damages(AIUnit source, AttackableUnit target, float delta, bool critical, DamageType type, bool applyAutoAttack)
        {
            this.Source = source;
            this.Target = target;
            this.Delta = delta;
            this.Type = type;
            this.Critical = critical;
            this.Result = GenerateResult();
            this.ApplyAutoAttack = applyAutoAttack;
        }



        [InDevelopment(InDevelopmentState.TODO, "Just todo ^.^")]
        private DamageResultEnum GenerateResult()
        {
            if (Target.Stats.IsInvulnerable)
            {
                return DamageResultEnum.DAMAGE_TEXT_INVULNERABLE;
            }
            if (Target.Stats.IsPhysicalImmune && Type == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                return DamageResultEnum.DAMAGE_TEXT_INVULNERABLE;
            }
            if (Target.Stats.IsTargetable == false)
            {
                return DamageResultEnum.DAMAGE_TEXT_MISS;
            }
            if (Target.Stats.IsMagicImmune && Type == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                return DamageResultEnum.DAMAGE_TEXT_INVULNERABLE;
            }


            if (Critical)
            {
                return DamageResultEnum.DAMAGE_TEXT_CRITICAL;
            }
            else
            {
                return DamageResultEnum.DAMAGE_TEXT_NORMAL;
            }
        }
        /// <summary>
        /// http://fr.leagueoflegends.wikia.com/wiki/Armure
        /// etc
        /// </summary>
        public void Apply()
        {
            if (Result == DamageResultEnum.DAMAGE_TEXT_MISS || Result == DamageResultEnum.DAMAGE_TEXT_INVULNERABLE ||
                 Result == DamageResultEnum.DAMAGE_TEXT_DODGE)
            {
                Delta = 0;
                return;
            }

            if (Critical)
            {
                Delta *= Source.Stats.CriticalDamageRatio.TotalSafe;
            }

            if (Type == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                ApplyBasicReduction(Target.Stats.Armor.TotalSafe);
            }
            else if (Type == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                ApplyBasicReduction(Target.Stats.MagicResistance.TotalSafe);
            }

        }
        private void ApplyBasicReduction(float reductionStat)
        {
            if (reductionStat >= 0)
            {
                Delta *= (100f / (100 + reductionStat));
            }
            else
            {
                Delta *= (2 - 100 / (100 - reductionStat));
            }
        }
    }
}
