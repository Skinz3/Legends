using Legends.Core.Attributes;
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
    [Table("aiunits")]
    public class AIUnitRecord : ITable
    {
        public const string BASIC_ATTACK_SPELL_SUFFIX = "BasicAttack";

        private static List<AIUnitRecord> AIUnits = new List<AIUnitRecord>();

        [InibinField(InibinHashEnum.CHARACTER_championId)]
        public int Id
        {
            get;
            set;
        }
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
        [InibinField(InibinHashEnum.CHARACTER_PathfindingCollisionRadius)]
        public double PathfindingCollisionRadius
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
        public float AbilityPowerIncPerLevel
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
        [InibinField(InibinHashEnum.CHARACTER_PerceptionBubbleRadius)]
        public double PerceptionBubbleRadius
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
        [InibinField(InibinHashEnum.CHARACTER_AcquisitionRange)]
        public double AquisitionRange
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ChasingAttackRangePercent)]
        public double ChasingAttackRangePercent
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
        [InibinField(InibinHashEnum.CHARACTER_AttackDelayCastOffsetPercent)]
        public double AttackDelayCastOffsetPercent
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
        [InibinField(InibinHashEnum.CHARACTER_SPELL1)]
        public string Spell1
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SPELL2)]
        public string Spell2
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SPELL3)]
        public string Spell3
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SPELL4)]
        public string Spell4
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell1)]
        public string ExtraSpell1
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell2)]
        public string ExtraSpell2
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell3)]
        public string ExtraSpell3
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell4)]
        public string ExtraSpell4
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell5)]
        public string ExtraSpell5
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell6)]
        public string ExtraSpell6
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell7)]
        public string ExtraSpell7
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell8)]
        public string ExtraSpell8
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell9)]
        public string ExtraSpell9
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraSpell10)]
        public string ExtraSpell10
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraAttack1)]
        public string ExtraAttack1
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraAttack2)]
        public string ExtraAttack2
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraAttack3)]
        public string ExtraAttack3
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_ExtraAttack4)]
        public string ExtraAttack4
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_GlobalGoldGivenOnDeath)]
        public float GlobalGoldGivenOnDeath
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SpellSupLevels1)]
        public string SpellSupLevels1
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SpellSupLevels2)]
        public string SpellSupLevels2
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SpellSupLevels3)]
        public string SpellSupLevels3
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_SpellSupLevels4)]
        public string SpellSupLevels4
        {
            get;
            set;
        }
        [InibinField(InibinHashEnum.CHARACTER_LocalGoldGivenOnDeath)]
        public float LocalGoldGivenOnDeath
        {
            get;
            set;
        }

        [Ignore]
        public SpellRecord BasicAttack
        {
            get;
            set;
        }
        [Ignore]
        public SkinRecord[] Skins
        {
            get;
            private set;
        }

        public AIUnitRecord()
        {

        }
        public static AIUnitRecord[] GetAIUnits()
        {
            return AIUnits.ToArray();
        }
        [StartupInvoke("AIUnits Hooks", StartupInvokePriority.Ninth)]
        public static void Initialize()
        {
            foreach (var record in AIUnitRecord.AIUnits)
            {
                record.Skins = SkinRecord.GetSkins(record.Id);

                record.BasicAttack = SpellRecord.GetSpellCaseInsensitive(record.Name + BASIC_ATTACK_SPELL_SUFFIX);

            }
        }

        public float GetSkinScale(int skinId)
        {
            return Unit.DEFAULT_MODEL_SIZE;
        }

        public static AIUnitRecord GetAIUnitRecord(string name)
        {
            return AIUnits.FirstOrDefault(x => x.Name == name);
        }
    }
}
