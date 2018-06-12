using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Geometry;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class DashMessage : GameMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Dash;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint sourceNetId;
        public float dashSpeed;
        public float leapHeight;
        public Vector2 unitPosition;
        public bool keepFacingLastDirection;
        public uint targetNetId;
        public Vector2 mapSize;
        public Vector2 targetPosition;
        public float followTargetMaxDistance;
        public float backDistance;
        public float travelTime;

        public DashMessage(uint sourceNetId, float dashSpeed, float leapHeight, Vector2 unitPosition,
            bool keepFacingLastDirection, uint targetNetId, Vector2 mapSize, Vector2 targetPosition,
            float followTargetMaxDistance, float backDistance, float travelTime)
        {
            this.sourceNetId = sourceNetId;
            this.dashSpeed = dashSpeed;
            this.leapHeight = leapHeight;
            this.unitPosition = unitPosition;
            this.keepFacingLastDirection = keepFacingLastDirection;
            this.targetNetId = targetNetId;
            this.mapSize = mapSize;
            this.targetPosition = targetPosition;
            this.followTargetMaxDistance = followTargetMaxDistance;
            this.backDistance = backDistance;
            this.travelTime = travelTime;
        }
        public DashMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteShort((short)1); // Number of dashes

            writer.WriteByte((byte)4); // Waypoints size * 2
            writer.WriteUInt((uint)sourceNetId);
            writer.WriteFloat((float)dashSpeed);
            writer.WriteFloat((float)leapHeight);
            writer.WriteFloat((float)unitPosition.X);
            writer.WriteFloat((float)unitPosition.Y);

            writer.WriteByte((byte)(keepFacingLastDirection ? 0x01 : 0x00));

            writer.WriteUInt(targetNetId);

            writer.WriteFloat((float)followTargetMaxDistance);
            writer.WriteFloat((float)backDistance);
            writer.WriteFloat((float)travelTime);

            var waypoints = new List<Vector2>
            {
                new Vector2(unitPosition.X, unitPosition.Y),
                new Vector2(targetPosition.X, targetPosition.Y)
            };

            writer.WriteBytes(MovementVector.EncodeWaypoints(waypoints.ToArray(), mapSize));

        }
    }
}
