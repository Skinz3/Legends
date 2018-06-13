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
            var recipeItems = Items.Where(x => x.Value.Id == record.RecipeItem1 || x.Value.Id == record.RecipeItem2 ||
            x.Value.Id == record.RecipeItem3 || x.Value.Id == record.RecipeItem4).ToArray();


            Dictionary<byte, Item> temp = new Dictionary<byte, Item>();

            foreach (var pair in recipeItems)
            {
                if (temp.Values.FirstOrDefault(x => pair.Value.Id == x.Id) == null)
                {
                    temp.Add(pair.Key, pair.Value);
                }
            }

            foreach (var recipeItem in temp)
            {
                RemoveItem(recipeItem.Key);
            }
            return temp.Values.ToArray();
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
