using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class PingLoadInfoAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Ping_Load_Info;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int unk1;
        public long userId;
        public float loaded;
        public float ping;
        public short unk2;
        public short unk3;
        public byte unk4;

        public PingLoadInfoAnswerMessage(int unk1, long userId, float loaded, float ping, short unk2, short unk3, byte unk4, uint netId) : base(netId)
        {
            this.unk1 = unk1;
            this.userId = userId;
            this.loaded = loaded;
            this.ping = ping;
            this.unk2 = unk2;
            this.unk3 = unk3;
            this.unk4 = unk4;
        }
        public PingLoadInfoAnswerMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.unk1 = reader.ReadInt();
            this.userId = reader.ReadLong();
            this.loaded = reader.ReadFloat();
            this.ping = reader.ReadFloat();
            this.unk2 = reader.ReadShort();
            this.unk3 = reader.ReadShort();
            this.unk4 = reader.ReadByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(unk1);
            writer.WriteLong(userId);
            writer.WriteFloat(loaded);
            writer.WriteFloat(ping);
            writer.WriteShort(unk2);
            writer.WriteShort(unk3);
            writer.WriteByte(unk4);
        }
    }
}
