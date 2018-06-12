using Legends.Records;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Items
{
    public class Item
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
        public Item(ItemRecord record, AttackableUnit owner)
        {
            this.Record = record;
            this.Owner = owner;
        }
        public void ApplyStats()
        {

        }
        public void UnapplyStats()
        {

        }
    }
}
