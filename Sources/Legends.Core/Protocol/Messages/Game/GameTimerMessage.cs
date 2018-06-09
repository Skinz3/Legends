using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Game
{
    public class GameTimerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_GameTimer;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public float time;

        public GameTimerMessage(uint netId, float time) : base(netId)
        {
            this.time = time;
        }
        public GameTimerMessage()
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
