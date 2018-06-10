using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class MovementConfirmationMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_MoveConfirm;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public MovementConfirmationMessage()
        {

        }
        /// <summary>
        /// A actually dont know what this 9 bytes are for...
        /// </summary>
        /// <param name="reader"></param>
        public override void Deserialize(LittleEndianReader reader)
        {
           
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            
        }
    }
}
