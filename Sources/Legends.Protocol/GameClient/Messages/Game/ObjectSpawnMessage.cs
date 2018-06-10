using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ObjectSpawnMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ObjectSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_GAMEPLAY;
        public override Channel Channel => CHANNEL;

        public ObjectSpawnMessage(uint netId):base(netId)
        {
            
        }
        public ObjectSpawnMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Fill(0, 3);
        }
    }
}
