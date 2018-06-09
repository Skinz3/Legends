using Legends.Core.Protocol.Enum;
using Legends.Records;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class HeroStats : AIStats
    {
        public const float DEFAULT_PERCEPTION_BUBBLE_RADIUS = 1350;

        public HeroStats(AIUnitRecord record, int skinId) : base((float)record.BaseHp, (float)record.BaseMp, (float)record.BaseHpRegen, (float)record.BaseArmor,
            (float)record.BaseDamage, record.BaseAbilityPower, (float)record.BaseDodge, (float)record.BaseCritChance, (float)record.BaseMagicResist,
            (float)record.BaseMpRegen, record.AttackRange, (float)record.BaseAttackSpeed,(float)record.AttackDelayOffsetPercent, AIHero.DEFAULT_COOLDOWN_REDUCTION,
            0, 0, 0, 0, 0, DEFAULT_PERCEPTION_BUBBLE_RADIUS, record.BaseMovementSpeed, record.GetSkinScale(skinId)) // 1 = todo SkinRecord
        {

        }

        public override void UpdateReplication(bool partial = true)
        {

            ReplicationManager.UpdateFloat(Gold, 0, 0); // gold
            ReplicationManager.UpdateFloat(GoldTotal, 0, 1); // gold Total

            ReplicationManager.UpdateUInt(uint.MaxValue, 0, 2); // mReplicatedSpellCanCastBitsLower1
            ReplicationManager.UpdateUInt(uint.MaxValue, 0, 3); // mReplicatedSpellCanCastBitsUpper1
            ReplicationManager.UpdateUInt(uint.MaxValue, 0, 4); // mReplicatedSpellCanCastBitsLower2
            ReplicationManager.UpdateUInt(uint.MaxValue, 0, 5); // mReplicatedSpellCanCastBitsUpper2

            ReplicationManager.UpdateUInt((uint)0, 0, 6); // evolvePoints kha zix?
            ReplicationManager.UpdateUInt((uint)0, 0, 7); // ? spells of evolve flags?

            ReplicationManager.UpdateFloat(20f, 0, 8); // manaCost 0
            ReplicationManager.UpdateFloat(20f, 0, 9); // manaCost 1
            ReplicationManager.UpdateFloat(20f, 0, 10); // manaCost 2
            ReplicationManager.UpdateFloat(20f, 0, 11); // manaCost 3

            ReplicationManager.UpdateInt(0, 0, 12); // manaCost  Ex0
            ReplicationManager.UpdateInt(0, 0, 13); // manaCost  Ex1
            ReplicationManager.UpdateInt(0, 0, 14); // manaCost  Ex2
            ReplicationManager.UpdateInt(0, 0, 15); // manaCost  Ex3
            ReplicationManager.UpdateInt(0, 0, 16); // manaCost  Ex4
            ReplicationManager.UpdateInt(0, 0, 17); // manaCost  Ex5
            ReplicationManager.UpdateInt(0, 0, 18); // manaCost  Ex6
            ReplicationManager.UpdateInt(0, 0, 19); // manaCost  Ex7
            ReplicationManager.UpdateInt(0, 0, 20); // manaCost  Ex8
            ReplicationManager.UpdateInt(0, 0, 21); // manaCost  Ex9
            ReplicationManager.UpdateInt(0, 0, 22); // manaCost  Ex10
            ReplicationManager.UpdateInt(0, 0, 23); // manaCost  Ex11
            ReplicationManager.UpdateInt(0, 0, 24); // manaCost  Ex12
            ReplicationManager.UpdateInt(0, 0, 25); // manaCost  Ex13
            ReplicationManager.UpdateInt(0, 0, 26); // manaCost  Ex14
            ReplicationManager.UpdateInt(0, 0, 27); // manaCost  Ex15

            ReplicationManager.UpdateUInt((uint)ActionState, 1, 0); // actionState

            ReplicationManager.UpdateBool(IsMagicImmune, 1, 1); // magicImmune ? boolean? uint based
            ReplicationManager.UpdateBool(IsInvulnerable, 1, 2); // IsInvulnerable , uint based
            ReplicationManager.UpdateBool(IsPhysicalImmune, 1, 3); // Physical Immune
            ReplicationManager.UpdateBool(IsLifeStealImmune, 1, 4); // lifesteal immune


            ReplicationManager.UpdateFloat(AttackDamage.BaseValue, 1, 5); // baseAttackDamage  non affiché, utilisé pour des items comme sheen
            ReplicationManager.UpdateFloat(AbilityPower.BaseValue, 1, 6); // baseAbilityDamage

            ReplicationManager.UpdateFloat(Dodge.TotalSafe, 1, 7); // Dodge
            ReplicationManager.UpdateFloat(CriticalHit.TotalSafe, 1, 8);  // crit

            ReplicationManager.UpdateFloat(Armor.TotalSafe, 1, 9); // marmor
            ReplicationManager.UpdateFloat(MagicResistance.TotalSafe, 1, 10); // magicResist

            ReplicationManager.UpdateFloat(HpRegeneration.TotalSafe, 1, 11); // hpRegenRate
            ReplicationManager.UpdateFloat(ManaRegeneration.TotalSafe, 1, 12); // mpRegenRate

            ReplicationManager.UpdateFloat(AttackRange.TotalSafe, 1, 13); // mAttackRange
            ReplicationManager.UpdateFloat(AttackDamage.FlatBonus, 1, 14); // flat physical damage mod (damagebonus)

            ReplicationManager.UpdateFloat(AttackDamage.PercentBonus, 1, 15); // mPercentPhysicalDamageMod  (percentage on ad bonus i think, check dat)
            ReplicationManager.UpdateFloat(AbilityPower.FlatBonus, 1, 16); // mFlatMagicReduction


            ReplicationManager.UpdateFloat(MagicResistance.PercentBonus, 1, 18); // mPercentMagicReduction

            ReplicationManager.UpdateFloat(AttackSpeed.Ratio, 1, 19); // mAttackSpeedMod 

            ReplicationManager.UpdateFloat(AttackRange.FlatBonus, 1, 20); //mFlatCastRangeMod

            ReplicationManager.UpdateFloat(-(CooldownReduction.TotalSafe / 100f), 1, 21); // mPercentCooldownMod  -0.5f = 50% cd reduction

            ReplicationManager.UpdateFloat(0f, 1, 22); // mPassiveCooldownEndTime

            ReplicationManager.UpdateFloat(0f, 1, 23); // mPassiveCooldownTotalTime

            ReplicationManager.UpdateFloat(ArmorPenetration.FlatBonus, 1, 24); // mFlatArmorPenetration
            ReplicationManager.UpdateFloat(2f - ArmorPenetration.PercentBonus, 1, 25); // mPercentArmorPenetration 0.6 is percentage
            ReplicationManager.UpdateFloat(MagicPenetration.TotalSafe, 1, 26);// mFlatMagicPenetration
            ReplicationManager.UpdateFloat(2f - MagicPenetration.PercentBonus, 1, 27);  //mPercentMagicPenetration

            ReplicationManager.UpdateFloat(LifeSteal.TotalSafe, 1, 28); // mPercentLifeStealMod
            ReplicationManager.UpdateFloat(LifeSteal.PercentBonus, 1, 29); // mPercentSpellVampMod

            ReplicationManager.UpdateFloat(CCReduction.TotalSafe, 1, 30); // mPercentCCReduction

            ReplicationManager.UpdateFloat(ArmorPenetration.PercentBonus, 2, 0); // mPercentBonusArmorPenetration
            ReplicationManager.UpdateFloat(MagicPenetration.PercentBonus, 2, 1);  // mPercentBonusMagicPenetration
            ReplicationManager.UpdateFloat(HpRegeneration.BaseValue, 2, 2); // mBaseHPRegenRate
            ReplicationManager.UpdateFloat(ManaRegeneration.BaseValue, 2, 3); //  mBasePARRegenRate

            ReplicationManager.UpdateFloat(Health.Current, 3, 0); // current hp
            ReplicationManager.UpdateFloat(Mana.Current, 3, 1); // current mp

            ReplicationManager.UpdateFloat(Health.TotalSafe, 3, 2); // max hp

            ReplicationManager.UpdateFloat(Mana.TotalSafe, 3, 3); // max mp

            ReplicationManager.UpdateFloat(Experience, 3, 4); // currentExp

            ReplicationManager.UpdateFloat(0f, 3, 5); // lifetime ?
            ReplicationManager.UpdateFloat(0f, 3, 6); // mMaxLifetime ?
            ReplicationManager.UpdateFloat(0f, 3, 7); // mLifetimeTicks? 

            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.FlatBonus, 3, 8); // mFlatBubbleRadiusMod
            ReplicationManager.UpdateFloat(PerceptionBubbleRadius.PercentBonus, 3, 9); // mPercentBubbleRadiusMod

            ReplicationManager.UpdateFloat(MoveSpeed.TotalSafe, 3, 10); // move speed, working
            ReplicationManager.UpdateFloat(ModelSize.TotalSafe, 3, 11); // modelSize

            ReplicationManager.UpdateFloat(0f, 3, 12); // mPathfindingRadiusMod
            ReplicationManager.UpdateUInt((uint)Level, 3, 13); // mLevel uint

            ReplicationManager.UpdateUInt((uint)NeutralMinionsKilled, 3, 14); // mNumNeutralMinionsKilled

            ReplicationManager.UpdateBool(IsTargetable, 3, 15); // is targetable?

            ReplicationManager.UpdateUInt((uint)TargetableToTeam, 3, 16); // targetableToTeam Flags
        }

    }


}

