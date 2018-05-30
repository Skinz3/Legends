using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.LoadingScreen
{
    public class StartSpawnMessage : StateMessage2
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_StartSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public override void Deserialize(LittleEndianReader reader)
        {
            
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            
        }
    }
}
