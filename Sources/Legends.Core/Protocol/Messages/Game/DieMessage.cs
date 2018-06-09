using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Extended
{
    public class DieMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Die_MapView;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint sourceNetId;

        public DieMessage()
        {

        }
        public DieMessage(uint sourceNetId,uint targetNetId):base(targetNetId)
        {
            this.sourceNetId = sourceNetId;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt((int)0);
            writer.WriteByte((byte)0);
            writer.WriteUInt(sourceNetId);
            writer.WriteByte((byte)0); // unk
            writer.WriteByte((byte)7); // unk
            writer.WriteInt((int)0); // Flags?
        }
    }
}
