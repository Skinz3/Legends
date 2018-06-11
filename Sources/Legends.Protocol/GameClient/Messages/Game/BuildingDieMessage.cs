using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BuildingDieMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BuildingDie;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint killerNetId;

        public BuildingDieMessage()
        {

        }
        public BuildingDieMessage(uint killerNetId, uint inhibNetId) : base(inhibNetId)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(killerNetId);
            writer.WriteInt(0); // unks
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
