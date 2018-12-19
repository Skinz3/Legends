using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.World.Entities.AI;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using Legends.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public abstract class AIStats : ReplicationStats
    {
        public AIStats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor, float baseAttackDamage,
            float baseAbilityPower, float baseDodge, float baseCriticalHit, float baseMagicResistance, float baseManaRegeneration,
            float baseAttackRange, float baseAttackSpeed, float attackDelayPercent, float baseCooldownReduction, float baseArmorPenetration,
            float baseMagicPenetration, float baseLifeSteal, float baseSpellVamp, float baseCCReduction, float basePerceptionBubbleRadius,
            float baseMoveSpeed, float baseModelSize) : base(baseHeath, baseMana, baseHpRegen, baseArmor, baseAttackDamage,
            baseAbilityPower, baseDodge, baseCriticalHit, baseMagicResistance, baseManaRegeneration,
            baseAttackRange, baseAttackSpeed, attackDelayPercent, baseCooldownReduction, baseArmorPenetration,
            baseMagicPenetration, baseLifeSteal, baseSpellVamp, baseCCReduction, basePerceptionBubbleRadius,
            baseMoveSpeed, baseModelSize)
        {
            
        }

    }
}
