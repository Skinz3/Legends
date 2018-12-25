using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Core;
using Legends.Protocol.GameClient.Enum;
using System.Numerics;
using Legends.Protocol.GameClient.Types;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BasicAttackMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BasicAttack;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public ProtocolBasicAttack basicAttack;

        public BasicAttackMessage(uint sourceNetId, ProtocolBasicAttack basicAttack) : base(sourceNetId)
        {
            this.basicAttack = basicAttack;
        }

        public BasicAttackMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            basicAttack.Serialize(writer);
        }
    }
}
