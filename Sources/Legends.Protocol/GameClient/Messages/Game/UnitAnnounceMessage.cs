using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class UnitAnnounceMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Announce2;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public UnitAnnounceEnum announceEnum;
        public uint sourceNetId;
        public uint[] assitsNetIds;

        public UnitAnnounceMessage()
        {

        }
        public UnitAnnounceMessage(uint netId, UnitAnnounceEnum announce, uint sourceNetId, uint[] assitsNetIds):base(netId)
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
