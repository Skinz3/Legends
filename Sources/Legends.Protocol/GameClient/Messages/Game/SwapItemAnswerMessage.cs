using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SwapItemAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SwapItems;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte slotFrom;
        public byte slotTo;

        public SwapItemAnswerMessage()
        {

        }
        public SwapItemAnswerMessage(uint netId, byte slotFrom, byte slotTo) : base(netId)
        {
            this.slotFrom = slotFrom;
            this.slotTo = slotTo;
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(slotFrom);
            writer.WriteByte(slotTo);
        }

        public override void Deserialize(LittleEndianReader reader)
        {

        }
    }
}
