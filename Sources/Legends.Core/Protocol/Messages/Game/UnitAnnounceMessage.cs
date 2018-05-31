using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Core.Protocol.Enum;

namespace Legends.Core.Protocol.Messages.Game
{
    public class UnitAnnounceMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Announce2;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public UnitAnnounceEnum announceEnum;
        public int sourceNetId;
        public int[] assitsNetIds;

        public UnitAnnounceMessage()
        {

        }
        public UnitAnnounceMessage(int netId, UnitAnnounceEnum announce, int sourceNetId, int[] assitsNetIds):base(netId)
        {
            this.announceEnum = announce;
            this.sourceNetId = sourceNetId;
            this.assitsNetIds = assitsNetIds;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)announceEnum);

            if (sourceNetId != 0)
            {
                writer.WriteLong((long)sourceNetId);
                writer.WriteInt(assitsNetIds.Length);
                foreach (var a in assitsNetIds)
                    writer.WriteUInt((uint)a);
                for (int i = 0; i < 12 - assitsNetIds.Length; i++)
                    writer.WriteInt((int)0);
            }
        }
    }
}
