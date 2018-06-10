using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// ?
    /// </summary>
    public class ViewMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ViewAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte requestNo;

        public ViewMessage(byte requestNo)
        {
            this.requestNo = requestNo;
        }
        public ViewMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.requestNo = reader.ReadByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(requestNo);
        }
    }
}
