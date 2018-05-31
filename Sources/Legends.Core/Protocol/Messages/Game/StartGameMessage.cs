using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// Obvious :p
    /// </summary>
    public class StartGameMessage : StateMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_StartGame;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public StartGameMessage(int netId) : base(netId)
        {
                
        }
        public StartGameMessage()
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
