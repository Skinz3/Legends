using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Other
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

        public DebugMessage(int netId,string message) : base(netId)
        {
            this.message = message;
        }
        public DebugMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(0);
            foreach (var b in Encoding.Default.GetBytes(message))
                writer.WriteByte(b);
            writer.Fill(0, 512 - message.Length);
        }

        public override void Deserialize(LittleEndianReader reader)
        {

        }
    }
}
