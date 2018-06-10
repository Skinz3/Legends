using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class HeartBeatMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_HeartBeat;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_GAMEPLAY;
        public override Channel Channel => CHANNEL;

        public float receiveTime;
        public float ackTime;

        public HeartBeatMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.receiveTime = reader.ReadFloat();
            this.ackTime = reader.ReadFloat();
        }
    }
}
