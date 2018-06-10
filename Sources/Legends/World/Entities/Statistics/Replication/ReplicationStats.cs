using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public abstract class ReplicationStats : Stats
    {
        public ReplicationManager ReplicationManager
        {
            get;
            private set;
        }
        public ReplicationStats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor, float baseAttackDamage,
            float baseAbilityPower, float baseDodge, float baseCriticalHit, float baseMagicResistance, float baseManaRegeneration,
            float baseAttackRange, float baseAttackSpeed, float attackDelayPercent, float baseCooldownReduction, float baseArmorPenetration,
            float baseMagicPenetration, float baseLifeSteal, float baseSpellVamp, float baseCCReduction, float basePerceptionBubbleRadius,
            float baseMoveSpeed, float baseModelSize) : base(baseHeath, baseMana, baseHpRegen, baseArmor, baseAttackDamage,
            baseAbilityPower, baseDodge, baseCriticalHit, baseMagicResistance, baseManaRegeneration,
            baseAttackRange, baseAttackSpeed, attackDelayPercent, baseCooldownReduction, baseArmorPenetration,
            baseMagicPenetration, baseLifeSteal, baseSpellVamp, baseCCReduction, basePerceptionBubbleRadius,
            baseMoveSpeed, baseModelSize)
        {
            this.ReplicationManager = new ReplicationManager();
        }

        public abstract void UpdateReplication(bool partial = true);

    }
}
