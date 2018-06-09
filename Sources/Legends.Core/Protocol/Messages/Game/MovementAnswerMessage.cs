using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Geometry;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// omfg 
    /// </summary>
    public class MovementAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_MoveAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_LOW_PRIORITY;
        public override Channel Channel => CHANNEL;

        public Vector2[] wayPoints;
        public uint actorNetId;
        public Vector2 mapSize;

        public MovementAnswerMessage(uint netId, Vector2[] wayPoints, uint actorNetId, Vector2 mapSize) : base(netId)
        {
            this.wayPoints = wayPoints;
            this.actorNetId = actorNetId;
            this.mapSize = mapSize;
        }
        public MovementAnswerMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(Environment.TickCount); // syncID
            writer.WriteShort(1);

            var numCoords = wayPoints.Count() * 2;
            writer.WriteByte((byte)numCoords);
            writer.WriteUInt(actorNetId);
            writer.WriteBytes(MovementVector.EncodeWaypoints(wayPoints, mapSize));

        }
      

    }
}
