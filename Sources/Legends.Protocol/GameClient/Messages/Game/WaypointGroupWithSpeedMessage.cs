using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class WaypointGroupWithSpeedMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_WaypointGroupWithSpeed;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public MovementDataWithSpeed[] movementDatas;
        public int syncId;

        public WaypointGroupWithSpeedMessage(uint netId, MovementDataWithSpeed[] movementDatas,
            int syncId) : base(netId)
        {
            this.movementDatas = movementDatas;
            this.syncId = syncId;
        }

        public WaypointGroupWithSpeedMessage()
        {
        }


        public override void Serialize(LittleEndianWriter writer)
        {
            int count = movementDatas.Length;
            if (count > 0x7FFF)
            {
                throw new IOException("Too many movementdata!");
            }
            writer.WriteInt(syncId);
            writer.WriteShort((short)count);

            foreach (var data in movementDatas)
            {
                data.Serialize(writer);
            }
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
