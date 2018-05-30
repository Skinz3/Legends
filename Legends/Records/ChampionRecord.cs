using Legends.Core.Inibin;
using Legends.Core.Utils;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("champions", 1)]
    public class ChampionRecord : ITable
    {
        public static List<ChampionRecord> Champions = new List<ChampionRecord>();

        [Primary]
        [InibinField(InibinHashEnum.CHAMPION_championId)]
        public int Id;

        [InibinFieldFileName]
        public string Name;

        [InibinField(InibinHashEnum.CHAMPION_BaseHP)]
        public double BaseHp;

        [InibinField(InibinHashEnum.CHAMPION_BaseMP)]
        public double BaseMp;

        [InibinField(InibinHashEnum.CHAMPION_MoveSpeed)]
        public short BaseMovementSpeed;

        [InibinField(InibinHashEnum.CHAMPION_AbilityPowerIncPerLevel)]
        public short AbilityPowerIncPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_ArmorPerLevel)]
        public double ArmorPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_AttackRange)]
        public short AttackRange;

        [InibinField(InibinHashEnum.CHAMPION_AttackSpeedPerLevel)]
        public double AttackSpeedPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_BaseAbilityPower)]
        public short BaseAbilityPower;

        [InibinField(InibinHashEnum.CHAMPION_BaseCritChance)]
        public double BaseCritChance;

        [InibinField(InibinHashEnum.CHAMPION_BaseDamage)]
        public double BaseDamage;

        [InibinField(InibinHashEnum.CHAMPION_CritPerLevel)]
        public double CritPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_DamagePerLevel)]
        public double DamagePerLevel;

        [InibinField(InibinHashEnum.CHAMPION_HPPerLevel)]
        public double HpPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_HPRegenPerLevel)]
        public double HpRegenPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_IsMelee)]
        public bool IsMelee;

        [InibinField(InibinHashEnum.CHAMPION_MPPerLevel)]
        public double MpPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_MPRegenPerLevel)]
        public double MPRegenPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_Armor)]
        public double BaseArmor;

        /// <summary>
        /// SpellBlock = MagicResist => must check
        /// </summary>
        [InibinField(InibinHashEnum.CHAMPION_SpellBlock)]
        public double BaseMagicResist;

        [InibinField(InibinHashEnum.CHAMPION_BaseStaticHPRegen)]
        public double BaseHpRegen;

        [InibinField(InibinHashEnum.CHAMPION_BaseStaticMPRegen)]
        public double BaseMpRegen;

        [InibinField(InibinHashEnum.CHAMPION_SpellBlockPerLevel)]
        public double MagicResistPerLevel;

        [InibinField(InibinHashEnum.CHAMPION_AttackDelayOffsetPercent)]
        public double AttackDelayOffsetPercent;

        [InibinField(InibinHashEnum.CHAMPION_BaseDodge)]
        public double BaseDodge;

        [InibinField(InibinHashEnum.CHAMPION_AttackSpeed)]
        public double BaseAttackSpeed;

        [InibinField(InibinHashEnum.CHAMPION_SelectionHeight)]
        public double SelectionHeight;

        [InibinField(InibinHashEnum.CHAMPION_SelectionRadius)]
        public double SelectionRadius;

        [Ignore]
        public SkinRecord[] Skins
        {
            get;
            private set;
        }

        public ChampionRecord()
        {

        }
        public ChampionRecord(int id, string name, double baseHp, double baseMp, short baseMovementSpeed,
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
            this.Skins = SkinRecord.GetSkins(Id);
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
                return 1.0f;
            }
        }

        public static ChampionRecord GetChampion(string name)
        {
            return Champions.FirstOrDefault(x => x.Name == name);
        }
    }
}
