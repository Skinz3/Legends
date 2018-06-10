using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;

namespace Legends.Protocol.GameClient.Other
{
    /// <summary>
    /// workin'?
    /// </summary>
    public class DebugMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_DebugMessage;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        string message;

        public DebugMessage(uint netId,string message) : base(netId)
        {
            this.message = message;
        }
        public DebugMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(0);
            foreach (var b in Encoding.UTF8.GetBytes(message))
                writer.WriteByte(b);
            writer.Fill(0, 512 - message.Length);
        }

        public override void Deserialize(LittleEndianReader reader)
        {

        }
    }
}
