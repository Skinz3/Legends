using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Core.Protocol.Enum;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// Chargement du modèle personnage
    /// </summary>
    public class HeroSpawnMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_HeroSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int netId;
        public int playerId;
        public TeamId team;
        public int skinId;
        public string name;
        public string championType;

        public HeroSpawnMessage()
        {

        }
        public HeroSpawnMessage(int netId,int playerId,TeamId team,int skinId,string name,string championType)
        {
            this.netId = netId;
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
            writer.WriteInt(0);
            writer.WriteInt(netId);
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
