using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class DestroyObjectMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_DestroyObject;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint sourceNetId;

        public uint targetNetId;

        public DestroyObjectMessage(uint sourceNetId, uint targetNetId) : base(sourceNetId)
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
            writer.WriteUInt(targetNetId);
        }
    }
}
