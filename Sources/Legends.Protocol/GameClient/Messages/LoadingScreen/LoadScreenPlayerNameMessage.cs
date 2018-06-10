using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class LoadScreenPlayerNameMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_LoadName;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public long userId;
        public int skinId;
        public string name;
        public byte description;

        public LoadScreenPlayerNameMessage(long userId, int skinId, string name, byte description)
        {
            this.name = name;
            this.userId = userId;
            this.skinId = skinId;
            this.description = description;
        }
        public LoadScreenPlayerNameMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteLong(userId);
            writer.WriteInt(0);
            writer.WriteInt(name.Length + 1);
            foreach (var b in Encoding.UTF8.GetBytes(name))
                writer.WriteByte(b);

           writer.WriteByte(description);
        }
    }
}
