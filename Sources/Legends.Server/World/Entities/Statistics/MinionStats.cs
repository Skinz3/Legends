using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class MinionStats : AIStats
    {
        public const float DEFAULT_MINION_MODEL_SIZE = 1;

        public MinionStats(AIUnitRecord record) : base((float)record.BaseHp, (float)record.BaseMp, (float)record.BaseHpRegen, (float)record.BaseArmor,
           (float)record.BaseDamage, record.BaseAbilityPower, (float)record.BaseDodge, (float)record.BaseCritChance, (float)record.BaseMagicResist,
           (float)record.BaseMpRegen, record.AttackRange, (float)record.BaseAttackSpeed, (float)record.AttackDelayOffsetPercent, 0,
           0, 0, 0, 0, 0, (float)record.PerceptionBubbleRadius, record.BaseMovementSpeed, DEFAULT_MINION_MODEL_SIZE) 
        {

        }

        public override bool IsLifeStealImmune => false;

        public override bool IsCriticalImmune => false;

        public override void UpdateReplication(bool partial = true)
        {
            ReplicationManager.UpdateFloat(Health.Current, 1, 0); //mHP
            ReplicationManager.UpdateFloat(Health.TotalSafe, 1, 1); //mMaxHP
            // ReplicationManager.UpdateFloat(LifeTime, 1, 2); //mLifetime
            // ReplicationManager.UpdateFloat(MaxLifeTime, 1, 3); //mMaxLifetime
            // ReplicationManager.UpdateFloat(LifeTimeTicks, 1, 4); //mLifetimeTicks
            // ReplicationManager.UpdateFloat(ManaPoints.TotalSafe, 1, 5); //mMaxMP
            // ReplicationManager.UpdateFloat(CurrentMana, 1, 6); //mMP
            ReplicationManager.UpdateUInt((uint)ActionState, 1, 7); //ActionState
            ReplicationManager.UpdateBool(IsMagicImmune, 1, 8); //MagicImmune
            ReplicationManager.UpdateBool(IsInvulnerable, 1, 9); //IsInvulnerable
            ReplicationManager.UpdateBool(IsPhysicalImmune, 1, 10); //IsPhysicalImmune
            ReplicationManager.UpdateBool(IsLifeStealImmune, 1, 11); //IsLifestealImmune
            ReplicationManager.UpdateFloat(AttackDamage.BaseValue, 1, 12); //mBaseAttackDamage
            ReplicationManager.UpdateFloat(Armor.TotalSafe, 1, 13); //mArmor
            ReplicationManager.UpdateFloat(MagicResistance.TotalSafe, 1, 14); //mSpellBlock
            ReplicationManager.UpdateFloat(0, 1, 15); //mAttackSpeedMod
            ReplicationManager.UpdateFloat(AttackDamage.FlatBonus, 1, 16); //mFlatPhysicalDamageMod
            ReplicationManager.UpdateFloat(AttackDamage.PercentBonus, 1, 17); //mPercentPhysicalDamageMod
            ReplicationManager.UpdateFloat(AbilityPower.TotalSafe, 1, 18); //mFlatMagicDamageMod
            ReplicationManager.UpdateFloat(HealthRegeneration.TotalSafe, 1, 19); //mHPRegenRate
            ReplicationManager.UpdateFloat(ManaRegeneration.TotalSafe, 1, 20); //mPARRegenRate
            ReplicationManager.UpdateFloat(MagicResistance.FlatBonus, 1, 21); //mFlatMagicReduction
            ReplicationManager.UpdateFloat(MagicResistance.PercentBonus, 1, 22); //mPercentMagicReduction
            ReplicationManager.UpdateFloat(0, 3, 0); //mFlatBubbleRadiusMod
            ReplicationManager.UpdateFloat(0, 3, 1); //mPercentBubbleRadiusMod
            ReplicationManager.UpdateFloat(MoveSpeed.TotalSafe, 3, 2); //mMoveSpeed
            ReplicationManager.UpdateFloat(ModelSize.TotalSafe, 3, 3); //mSkinScaleCoef(mistyped as mCrit)
            ReplicationManager.UpdateBool(IsTargetable, 3, 4); //mIsTargetable
            ReplicationManager.UpdateUInt((uint)TargetableToTeam, 3, 5); //mIsTargetableToTeamFlags
        }
    }
}
