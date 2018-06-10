using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System.Text;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Working S2C and C2S
    /// </summary>
    public class ChatBoxMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_ChatBoxMessage;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_COMMUNICATION;
        public override Channel Channel => CHANNEL;

        public int playerId;
        public int botNetId;
        public byte isBotMessage;
        public ChatChannelType channel;
        public int unk1; // playerNo?
        public byte[] unk2 = new byte[32];
        public string content;
        public int length;

        public ChatBoxMessage()
        {

        }
        public ChatBoxMessage(int playerId, int botNetId, byte isBotMessage, ChatChannelType channel, int unk1,
            byte[] unk2, string content)
        {
            this.playerId = playerId;
            this.botNetId = botNetId;
            this.isBotMessage = isBotMessage;
            this.channel = channel;
            this.unk1 = unk1;
            this.unk2 = unk2;
            this.content = content;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.playerId = reader.ReadInt();
            this.botNetId = reader.ReadInt();
            this.isBotMessage = reader.ReadByte();
            this.channel = (ChatChannelType)reader.ReadInt();
            this.unk1 = reader.ReadInt();
            this.length = reader.ReadInt();
            this.unk2 = reader.ReadBytes(32);

            var bytes = new List<byte>();
            for (var i = 0; i < length; i++)
                bytes.Add(reader.ReadByte());
            this.content = Encoding.UTF8.GetString(bytes.ToArray());
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(playerId);
            writer.WriteInt(botNetId);
            writer.WriteByte(isBotMessage);
            writer.WriteInt((int)channel);
            writer.WriteInt(unk1);
            writer.WriteInt(length);
            writer.WriteBytes(unk2);

            foreach (var b in Encoding.UTF8.GetBytes(content))
                writer.WriteByte((byte)b);

            writer.WriteByte(0);
        }
    }
}
