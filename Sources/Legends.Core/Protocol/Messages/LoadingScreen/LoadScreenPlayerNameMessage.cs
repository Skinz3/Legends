using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.LoadingScreen
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
            foreach (var b in Encoding.Default.GetBytes(name))
                writer.WriteByte(b);

           writer.WriteByte(description);
        }
    }
}
