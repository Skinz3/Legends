using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ClickMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_Click;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public int zero;
        public uint targetNetId;

        public ClickMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.zero = reader.ReadInt();
            this.targetNetId = reader.ReadUInt();
        }
    }
}
