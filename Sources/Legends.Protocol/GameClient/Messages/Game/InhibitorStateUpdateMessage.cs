using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class InhibitorStateUpdateMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_InhibitorState;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public InhibitorStateEnum state;

        public InhibitorStateUpdateMessage(uint netId, InhibitorStateEnum state):base(netId)
        {
            this.state = state;
        }
        public InhibitorStateUpdateMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)state);
            writer.WriteByte((byte)0);
            writer.WriteByte((byte)0);
        }
    }
}
