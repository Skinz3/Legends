using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class QueryStatusRequestMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_QueryStatusReq;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public byte undefined;

        public QueryStatusRequestMessage(byte undefined)
        {
            this.undefined = undefined;
        }
        public QueryStatusRequestMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.undefined = reader.ReadByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(undefined);

        }
    }
}
