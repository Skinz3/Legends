using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System.Text;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Chargement du modèle personnage
    /// </summary>
    public class HeroSpawnMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_HeroSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int playerId;
        public TeamId team;
        public int skinId;
        public string name;
        public string championType;

        public HeroSpawnMessage()
        {

        }
        public HeroSpawnMessage(uint netId, int playerId, TeamId team, int skinId, string name, string championType) : base(netId)
        {
            this.playerId = playerId;
            this.team = team;
            this.skinId = skinId;
            this.name = name;
            this.championType = championType;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(netId);
            writer.WriteInt(playerId);
            writer.WriteByte(40); // net node id?
            writer.WriteByte(0); // botskilllevel

            if (team == TeamId.BLUE)
            {
                writer.WriteByte(1);
            }
            else
            {
                writer.WriteByte(0);
            }

            writer.WriteByte(0); // is bot
            writer.WriteByte(0); // spawn pos index
            writer.WriteInt(skinId);



            foreach (var b in Encoding.Default.GetBytes(name))
                writer.WriteByte((byte)b);

            writer.Fill(0, 128 - name.Length);


            foreach (var b in Encoding.Default.GetBytes(championType))
                writer.WriteByte((byte)b);

            writer.Fill(0, 40 - championType.Length);


            writer.WriteFloat(0.0f);
            writer.WriteFloat(0.0f);
            writer.WriteInt(0);
            writer.WriteByte(0);
        }
    }
}
