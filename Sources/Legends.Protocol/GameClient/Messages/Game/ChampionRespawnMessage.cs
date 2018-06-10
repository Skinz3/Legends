using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ChampionRespawnMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ChampionRespawnAlive;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector2 position;

        public ChampionRespawnMessage(uint netId, Vector2 position) : base(netId)
        {
            this.position = position;
        }
        public ChampionRespawnMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(position.X);
            writer.WriteFloat(position.Y);
            writer.WriteFloat(0); // Z ? wtf? 
        }
    }
}
