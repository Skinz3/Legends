using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SellItemMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_SellItem;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public byte slotId;

        public SellItemMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            slotId = reader.ReadByte();
        }
    }
}
