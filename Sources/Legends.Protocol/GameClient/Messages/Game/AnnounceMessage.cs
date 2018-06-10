using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class AnnounceMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Announce;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int mapId;
        public AnnounceEnum announce;

        public AnnounceMessage(uint netId, int mapId, AnnounceEnum announce) : base(netId)
        {
            this.mapId = mapId;
            this.announce = announce;
        }
        public AnnounceMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)announce);
            writer.WriteLong((long)0);

            if (mapId > 0)
            {
                writer.WriteInt(1);
            }
        }
    }
}
