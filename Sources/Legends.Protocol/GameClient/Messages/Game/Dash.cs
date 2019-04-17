using Legends.Core;
using Legends.Core.Geometry;
using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class Dash : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_WaypointGroupWithSpeed;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint OwnerNetId;

        public float dashSpeed;
        public float leapHeigth;
        public Vector2 OwnerPosition;
        public bool keepFacingLastDirection;
        public float followTargetMaxDistance;
        public float backDistance;
        public float travelTime;

        public Vector2 targetPosition;
        public Vector2 mapSize;

        public Dash(uint netId, uint ownerNetId, float dashSpeed, float leapHeigth, Vector2 ownerPosition, bool keepFacingLastDirection,
            float followTargetMaxDistance, float backDistance, float travelTime, Vector2 targetPosition,
            Vector2 mapSize) : base(netId)
        {
            this.OwnerNetId = ownerNetId;
            this.dashSpeed = dashSpeed;
            this.leapHeigth = leapHeigth;
            this.OwnerPosition = ownerPosition;
            this.keepFacingLastDirection = keepFacingLastDirection;
            this.followTargetMaxDistance = followTargetMaxDistance;
            this.backDistance = backDistance;
            this.travelTime = travelTime;
            this.targetPosition = targetPosition;
            this.mapSize = mapSize;
        }

        public Dash()
        {
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Environment.TickCount); // syncID
            writer.WriteShort((short)1); // Number of dashes
            writer.WriteByte((byte)4); // Waypoints size * 2
            writer.WriteUInt(OwnerNetId);
            writer.WriteFloat(dashSpeed);
            writer.WriteFloat(leapHeigth);

            OwnerPosition.Serialize(writer);

            writer.WriteByte((byte)(keepFacingLastDirection ? 0x01 : 0x00));

            if (true) // target is position?
            {
                writer.WriteUInt((uint)0);
            }
            else
            {
                //  WriteNetId(t as IGameObject);
            }

            writer.WriteFloat(followTargetMaxDistance);

            writer.WriteFloat(backDistance);
            writer.WriteFloat(travelTime);

            var waypoints = new List<Vector2>
            {
                new Vector2(OwnerPosition.X, OwnerPosition.Y),
                new Vector2(targetPosition.X, targetPosition.Y)
            };


            writer.WriteBytes(MovementVector.EncodeWaypoints(waypoints.ToArray(), mapSize));
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
