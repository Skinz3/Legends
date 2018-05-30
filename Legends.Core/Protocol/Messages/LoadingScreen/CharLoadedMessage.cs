using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.LoadingScreen
{
    public class CharLoadedMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_CharLoaded;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public CharLoadedMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }
}
