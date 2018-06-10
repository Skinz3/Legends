using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using System.Text;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class PlayerInformations
    {
        public long userId;
        public short unk; // 0x1E
        public int summonerSkill1;
        public int summonerSkill2;
        public byte isBot;
        public int teamId;
        public string name; // 2x64 (buffer.fill(0,64) x2
        public string rank;
        public int summonerIcon;
        public short ribbon;

        public PlayerInformations(long userId, short unk, int summonerSkill1, int summonerSkill2, byte isBot, int teamId,
            string name, string rank, int summonerIcon, short ribbon)
        {
            this.userId = userId;
            this.unk = unk;
            this.summonerSkill1 = summonerSkill1;
            this.summonerSkill2 = summonerSkill2;
            this.isBot = isBot;
            this.teamId = teamId;
            this.name = name;
            this.rank = rank;
            this.summonerIcon = summonerIcon;
            this.ribbon = ribbon;
        }

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteLong(userId);
            writer.WriteShort(0x1E);
            writer.WriteInt(summonerSkill1);
            writer.WriteInt(summonerSkill2);

            writer.WriteByte(isBot);
            writer.WriteInt(teamId);

            writer.Fill(0, 64); // name is no longer here
            writer.Fill(0, 64);
            foreach (var b in Encoding.UTF8.GetBytes(rank))
                writer.WriteByte(b);
            writer.Fill(0, 24 - rank.Length);

            writer.WriteInt(summonerIcon);
            writer.WriteShort(ribbon);
        }
    }
}
