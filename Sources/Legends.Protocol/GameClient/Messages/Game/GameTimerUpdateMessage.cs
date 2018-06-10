using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class GameTimerUpdateMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_GameTimerUpdate;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public float time;

        public GameTimerUpdateMessage(uint netId, float time) : base(netId)
        {
            this.time = time;
        }
        public GameTimerUpdateMessage()
        {

        }


        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(time);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
