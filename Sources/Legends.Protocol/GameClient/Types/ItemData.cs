using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class ItemData
    {
        public uint ItemId
        {
            get;
            set;
        }
        public byte Slot
        {
            get;
            set;
        }
        public byte ItemsInSlot
        {
            get;
            set;
        }
        public byte SpellCharges
        {
            get;
            set;
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(ItemId);
            writer.WriteByte(Slot);
            writer.WriteByte(ItemsInSlot);
            writer.WriteByte(SpellCharges);
        }
        public void Deserialize(LittleEndianReader reader)
        {
            ItemId = reader.ReadUInt();
            Slot = reader.ReadByte();
            ItemsInSlot = reader.ReadByte();
            SpellCharges = reader.ReadByte();
        }
    }


}
