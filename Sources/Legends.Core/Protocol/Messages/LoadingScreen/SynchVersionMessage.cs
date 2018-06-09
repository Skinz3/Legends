using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.LoadingScreen
{
    public class SynchVersionMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_SynchVersion;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public int unk1;
        public byte[] version;// byte[256];

        public SynchVersionMessage()
        {

        }
        public SynchVersionMessage(uint netId, int unk1, byte[] version) : base(netId)
        {
            this.unk1 = unk1;
            this.version = version;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.unk1 = reader.ReadInt();
            this.version = reader.ReadBytes(256);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(unk1);
            writer.WriteBytes(version);
        }
    }
}
