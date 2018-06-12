using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Items
{
    public class Inventory
    {
        private Dictionary<byte, Item> Items
        {
            get;
            set;
        }
        public void AddItem(Item item)
        {
            item.ApplyStats();
            Items.Add(0, item);
        }
        public void RemoveItem(byte slot)
        {
            Items[slot].UnapplyStats();
            Items.Remove(slot);
        }
    }
}
