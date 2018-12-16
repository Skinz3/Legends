using Legends.Core;
using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class OnAttackMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_OnAttack;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public AttackTypeEnum attackType;
        public Vector3 targetPosition;
        public uint targetNetId;

        public OnAttackMessage()
        {

        }
        public OnAttackMessage(uint netId, AttackTypeEnum type, Vector3 targetPosition,
            uint targetNetId) : base(netId)
        {
            this.attackType = type;
            this.targetPosition = targetPosition;
            this.targetNetId = targetNetId;
        }


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)attackType);
            targetPosition.Serialize(writer);
            writer.WriteUInt(targetNetId);
        }
    }
}
