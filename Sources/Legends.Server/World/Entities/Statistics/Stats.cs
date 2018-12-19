using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.World.Entities.AI;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    /// <summary>
    /// Stats are used for all unit types.
    /// </summary>
    public abstract class Stats
    {
        /// <summary>
        /// From LoL
        /// </summary>
        public const float MAX_LIFESTEAL = 20f;

        public const float MAX_SPELLVAMP = 20f;

        /// <summary>
        /// 200% 
        /// </summary>
        public const float ATTACK_CRITICAL_RATIO = 2;

        public const byte DEFAULT_SKILL_POINTS = 1;

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

        public SpellFlags TargetableToTeam
        {
            get;
            set;
        }
        public Stat Armor
        {
            get;
            private set;
        }
        public Stat HealthRegeneration
        {
            get;
            private set;
        }
        public StatActionStateEnum ActionState
        {
            get;
            set;
        }
        public abstract bool IsLifeStealImmune
        {
            get;
        }
        public abstract bool IsCriticalImmune
        {
            get;
        }
        public Stat AttackDamage
        {
            get;
            private set;
        }
        public Stat AbilityPower
        {
            get;
            private set;
        }
        public Stat Dodge
        {
            get;
            private set;
        }
        public Stat CriticalHit
        {
            get;
            private set;
        }
        public Stat MagicResistance
        {
            get;
            private set;
        }
        public Stat ManaRegeneration
        {
            get;
            private set;
        }
        public Stat AttackRange
        {
            get;
            private set;
        }
        public AttackSpeed AttackSpeed
        {
            get;
            private set;
        }
        public Stat CriticalDamageRatio
        {
            get;
            private set;
        }
        public Stat CooldownReduction
        {
            get;
            private set;
        }
        public Stat ArmorPenetration
        {
            get;
            private set;
        }
        public Stat MagicPenetration
        {
            get;
            private set;
        }
        public Stat LifeSteal
        {
            get;
            private set;
        }
        public Stat SpellVamp
        {
            get;
            private set;
        }
        public Stat CCReduction
        {
            get;
            private set;
        }
        public float Experience
        {
            get;
            protected set;
        }
        public Stat PerceptionBubbleRadius
        {
            get;
            private set;
        }
        public Stat MoveSpeed
        {
            get;
            private set;
        }
        public Stat ModelSize
        {
            get;
            private set;
        }
        public int Level
        {
            get
            {
                return ExperienceRecord.GetLevel(Experience); // weird a bit, threadsafe?
            }
        }
        public float Gold
        {
            get;
            protected set;
        }
        public float GoldTotal
        {
            get;
            protected set;
        }
        public int NeutralMinionsKilled
        {
            get;
            private set;
        }
        public byte SkillPoints
        {
            get;
            set;
        }
        public Stats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor, float baseAttackDamage,
            float baseAbilityPower, float baseDodge, float baseCriticalHit, float baseMagicResistance, float baseManaRegeneration,
            float baseAttackRange, float baseAttackSpeed, float attackDelayPercent, float baseCooldownReduction, float baseArmorPenetration,
            float baseMagicPenetration, float baseLifeSteal, float baseSpellVamp, float baseCCReduction, float basePerceptionBubbleRadius,
            float baseMoveSpeed, float baseModelSize)
        {
            this.Health = new Health(baseHeath);
            this.Mana = new Health(baseMana);
            this.IsInvulnerable = false;
            this.IsPhysicalImmune = false;
            this.IsMagicImmune = false;
            this.TargetableToTeam = SpellFlags.TargetableToAll;
            this.Armor = new Stat(baseArmor);
            this.HealthRegeneration = new Stat(baseHpRegen);
            this.IsTargetable = true;
            this.ActionState = StatActionStateEnum.CanAttack | StatActionStateEnum.CanCast | StatActionStateEnum.CanMove | StatActionStateEnum.Unknown;
            this.AttackDamage = new Stat(baseAttackDamage);
            this.AbilityPower = new Stat(baseAbilityPower);
            this.Dodge = new Stat(baseDodge);
            this.CriticalHit = new Stat(baseCriticalHit);
            this.MagicResistance = new Stat(baseMagicResistance);
            this.ManaRegeneration = new Stat(baseManaRegeneration);
            this.AttackRange = new Stat(baseAttackRange);
            this.AttackSpeed = new AttackSpeed(attackDelayPercent); // attakdelay , what is this?
            this.CooldownReduction = new Stat(baseCooldownReduction);
            this.CriticalDamageRatio = new Stat(ATTACK_CRITICAL_RATIO);
            this.ArmorPenetration = new Stat(baseArmorPenetration);
            this.MagicPenetration = new Stat(baseMagicPenetration);
            this.LifeSteal = new Stat(baseLifeSteal, 0, MAX_LIFESTEAL);
            this.SpellVamp = new Stat(baseSpellVamp, 0, MAX_SPELLVAMP);
            this.CCReduction = new Stat(baseCCReduction);
            this.PerceptionBubbleRadius = new Stat(basePerceptionBubbleRadius);
            this.MoveSpeed = new Stat(baseMoveSpeed);
            this.ModelSize = new Stat(baseModelSize);
            this.Gold = AIHero.DEFAULT_START_GOLD;
            this.GoldTotal = this.Gold;
            this.SkillPoints = DEFAULT_SKILL_POINTS;
        }

        public bool CriticalStrike()
        {
            return Extensions.RandomAssertion(CriticalHit.TotalSafe);
        }
        public void AddGold(float value)
        {
            Gold += value;
            GoldTotal += value;
        }
        public void RemoveGold(float value)
        {
            Gold -= value;
        }
        public void AddExperience(float value)
        {
            Experience += value;
        }
    }
}
