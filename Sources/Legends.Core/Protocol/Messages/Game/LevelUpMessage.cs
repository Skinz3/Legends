using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Game
{
    public class LevelUpMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_LevelUp;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte newLevel;
        public short skillPoints;


        public LevelUpMessage(int netId, byte newLevel, short skillPoints) : base(netId)
        {
            this.newLevel = newLevel;
            this.skillPoints = skillPoints;
        }
        public LevelUpMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(newLevel);
            writer.WriteShort(skillPoints);
        }
    }
}
