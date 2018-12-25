using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Types;
using System.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class WaypointGroupMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_MoveAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_LOW_PRIORITY;
        public override Channel Channel => CHANNEL;

        public int syncId;
        public List<MovementDataNormal> movements;


        public WaypointGroupMessage(uint netId, int syncId, List<MovementDataNormal> movements) : base(netId)
        {
            this.syncId = syncId;
            this.movements = movements;
        }
        public WaypointGroupMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            int count = movements.Count;
            if (count > 0x7FFF)
            {
                throw new IOException("Too many movementdata!");
            }
            writer.WriteInt(syncId);
            writer.WriteShort((short)count);

            foreach (var data in movements)
            {
                data.Write(writer);
            }

        }


    }
}
