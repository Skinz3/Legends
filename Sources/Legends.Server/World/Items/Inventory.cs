using Legends.Handlers;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World.Entities;
using Legends.World.Entities.AI;
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
                    return i;
                }
            }

            return 7;
        }
        /// <summary>
        /// A recursive one ;)
        /// </summary>
        public int[] RemoveRecipeItem(ItemRecord record)
        {
            List<int> results = new List<int>();

            foreach (var itemRecipe in record.RecipeItemRecords)
            {
                var removed = RemoveItem(itemRecipe.ItemId);

                if (removed != null)
                {
                    results.Add(removed.Record.GetTotalPrice());
                }
                else
                {
                    results.AddRange(RemoveRecipeItem(itemRecipe));
                }

            }

            return results.ToArray();
        }
        public Item AddItem(int itemId)
        {
            return AddItem(ItemRecord.GetItemRecord(itemId));
        }
        public Item AddExtraItem(int itemId, byte slot)
        {
            var item = new Item(ItemRecord.GetItemRecord(itemId), Owner, slot);
            Items.Add(slot, item);
            item.ApplyStats();
            Owner.OnItemAdded(item);
            return item;
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

        public void SwapItems(byte slotFrom, byte slotTo)
        {
            if (Items.ContainsKey(slotFrom) == false)
            {
                return;
            }
            if (Items.ContainsKey(slotTo))
            {
                var temp = Items[slotFrom];
                Items[slotFrom].Slot = slotTo;
                Items[slotFrom] = Items[slotTo];


                Items[slotTo].Slot = slotFrom;
                Items[slotTo] = temp;

            }
            else
            {

                Items.Add(slotTo, Items[slotFrom]);
                Items[slotTo].Slot = slotTo;
                Items.Remove(slotFrom);
            }

        }
        public Item[] GetItems()
        {
            return Items.Values.ToArray();
        }
        public ItemData[] GetItemDatas()
        {
            return Array.ConvertAll(GetItems(), x => x.GetProtocolObject());
        }
        public Item FindItem(Func<Item, bool> func)
        {
            for (byte i = 0; i < 6; i++)
            {
                if (Items.ContainsKey(i) && func(Items[i]))
                {
                    return Items[i];
                }
            }
            return null;
        }
        public Item RemoveItem(uint itemId)
        {
            var item = FindItem(x => x.Id == itemId);

            if (item != null)
            {
                return RemoveItem(item.Slot);
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
