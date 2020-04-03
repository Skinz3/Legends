using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.LoadingScreen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Configurations
{
    public class PlayerData
    {
        public long UserId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string ChampionName
        {
            get;
            set;
        }
        public int SkinId
        {
            get;
            set;
        }
        public TeamId Team
        {
            get;
            set;
        }
       
        public SummonerSpellId Summoner1
        {
            get;
            set;
        }
        public SummonerSpellId Summoner2
        {
            get;
            set;
        }
        public string Rank
        {
            get;
            set;
        }
        public int SummonerIcon
        {
            get;
            set;
        }
        public int Ribbon
        {
            get;
            set;
        }
        public PlayerInformations GetPlayerInformations()
        {
            return new PlayerInformations(UserId, 0, (int)Summoner1, (int)Summoner2, 0, (int)Team, Name, Rank, SummonerIcon, (short)Ribbon);
        }

    }
}
