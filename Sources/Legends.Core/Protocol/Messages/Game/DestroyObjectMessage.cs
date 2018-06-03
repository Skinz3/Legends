using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Game
{
    public class DestroyObjectMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_DestroyObject;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int sourceNetId;

        public int targetNetId;

        public DestroyObjectMessage(int sourceNetId, int targetNetId) : base(sourceNetId)
        {
            this.sourceNetId = sourceNetId;
            this.targetNetId = targetNetId;
        }
        public DestroyObjectMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(targetNetId);
        }
    }
}
