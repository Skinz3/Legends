using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BuyItemAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BuyItemAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int itemId;
        public byte slot;
        public byte stacks;
        public byte unk;

        public BuyItemAnswerMessage(uint netId, int itemId, byte slot, byte stacks, byte unk) : base(netId)
        {
            this.itemId = itemId;
            this.slot = slot;
            this.stacks = stacks;
            this.unk = unk;
        }
        public BuyItemAnswerMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt((int)itemId);
            writer.WriteByte((byte)slot);
            writer.WriteByte((byte)stacks);
            writer.WriteByte((byte)0); //unk or stacks => short
            writer.WriteByte((byte)0x29); //unk (turret 0x01 and champions 0x29)
        }
    }
}
