using Legends.Core.Protocol.Enum;
using Legends.Records;
using Legends.World.Entities.AI;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public abstract class AIStats : ReplicationStats
    {
        public StatActionStateEnum ActionState
        {
            get;
            set;
        }
        public bool IsLifeStealImmune
        {
            get;
            private set;
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
            private set;
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
            private set;
        }
        public float GoldTotal
        {
            get;
            private set;
        }
        public int NeutralMinionsKilled
        {
            get;
            private set;
        }
        public AIStats(float baseHeath, float baseMana, float baseHpRegen, float baseArmor, float baseAttackDamage,
            float baseAbilityPower, float baseDodge, float baseCriticalHit, float baseMagicResistance, float baseManaRegeneration,
            float baseAttackRange, float baseAttackSpeed, float baseCooldownReduction, float baseArmorPenetration,
            float baseMagicPenetration, float baseLifeSteal, float baseSpellVamp, float baseCCReduction, float basePerceptionBubbleRadius,
            float baseMoveSpeed, float baseModelSize) : base(baseHeath, baseMana, baseHpRegen, baseArmor)
        {
            this.ActionState = StatActionStateEnum.CanAttack | StatActionStateEnum.CanCast | StatActionStateEnum.CanMove | StatActionStateEnum.Unknown;
            this.IsLifeStealImmune = false;
            this.AttackDamage = new Stat(baseAttackDamage);
            this.AbilityPower = new Stat(baseAbilityPower);
            this.Dodge = new Stat(baseDodge);
            this.CriticalHit = new Stat(baseCriticalHit);
            this.MagicResistance = new Stat(baseMagicResistance);
            this.ManaRegeneration = new Stat(baseManaRegeneration);
            this.AttackRange = new Stat(baseAttackRange);
            this.AttackSpeed = new AttackSpeed(0f); // attakdelay , what is this?
            this.CooldownReduction = new Stat(baseCooldownReduction);
            this.ArmorPenetration = new Stat(baseArmorPenetration);
            this.MagicPenetration = new Stat(baseMagicPenetration);
            this.LifeSteal = new Stat(baseLifeSteal);
            this.SpellVamp = new Stat(baseSpellVamp);
            this.CCReduction = new Stat(baseCCReduction);
            this.PerceptionBubbleRadius = new Stat(basePerceptionBubbleRadius);
            this.MoveSpeed = new Stat(baseMoveSpeed);
            this.ModelSize = new Stat(baseModelSize);
            this.Gold = AIHero.DEFAULT_START_GOLD;
            this.GoldTotal = this.Gold;
        }
        public void AddGold(float value)
        {
            Gold += value;
            GoldTotal += value;
        }
        public void AddExperience(float value)
        {
            Experience += value;
        }
    }
}
