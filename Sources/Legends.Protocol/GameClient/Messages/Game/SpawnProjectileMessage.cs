using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Legends.Core;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SpawnProjectileMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SpawnProjectile;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;


        public Vector3 position;
        public Vector3 casterPosition;
        public Vector3 direction;
        public Vector3 velocity;
        public Vector3 startPoint;
        public Vector3 endPoint;
        public Vector3 unitPosition;
        public float timeFromCreation;
        public float speed;
        public float lifePercentage;
        public float timedSpeedDelta;
        public float timedSpeedDeltaTime;
        public bool bounced;
        public CastInformations castInfo;

        public SpawnProjectileMessage()
        {

        }
        public SpawnProjectileMessage(uint netId, Vector3 position, Vector3 casterPosition, Vector3 direction,
            Vector3 velocity, Vector3 startPoint, Vector3 endPoint, Vector3 unitPosition, float timeFromCreation,
            float speed, float lifePercentage, float timedSpeedDelta, float timedSpeedDeltaTime, bool bounced, CastInformations castInfo) : base(netId)
        {
            this.netId = netId;
            this.position = position;
            this.casterPosition = casterPosition;
            this.direction = direction;
            this.velocity = velocity;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.unitPosition = unitPosition;
            this.timeFromCreation = timeFromCreation;
            this.speed = speed;
            this.lifePercentage = lifePercentage;
            this.timedSpeedDelta = timedSpeedDelta;
            this.timedSpeedDeltaTime = timedSpeedDeltaTime;
            this.bounced = bounced;
            this.castInfo = castInfo;


        }


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {

            position.Serialize(writer);
            casterPosition.Serialize(writer);
            direction.Serialize(writer);
            velocity.Serialize(writer);
            startPoint.Serialize(writer);
            endPoint.Serialize(writer);
            unitPosition.Serialize(writer);
            writer.WriteFloat(timeFromCreation);
            writer.WriteFloat(speed);
            writer.WriteFloat(lifePercentage);
            writer.WriteFloat(timedSpeedDelta);
            writer.WriteFloat(timedSpeedDeltaTime);

            byte bitfield = 0;
            if (bounced)
            {
                bitfield |= 1;
            }

            writer.WriteByte(bitfield);

            castInfo.Serialize(writer);
        }
    }
}
