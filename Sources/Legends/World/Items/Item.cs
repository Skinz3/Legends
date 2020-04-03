using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Items
{
    public class Item : IProtocolable<ItemData>
    {
        public ItemRecord Record
        {
            get;
            private set;
        }
        public AttackableUnit Owner
        {
            get;
            private set;
        }
        public uint Id
        {
            get
            {
                return Record.ItemId;
            }
        }
        public byte Stacks
        {
            get;
            set;
        }
        public byte Slot
        {
            get;
            set;
        }
        public Item(ItemRecord record, AttackableUnit owner, byte slot)
        {
            this.Record = record;
            this.Owner = owner;
            this.Slot = slot;
            this.Stacks = 1;
        }
        public ItemData GetProtocolObject()
        {
            return new ItemData()
            {
                ItemId = Id,
                ItemsInSlot = Slot,
                Slot = Slot,
                SpellCharges = 0,
            };
        }
        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "Mana != Energy ?")]
        public void ApplyStats()
        {
            this.Owner.Stats.MoveSpeed.FlatBonus += Record.FlatMovementSpeedMod;
            this.Owner.Stats.AttackDamage.FlatBonus += Record.FlatPhysicalDamageMod;
            this.Owner.Stats.Armor.FlatBonus += Record.FlatArmorMod;
            this.Owner.Stats.AttackSpeed.FlatBonus += Record.FlatAttackSpeedMod;
            this.Owner.Stats.CriticalHit.FlatBonus += Record.FlatCritChanceMod;
            this.Owner.Stats.Dodge.FlatBonus += Record.FlatDodgeMod;
            this.Owner.Stats.Health.FlatBonus += Record.FlatHpPoolMod;
            this.Owner.Stats.HealthRegeneration.FlatBonus += Record.FlatHpRegenMod;
            this.Owner.Stats.Mana.FlatBonus += Record.FlatMpPoolMod;
            this.Owner.Stats.ManaRegeneration.FlatBonus += Record.FlatMpRegenMod;
            this.Owner.Stats.AbilityPower.FlatBonus += Record.FlatAbilityPowerMod;
            this.Owner.Stats.CooldownReduction.FlatBonus += Record.CooldownReductionPercentMod;
            this.Owner.Stats.Armor.PercentBonus += Record.PercentArmorMod;
            this.Owner.Stats.AttackSpeed.PercentBonus += Record.PercentAttackSpeedMod;
            this.Owner.Stats.CriticalHit.PercentBonus += Record.PercentCritChanceMod;
            this.Owner.Stats.CriticalDamageRatio.PercentBonus += Record.PercentCritDamageMod;
            this.Owner.Stats.Health.PercentBonus += Record.PercentHpPoolMod;
            this.Owner.Stats.HealthRegeneration.PercentBonus += Record.PercentHpRegenMod;
            this.Owner.Stats.LifeSteal.FlatBonus += Record.PercentLifeStealMod;
            this.Owner.Stats.MagicResistance.FlatBonus += Record.FlatMagicResistMod;
            this.Owner.Stats.ManaRegeneration.PercentBonus += Record.PercentMpRegenMod;
            this.Owner.Stats.AbilityPower.PercentBonus += Record.PercentAbilityPowerMod;
            this.Owner.Stats.MagicPenetration.PercentBonus += Record.PercentMagicPenetrationMod;
            this.Owner.Stats.MoveSpeed.PercentBaseBonus += Record.PercentMovementSpeedMod;
            this.Owner.Stats.AttackDamage.PercentBonus += Record.PercentPhysicalDamageMod;
            this.Owner.Stats.MagicResistance.PercentBonus += Record.PercentMagicResistMod;
            this.Owner.Stats.SpellVamp.FlatBonus += Record.PercentSpellVampMod;
            this.Owner.Stats.MagicPenetration.PercentBonus += Record.PercentMagicPenetrationMod;

        }
        public void UnapplyStats()
        {
            this.Owner.Stats.MoveSpeed.FlatBonus -= Record.FlatMovementSpeedMod;
            this.Owner.Stats.AttackDamage.FlatBonus -= Record.FlatPhysicalDamageMod;
            this.Owner.Stats.Armor.FlatBonus -= Record.FlatArmorMod;
            this.Owner.Stats.AttackSpeed.FlatBonus -= Record.FlatAttackSpeedMod;
            this.Owner.Stats.CriticalHit.FlatBonus -= Record.FlatCritChanceMod;
            this.Owner.Stats.Dodge.FlatBonus -= Record.FlatDodgeMod;
            this.Owner.Stats.Health.FlatBonus -= Record.FlatHpPoolMod;
            this.Owner.Stats.HealthRegeneration.FlatBonus -= Record.FlatHpRegenMod;
            this.Owner.Stats.Mana.FlatBonus -= Record.FlatMpPoolMod;
            this.Owner.Stats.ManaRegeneration.FlatBonus -= Record.FlatMpRegenMod;
            this.Owner.Stats.AbilityPower.FlatBonus -= Record.FlatAbilityPowerMod;
            this.Owner.Stats.Armor.PercentBonus -= Record.PercentArmorMod;
            this.Owner.Stats.AttackSpeed.PercentBonus -= Record.PercentAttackSpeedMod;
            this.Owner.Stats.CriticalHit.PercentBonus -= Record.PercentCritChanceMod;
            this.Owner.Stats.CriticalDamageRatio.PercentBonus -= Record.PercentCritDamageMod;
            this.Owner.Stats.Health.PercentBonus -= Record.PercentHpPoolMod;
            this.Owner.Stats.HealthRegeneration.PercentBonus -= Record.PercentHpRegenMod;
            this.Owner.Stats.LifeSteal.FlatBonus -= Record.PercentLifeStealMod;
            this.Owner.Stats.MagicResistance.FlatBonus -= Record.FlatMagicResistMod;
            this.Owner.Stats.ManaRegeneration.PercentBonus -= Record.PercentMpRegenMod;
            this.Owner.Stats.AbilityPower.PercentBonus -= Record.PercentAbilityPowerMod;
            this.Owner.Stats.MagicPenetration.PercentBonus -= Record.PercentMagicPenetrationMod;
            this.Owner.Stats.MoveSpeed.PercentBaseBonus -= Record.PercentMovementSpeedMod;

            this.Owner.Stats.AttackDamage.PercentBonus -= Record.PercentPhysicalDamageMod;
            this.Owner.Stats.MagicResistance.PercentBonus -= Record.PercentMagicResistMod;
            this.Owner.Stats.SpellVamp.FlatBonus -= Record.PercentSpellVampMod;
            this.Owner.Stats.MagicPenetration.PercentBonus -= Record.PercentMagicPenetrationMod;
        }
    }
}
