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
                if (Items.ElementAtOrDefault(i).Value == null)
                {
                    return (byte)(i);
                }
            }

            return 7;
        }
        public Item AddItem(ItemRecord record)
        {
            if (Items.Count < MAX_INVENTORY_SIZE)
            {
                RemoveRecipeItems(record);

                byte slot = GetNextSlot();

                if (slot <= MAX_INVENTORY_SIZE)
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
            else
            {
                return null;
            }
        }

        public Item[] GetItems()
        {
            return Items.Values.ToArray();
        }

        private void RemoveRecipeItems(ItemRecord record)
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
            recipeItems = temp.ToArray();

            foreach (var recipeItem in recipeItems)
            {
                RemoveItem(recipeItem.Key);
            }


        }
        public Item RemoveItem(byte slot)
        {
            var item = Items[slot];
            item.UnapplyStats();
            Items.Remove(slot);
            Owner.OnItemRemoved(item);
            return item;
        }
    }
}
