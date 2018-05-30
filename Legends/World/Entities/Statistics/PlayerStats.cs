using Legends.Core.Protocol.Enum;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class PlayerStats : AIStats
    {
        public float Gold
        {
            get;
            private set;
        }
        public float GoldTotal
        {
            get;
            private set;
        }

        public PlayerStats(ChampionRecord record, int skinId) : base((float)record.BaseHp, (float)record.BaseMp, (float)record.BaseHpRegen, (float)record.BaseArmor,
            (float)record.BaseDamage, record.BaseAbilityPower, (float)record.BaseDodge, (float)record.BaseCritChance, (float)record.BaseMagicResist,
            (float)record.BaseMpRegen, record.AttackRange, (float)record.BaseAttackSpeed, Player.DEFAULT_COOLDOWN_REDUCTION,
            0, 0, 0, 0, 0, Player.DEFAULT_PERCEPTION_BUBBLE_RADIUS, record.BaseMovementSpeed, record.GetSkinScale(skinId)) // 1 = todo SkinRecord
        {
            this.Gold = Player.DEFAULT_START_GOLD;
            this.GoldTotal = this.Gold;
        }

        public override void UpdateReplication(bool partial = true)
        {

            ReplicationManager.Update(Gold, 0, 0); // gold
            ReplicationManager.Update(GoldTotal, 0, 1); // gold Total

            ReplicationManager.Update(uint.MaxValue, 0, 2); // mReplicatedSpellCanCastBitsLower1
            ReplicationManager.Update(uint.MaxValue, 0, 3); // mReplicatedSpellCanCastBitsUpper1
            ReplicationManager.Update(uint.MaxValue, 0, 4); // mReplicatedSpellCanCastBitsLower2
            ReplicationManager.Update(uint.MaxValue, 0, 5); // mReplicatedSpellCanCastBitsUpper2

            ReplicationManager.Update((uint)0, 0, 6); // evolvePoints kha zix?
            ReplicationManager.Update((uint)0, 0, 7); // ? spells of evolve flags?

            ReplicationManager.Update(20f, 0, 8); // manaCost 0
            ReplicationManager.Update(20f, 0, 9); // manaCost 1
            ReplicationManager.Update(20f, 0, 10); // manaCost 2
            ReplicationManager.Update(20f, 0, 11); // manaCost 3

            ReplicationManager.Update(0, 0, 12); // manaCost  Ex0
            ReplicationManager.Update(0, 0, 13); // manaCost  Ex1
            ReplicationManager.Update(0, 0, 14); // manaCost  Ex2
            ReplicationManager.Update(0, 0, 15); // manaCost  Ex3
            ReplicationManager.Update(0, 0, 16); // manaCost  Ex4
            ReplicationManager.Update(0, 0, 17); // manaCost  Ex5
            ReplicationManager.Update(0, 0, 18); // manaCost  Ex6
            ReplicationManager.Update(0, 0, 19); // manaCost  Ex7
            ReplicationManager.Update(0, 0, 20); // manaCost  Ex8
            ReplicationManager.Update(0, 0, 21); // manaCost  Ex9
            ReplicationManager.Update(0, 0, 22); // manaCost  Ex10
            ReplicationManager.Update(0, 0, 23); // manaCost  Ex11
            ReplicationManager.Update(0, 0, 24); // manaCost  Ex12
            ReplicationManager.Update(0, 0, 25); // manaCost  Ex13
            ReplicationManager.Update(0, 0, 26); // manaCost  Ex14
            ReplicationManager.Update(0, 0, 27); // manaCost  Ex15

            ReplicationManager.Update((uint)ActionState, 1, 0); // actionState

            ReplicationManager.Update(IsMagicImmune, 1, 1); // magicImmune ? boolean? uint based
            ReplicationManager.Update(IsInvulnerable, 1, 2); // IsInvulnerable , uint based
            ReplicationManager.Update(IsPhysicalImmune, 1, 3); // Physical Immune
            ReplicationManager.Update(IsLifeStealImmune, 1, 4); // lifesteal immune


            ReplicationManager.Update(AttackDamage.BaseValue, 1, 5); // baseAttackDamage  non affiché, utilisé pour des items comme sheen
            ReplicationManager.Update(AbilityPower.BaseValue, 1, 6); // baseAbilityDamage

            ReplicationManager.Update(Dodge.Total, 1, 7); // Dodge
            ReplicationManager.Update(CriticalHit.Total, 1, 8);  // crit

            ReplicationManager.Update(Armor.Total, 1, 9); // marmor
            ReplicationManager.Update(MagicResistance.Total, 1, 10); // magicResist

            ReplicationManager.Update(HpRegeneration.Total, 1, 11); // hpRegenRate
            ReplicationManager.Update(ManaRegeneration.Total, 1, 12); // mpRegenRate

            ReplicationManager.Update(AttackRange.Total, 1, 13); // mAttackRange
            ReplicationManager.Update(AttackDamage.FlatBonus, 1, 14); // flat physical damage mod (damagebonus)

            ReplicationManager.Update(AttackDamage.PercentBonus, 1, 15); // mPercentPhysicalDamageMod  (percentage on ad bonus i think, check dat)
            ReplicationManager.Update(AbilityPower.FlatBonus, 1, 16); // mFlatMagicReduction


            ReplicationManager.Update(MagicResistance.PercentBonus, 1, 18); // mPercentMagicReduction

            ReplicationManager.Update(AttackSpeed.BaseBonus, 1, 19); // mAttackSpeedMod 
            ReplicationManager.Update(AttackRange.FlatBonus, 1, 20); //mFlatCastRangeMod

            ReplicationManager.Update(-(CooldownReduction.Total / 100f), 1, 21); // mPercentCooldownMod  -0.5f = 50% cd reduction

            ReplicationManager.Update(0f, 1, 22); // mPassiveCooldownEndTime

            ReplicationManager.Update(0f, 1, 23); // mPassiveCooldownTotalTime

            ReplicationManager.Update(ArmorPenetration.FlatBonus, 1, 24); // mFlatArmorPenetration
            ReplicationManager.Update(2f - ArmorPenetration.PercentBonus, 1, 25); // mPercentArmorPenetration 0.6 is percentage
            ReplicationManager.Update(MagicPenetration.Total, 1, 26);// mFlatMagicPenetration
            ReplicationManager.Update(2f - MagicPenetration.PercentBonus, 1, 27);  //mPercentMagicPenetration

            ReplicationManager.Update(LifeSteal.Total, 1, 28); // mPercentLifeStealMod
            ReplicationManager.Update(LifeSteal.PercentBonus, 1, 29); // mPercentSpellVampMod

            ReplicationManager.Update(CCReduction.Total, 1, 30); // mPercentCCReduction

            ReplicationManager.Update(ArmorPenetration.PercentBonus, 2, 0); // mPercentBonusArmorPenetration
            ReplicationManager.Update(MagicPenetration.PercentBonus, 2, 1);  // mPercentBonusMagicPenetration
            ReplicationManager.Update(HpRegeneration.BaseValue, 2, 2); // mBaseHPRegenRate
            ReplicationManager.Update(ManaRegeneration.BaseValue, 2, 3); //  mBasePARRegenRate

            ReplicationManager.Update(Health.Current, 3, 0); // current hp
            ReplicationManager.Update(Mana.Current, 3, 1); // current mp

            ReplicationManager.Update(Health.Total, 3, 2); // max hp

            ReplicationManager.Update(Mana.Total, 3, 3); // max mp

            ReplicationManager.Update(Experience, 3, 4); // currentExp

            ReplicationManager.Update(0f, 3, 5); // lifetime ?
            ReplicationManager.Update(0f, 3, 6); // mMaxLifetime ?
            ReplicationManager.Update(0f, 3, 7); // mLifetimeTicks? 

            ReplicationManager.Update(PerceptionBubbleRadius.FlatBonus, 3, 8); // mFlatBubbleRadiusMod
            ReplicationManager.Update(PerceptionBubbleRadius.PercentBonus, 3, 9); // mPercentBubbleRadiusMod

            ReplicationManager.Update(MoveSpeed.Total, 3, 10); // move speed, working
            ReplicationManager.Update(ModelSize.Total, 3, 11); // modelSize

            ReplicationManager.Update(0f, 3, 12); // mPathfindingRadiusMod
            ReplicationManager.Update((uint)Level, 3, 13); // mLevel uint

            ReplicationManager.Update((uint)NeutralMinionsKilled, 3, 14); // mNumNeutralMinionsKilled

            ReplicationManager.Update(IsTargetable, 3, 15); // is targetable?

            ReplicationManager.Update((uint)TargetableToTeam, 3, 16); // targetableToTeam Flags
        }
        public void AddGold(float value)
        {
            Gold += value;
            GoldTotal += value;
        }
    }


}

