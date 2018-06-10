using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
   public class ShowProjectileMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ForceCreateProjectile;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint projectileNetId;

        public ShowProjectileMessage(uint ownerId,uint projectileNetId):base(ownerId)
        {
            this.projectileNetId = projectileNetId;
        }
        public ShowProjectileMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(projectileNetId);
        }
    }
}
