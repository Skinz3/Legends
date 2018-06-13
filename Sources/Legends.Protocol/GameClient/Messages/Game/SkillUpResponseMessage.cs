using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SkillUpResponseMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SkillUp;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte skillId;
        public byte level;
        public byte skillPoints;

        public SkillUpResponseMessage()
        {

        }
        public SkillUpResponseMessage(uint netId, byte skillId, byte level, byte skillPoints) : base(netId)
        {
            this.skillId = skillId;
            this.level = level;
            this.skillPoints = skillPoints;
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(skillId);
            writer.WriteByte(level);
            writer.WriteByte(skillPoints);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
