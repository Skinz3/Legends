using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Protocol.Enum;

namespace Legends.Core.Protocol.Messages.Game
{
    public class AttentionPingRequestMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_AttentionPing;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public Vector2 position;
        public int targetNetId;
        public PingTypeEnum pingType;

        public AttentionPingRequestMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.position = new Vector2(reader.ReadFloat(), reader.ReadFloat());
            this.targetNetId = reader.ReadInt();
            this.pingType = (PingTypeEnum)reader.ReadByte();
        }
    }
}
