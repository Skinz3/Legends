using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class DestroyClientMissile : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_DestroyClientMissile;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public DestroyClientMissile(uint netId) : base(netId)
        {
        }
        public DestroyClientMissile()
        {

        }


        public override void Serialize(LittleEndianWriter writer)
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {

        }
    }
}
