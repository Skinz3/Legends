using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class TurretStats : AIStats
    {
        public TurretStats(float baseHeath, float baseMana, float baseHpRegen,
            float baseArmor, float baseAttackDamage, float baseAbilityPower,
            float baseDodge, float baseCriticalHit, float baseMagicResistance, 
            float baseManaRegeneration, float baseAttackRange, float baseAttackSpeed,
            float baseCooldownReduction, float baseArmorPenetration, float baseMagicPenetration,
            float baseLifeSteal, float baseSpellVamp, float baseCCReduction,
            float basePerceptionBubbleRadius, float baseMoveSpeed, float baseModelSize) : 
            base(baseHeath, baseMana, baseHpRegen, baseArmor, baseAttackDamage, baseAbilityPower, baseDodge, baseCriticalHit, baseMagicResistance, baseManaRegeneration, baseAttackRange, baseAttackSpeed, baseCooldownReduction, baseArmorPenetration, baseMagicPenetration, baseLifeSteal, baseSpellVamp, baseCCReduction, basePerceptionBubbleRadius, baseMoveSpeed, baseModelSize)
        {
        }

        public override void UpdateReplication(bool partial = true)
        {
            ReplicationManager.UpdateFloat(Mana.Total,1,0);
            ReplicationManager.UpdateFloat(Mana.Current, 1, 1);
            ReplicationManager.UpdateUInt((uint)ActionState, 1, 2);
            ReplicationManager.UpdateBool(IsMagicImmune, 1, 3);
            ReplicationManager.UpdateBool(IsInvulnerable, 1, 4);
            ReplicationManager.UpdateBool(IsPhysicalImmune, 1, 5);
            ReplicationManager.UpdateBool(IsLifeStealImmune, 1, 6);
            ReplicationManager.UpdateFloat(AttackDamage.BaseValue, 1, 7);
            ReplicationManager.UpdateFloat(Armor.Total, 1, 8);
            ReplicationManager.UpdateFloat(MagicResistance.Total, 1, 90);
            ReplicationManager.UpdateFloat(AttackSpeed.BaseBonus, 1, 10);
            ReplicationManager.UpdateFloat(AttackDamage.FlatBonus, 1, 11);
            ReplicationManager.UpdateFloat(AttackDamage.PercentBonus, 1, 12);
            ReplicationManager.UpdateFloat(AbilityPower.BaseBonus, 1, 13);
            ReplicationManager.UpdateFloat(HpRegeneration.Total, 1, 14);
            ReplicationManager.UpdateFloat(Health.Current, 3, 0);
            ReplicationManager.UpdateFloat(Health.Total, 3, 1);
            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.Total, 3, 2);
            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.PercentBonus, 3, 3);
            ReplicationManager.UpdateFloat(MoveSpeed.Total, 3, 4);
            ReplicationManager.UpdateFloat(ModelSize.Total, 3, 5);
            ReplicationManager.UpdateBool(IsTargetable, 5, 0);
            ReplicationManager.UpdateUInt((uint)TargetableToTeam, 5, 1);
        }
    }
}
