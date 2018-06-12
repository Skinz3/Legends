using Legends.Records;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class BuildingStats : ReplicationStats
    {
        public override bool IsLifeStealImmune => true;

        public override bool IsCriticalImmune => true;

        public BuildingStats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor, float baseAttackDamage,
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
        public BuildingStats(BuildingRecord record) : base(record.Health, record.Mana, record.BaseStaticHpRegen, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, record.PerceptionBubbleRadius, 0, 1f)
        {

        }
        public override void UpdateReplication(bool partial = true)
        {
            ReplicationManager.UpdateFloat(Health.Current, 1, 0);
            ReplicationManager.UpdateBool(IsInvulnerable, 1, 1);
            ReplicationManager.UpdateBool(IsTargetable, 5, 0);
            ReplicationManager.UpdateUInt((uint)TargetableToTeam, 5, 1);
        }
    }
}
