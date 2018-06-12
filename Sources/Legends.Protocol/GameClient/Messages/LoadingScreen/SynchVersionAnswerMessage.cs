using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class SynchVersionAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SynchVersion;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte unk; // = 9?
        public int mapId;
        public PlayerInformations[] players;
        public string version; // "NA1"
        public GameModeEnum gameMode;
        public string region;
        public int gameFeatures; // 487826

        public SynchVersionAnswerMessage(uint netId, byte unk, int mapId, PlayerInformations[] players,
            string version, GameModeEnum gameMode, string region, int gameFeatures) : base(netId)
        {
            this.unk = unk;
            this.mapId = mapId;
            this.players = players;
            this.version = version;
            this.gameMode = gameMode;
            this.region = region;
            this.gameFeatures = gameFeatures;
        }
        public SynchVersionAnswerMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(unk);
            writer.WriteInt(mapId);

            foreach (var player in players)
            {
                player.Serialize(writer);
            }
            for (var i = 0; i < 12 - players.Length; ++i)
            {
                writer.WriteLong(-1);
                writer.Fill(0, 173);
            }
            foreach (var b in Encoding.UTF8.GetBytes(version))
                writer.WriteByte(b);

            writer.Fill(0, 256 - version.Length);

            string gameModeString = gameMode.ToString();
            foreach (var b in Encoding.UTF8.GetBytes(gameModeString))
                writer.WriteByte(b);

            writer.Fill(0, 128 - gameModeString.Length);

            foreach (var b in Encoding.UTF8.GetBytes(region))
                writer.WriteByte((byte)b);
            writer.Fill(0, 2333); // 128 - 3 + 661 + 1546
            writer.WriteInt(gameFeatures); // gameFeatures (turret range indicators, etc.)
            writer.Fill(0, 256);
            writer.WriteInt((int)0);
            writer.Fill(1, 19);
        }
    }
}
