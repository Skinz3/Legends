using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class FXKillMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FXKill;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;

        public uint particleNetId;

        public FXKillMessage(uint netId, uint particleNetId) : base(netId)
        {
            this.particleNetId = particleNetId;
        }

        public FXKillMessage()
        {
        }

        public override Channel Channel => CHANNEL;


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(particleNetId);
        }
    }
}
