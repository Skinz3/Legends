using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CastSpellAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CastSpellAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int casterPositionSyncId;
        public bool unknown1;
        public CastInformations castInfo;

        public CastSpellAnswerMessage()
        {

        }
     
        public CastSpellAnswerMessage(uint netId,int casterPositionSyncId,bool unknown1,CastInformations castInfo) : base(netId)
        {
            this.casterPositionSyncId = casterPositionSyncId;
            this.unknown1 = unknown1;
            this.castInfo = castInfo;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(casterPositionSyncId);

            byte bitfield = 0;
            if (unknown1)
            {
                bitfield |= 1;
            }
            writer.WriteByte(bitfield);

            castInfo.Serialize(writer);
        }
    }
}
