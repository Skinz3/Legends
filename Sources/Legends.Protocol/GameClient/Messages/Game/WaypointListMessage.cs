using Legends.Core;
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
    public class WaypointListMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_WaypointList;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public int syncId;
        public Vector2[] waypoints;

        public WaypointListMessage()
        {

        }
        public WaypointListMessage(uint netId, int syncId, Vector2[] waypoints) : base(netId)
        {
            this.syncId = syncId;
            this.waypoints = waypoints;
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(syncId);

            foreach (var waypoint in waypoints)
            {
                waypoint.Serialize(writer);
            }
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
