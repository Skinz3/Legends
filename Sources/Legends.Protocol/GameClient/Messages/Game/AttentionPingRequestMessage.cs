using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class AttentionPingRequestMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_AttentionPing;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public Vector2 position;
        public uint targetNetId;
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
            this.targetNetId = reader.ReadUInt();
            this.pingType = (PingTypeEnum)reader.ReadByte();
        }
    }
}
