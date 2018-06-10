using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SkillUpRequestMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_SkillUp;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public byte skillId;

        public SkillUpRequestMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.skillId = reader.ReadByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
