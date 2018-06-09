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
    public class AttentionPingAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_AttentionPing;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector2 position;
        public uint targetNetId;
        public uint sourceNetId;
        public PingTypeEnum pingType;

        public AttentionPingAnswerMessage()
        {

        }
        public AttentionPingAnswerMessage(Vector2 position, uint targetNetId, uint sourceNetId, PingTypeEnum pingType)
        {
            this.position = position;
            this.targetNetId = targetNetId;
            this.sourceNetId = sourceNetId;
            this.pingType = pingType;
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(position.X);
            writer.WriteFloat(position.Y);

            writer.WriteUInt(targetNetId);
            writer.WriteUInt(sourceNetId);
            writer.WriteByte((byte)pingType);
            writer.WriteByte(0xFB);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
