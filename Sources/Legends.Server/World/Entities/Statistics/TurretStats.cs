using Legends.Records;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class TurretStats : AIStats
    {
        public const float DEFAULT_PERCEPTION_BUBBLE_RADIUS = 1000;

        public override bool IsLifeStealImmune => true;

        public override bool IsCriticalImmune => true;

      
        public TurretStats(AIUnitRecord record, BuildingRecord buildingRecord) : base((float)record.BaseHp, (float)record.BaseMp, (float)record.BaseHpRegen, (float)record.BaseArmor,
             (float)record.BaseDamage, record.BaseAbilityPower, (float)record.BaseDodge, (float)record.BaseCritChance, (float)record.BaseMagicResist,
             (float)record.BaseMpRegen, record.AttackRange, (float)record.BaseAttackSpeed, (float)record.AttackDelayOffsetPercent, AIHero.DEFAULT_COOLDOWN_REDUCTION,
             0, 0, 0, 0, 0, DEFAULT_PERCEPTION_BUBBLE_RADIUS, record.BaseMovementSpeed, 1)
        {
           
        }
        public override void UpdateReplication(bool partial = true)
        {
            ReplicationManager.UpdateFloat(Mana.TotalSafe, 1, 0);
            ReplicationManager.UpdateFloat(Mana.Current, 1, 1);
            ReplicationManager.UpdateUInt((uint)ActionState, 1, 2);
            ReplicationManager.UpdateBool(IsMagicImmune, 1, 3);
            ReplicationManager.UpdateBool(IsInvulnerable, 1, 4);
            ReplicationManager.UpdateBool(IsPhysicalImmune, 1, 5);
            ReplicationManager.UpdateBool(IsLifeStealImmune, 1, 6);
            ReplicationManager.UpdateFloat(AttackDamage.BaseValue, 1, 7);
            ReplicationManager.UpdateFloat(Armor.TotalSafe, 1, 8);
            ReplicationManager.UpdateFloat(MagicResistance.TotalSafe, 1, 9);
            ReplicationManager.UpdateFloat(AttackSpeed.BaseBonus, 1, 10);
            ReplicationManager.UpdateFloat(AttackDamage.FlatBonus, 1, 11);
            ReplicationManager.UpdateFloat(AttackDamage.PercentBonus, 1, 12);
            ReplicationManager.UpdateFloat(AbilityPower.BaseBonus, 1, 13);
            ReplicationManager.UpdateFloat(HealthRegeneration.TotalSafe, 1, 14);
            ReplicationManager.UpdateFloat(Health.Current, 3, 0);
            ReplicationManager.UpdateFloat(Health.TotalSafe, 3, 1);
            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.TotalSafe, 3, 2);
            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.PercentBonus, 3, 3);
            ReplicationManager.UpdateFloat(MoveSpeed.TotalSafe, 3, 4);
            ReplicationManager.UpdateFloat(ModelSize.TotalSafe, 3, 5);
            ReplicationManager.UpdateBool(IsTargetable, 5, 0);
            ReplicationManager.UpdateUInt((uint)TargetableToTeam, 5, 1);
        }
    }
}
