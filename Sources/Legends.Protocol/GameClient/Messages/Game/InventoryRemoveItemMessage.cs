using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class InventoryRemoveItemMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_RemoveItem;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte slot;
        public short remaining;

        public InventoryRemoveItemMessage()
        {

        }
        public InventoryRemoveItemMessage(uint netId, byte slot, short remaining) : base(netId)
        {
            this.slot = slot;
            this.remaining = remaining;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(slot);
            writer.WriteShort(remaining);
        }
    }
}
