using Legends.Core.Geometry;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Types;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Apparition d'un object dans le champ de vision
    /// </summary>
    public class OnEnterVisiblityClientMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ObjectSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public VisibilityData visibilityData;

        public OnEnterVisiblityClientMessage()
        {

        }
        public OnEnterVisiblityClientMessage(uint netId, VisibilityData data) : base(netId)
        {
            this.visibilityData = data;


        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUShort(0);
            visibilityData.Serialize(writer);
        }
    }

}
