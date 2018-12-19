using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BeginAutoAttackMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BeginAutoAttack;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint targetNetId;
        public byte extraTime;
        public uint futureProjectileNetId;
        public AttackSlotEnum slot;
        public Vector2 targetPosition;
        public Vector2 sourcePosition;
        public Vector2 middleOfMap;


        public BeginAutoAttackMessage(uint sourceNetId, uint targetNetId, byte extraTime, uint futureProjectileNetId, AttackSlotEnum slot,
            Vector2 targetPosition, Vector2 sourcePosition, Vector2 middleOfMap) : base(sourceNetId)
        {
            this.extraTime = extraTime;
            this.futureProjectileNetId = futureProjectileNetId;
            this.slot = slot;
            this.targetNetId = targetNetId;
            this.sourcePosition = sourcePosition;
            this.targetPosition = targetPosition;
        }
        public BeginAutoAttackMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(targetNetId);
            writer.WriteByte((byte)extraTime); // extraTime
            writer.WriteUInt(futureProjectileNetId); // Basic attack projectile ID, to be spawned later

            writer.WriteByte((byte)slot); // attackSlot

            writer.WriteByte((byte)0x80); // not sure what this is, but it should be correct (or maybe attacked x z y?) - 4.18
            writer.WriteByte((byte)0x01);

            writer.WriteShort((short)MovementVector.TargetXToNormalFormat(targetPosition.X, middleOfMap));
            writer.WriteByte((byte)0x80);
            writer.WriteByte((byte)0x01);
            writer.WriteShort((short)MovementVector.TargetYToNormalFormat(targetPosition.Y, middleOfMap));

            //    writer.WriteFloat(targetPosition.X);
            //    writer.WriteFloat(targetPosition.Y);



            writer.WriteByte((byte)0xCC);
            writer.WriteByte((byte)0x35);
            writer.WriteByte((byte)0xC4);
            writer.WriteByte((byte)0xD1);
            writer.WriteFloat(sourcePosition.X);
            writer.WriteFloat(sourcePosition.Y);
        }
    }
}
