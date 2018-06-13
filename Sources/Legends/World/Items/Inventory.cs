using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends.World.Items
{
    public class Inventory
    {
        public const int MAX_INVENTORY_SIZE = 6;

        public const int RELIC_BASE_SLOT = 6;

        private Dictionary<byte, Item> Items
        {
            get;
            set;
        }
        private AttackableUnit Owner
        {
            get;
            set;
        }
        public Inventory(AttackableUnit owner)
        {
            this.Owner = owner;
            this.Items = new Dictionary<byte, Item>();
        }
        private byte GetNextSlot()
        {
            for (byte i = 0; i < 6; i++)
            {
                if (Items.FirstOrDefault(x => x.Key == i).Value == null)
                {
                    return (byte)(i);
                }
            }

            return 7;
        }
        public Item[] RemoveRecipeItem(ItemRecord record)
        {
            List<Item> results = new List<Item>();

            foreach (var itemRecipe in record.RecipeItemRecords)
            {
                var removed = RemoveItem(itemRecipe.ItemId);
                if (removed != null)
                    results.Add(removed);
            }

            return results.ToArray();
        }
        public Item AddItem(int itemId)
        {
            return AddItem(ItemRecord.GetItemRecord(itemId));
        }
        public Item AddItem(ItemRecord record)
        {

            byte slot = GetNextSlot();
            bool isRelicBase = record.Group == ItemGroupEnum.RelicBase;  // Trinket

            if (isRelicBase)
            {
                RemoveItem(RELIC_BASE_SLOT);
                slot = RELIC_BASE_SLOT;
            }

            if (slot < MAX_INVENTORY_SIZE || (slot == RELIC_BASE_SLOT && isRelicBase))
            {
                var item = new Item(record, Owner, slot);
                Items.Add(slot, item);
                item.ApplyStats();
                Owner.OnItemAdded(item);
                return item;
            }
            else
            {
                return null;
            }

        }

        public Item[] GetItems()
        {
            return Items.Values.ToArray();
        }

        public Item RemoveItem(int itemId)
        {
            var pair = Items.FirstOrDefault(x => x.Value.Id == itemId);

            if (pair.Value != null)
            {
                return RemoveItem(pair.Key);
            }
            return null;
        }
        public Item RemoveItem(byte slot)
        {
            if (Items.ContainsKey(slot))
            {
                var item = Items[slot];
                item.UnapplyStats();
                Items.Remove(slot);
                Owner.OnItemRemoved(item);
                return item;
            }
            else
            {
                return null;
            }
        }
    }
}
