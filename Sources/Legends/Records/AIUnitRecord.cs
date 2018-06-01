using Legends.Core.DesignPattern;
using Legends.Core.IO.Inibin;
using Legends.Core.Utils;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.World.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("/Database/AIUnit/")]
    public class AIUnitRecord : ITable
    {
        [JsonCache]
        private static List<AIUnitRecord> AIUnits = new List<AIUnitRecord>();

        [InibinField(InibinHashEnum.CHARACTER_championId)]
        public int Id
        {
            get;
            set;
        }

        [JsonFileName]
        [InibinFieldFileName]
        public string Name
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseHP)]
        public double BaseHp
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseMP)]
        public double BaseMp
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_MoveSpeed)]
        public short BaseMovementSpeed
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_AbilityPowerIncPerLevel)]
        public short AbilityPowerIncPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_ArmorPerLevel)]
        public double ArmorPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_AttackRange)]
        public short AttackRange
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_AttackSpeedPerLevel)]
        public double AttackSpeedPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseAbilityPower)]
        public short BaseAbilityPower
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseCritChance)]
        public double BaseCritChance
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseDamage)]
        public double BaseDamage
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_CritPerLevel)]
        public double CritPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_DamagePerLevel)]
        public double DamagePerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_HPPerLevel)]
        public double HpPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_HPRegenPerLevel)]
        public double HpRegenPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_IsMelee)]
        public bool IsMelee
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_MPPerLevel)]
        public double MpPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_MPRegenPerLevel)]
        public double MPRegenPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_Armor)]
        public double BaseArmor
        {
            get;
            set;
        }

        /// <summary>
        /// SpellBlock = MagicResist => must check
        /// </summary>
        [InibinField(InibinHashEnum.CHARACTER_SpellBlock)]
        public double BaseMagicResist
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseStaticHPRegen)]
        public double BaseHpRegen
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseStaticMPRegen)]
        public double BaseMpRegen
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_SpellBlockPerLevel)]
        public double MagicResistPerLevel
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_AttackDelayOffsetPercent)]
        public double AttackDelayOffsetPercent
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_BaseDodge)]
        public double BaseDodge
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_AttackSpeed)]
        public double BaseAttackSpeed
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_SelectionHeight)]
        public double SelectionHeight
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.CHARACTER_SelectionRadius)]
        public double SelectionRadius
        {
            get;
            set;
        }

        [JsonIgnore]
        public SkinRecord[] Skins
        {
            get;
            private set;
        }

        public AIUnitRecord()
        {

        }
        public AIUnitRecord(int id, string name, double baseHp, double baseMp, short baseMovementSpeed,
            short abilityPowerPerLevel, double armorPerLevel, short attackRange, double attackSpeedPerLevel,
            short baseAbilityPower, double baseCriticalChance, double baseDamage, double critPerLevel,
            double damagePerLevel, double hpPerLevel, double hpRegenPerLevel, bool isMelee, double mpPerLevel,
            double mpRegenPerLevel, double baseArmor, double baseMagicResist, double baseHpRegen, double baseMpRegen,
            double magicResistPerLevel, double attackDelayOffsetPercent, double baseDodge, double baseAttackSpeed,
            double selectionHeight, double selectionRadius)
        {
            this.Id = id;
            this.Name = name;
            this.BaseHp = baseHp;
            this.BaseMp = baseMp;
            this.BaseMovementSpeed = baseMovementSpeed;
            this.AbilityPowerIncPerLevel = abilityPowerPerLevel;
            this.ArmorPerLevel = armorPerLevel;
            this.AttackRange = attackRange;
            this.AttackSpeedPerLevel = attackSpeedPerLevel;
            this.BaseAbilityPower = baseAbilityPower;
            this.BaseCritChance = baseCriticalChance;
            this.BaseDamage = baseDamage;
            this.CritPerLevel = critPerLevel;
            this.DamagePerLevel = damagePerLevel;
            this.HpPerLevel = hpPerLevel;
            this.HpRegenPerLevel = hpRegenPerLevel;
            this.IsMelee = isMelee;
            this.MpPerLevel = mpPerLevel;
            this.MPRegenPerLevel = mpRegenPerLevel;
            this.BaseMagicResist = baseMagicResist;
            this.BaseArmor = baseArmor;
            this.BaseMagicResist = baseMagicResist;
            this.BaseHpRegen = baseHpRegen;
            this.MagicResistPerLevel = magicResistPerLevel;
            this.AttackDelayOffsetPercent = attackDelayOffsetPercent;
            this.BaseMpRegen = baseMpRegen;
            this.BaseDodge = baseDodge;
            this.BaseAttackSpeed = baseAttackSpeed;
            this.SelectionHeight = selectionHeight;
            this.SelectionRadius = selectionRadius;
        }

        [StartupInvoke("Champion Hooks", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var record in AIUnitRecord.AIUnits)
            {
                record.Skins = SkinRecord.GetSkins(record.Id);
            }
        }

        public float GetSkinScale(int skinId)
        {
            var skin = Skins.FirstOrDefault(x => x.SkinId == skinId);

            if (skin != null)
            {
                return skin.Scale;
            }
            else
            {
                return Unit.DEFAULT_MODEL_SIZE;
            }
        }

        public static AIUnitRecord GetAIUnitRecord(string name)
        {
            return AIUnits.FirstOrDefault(x => x.Name == name);
        }
    }
}
