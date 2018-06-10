using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class EndSpawnMessage : StateMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_EndSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public EndSpawnMessage(uint netId) : base(netId)
        {

        }
        public EndSpawnMessage()
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
