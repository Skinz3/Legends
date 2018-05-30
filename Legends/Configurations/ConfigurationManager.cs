using Legends.Core.JSON;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.LoadingScreen;
using Legends.Core.Utils;
using Legends.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Configurations
{
    public class ConfigurationManager : Singleton<ConfigurationManager>
    {
        public static string PATH = Environment.CurrentDirectory + "/config.json";

        public Configuration Configuration
        {
            get;
            private set;
        }

        public void LoadDefault()
        {
            Configuration = new Configuration()
            {
                MySQLHost = "127.0.0.1",
                DatabaseName = "legends",
                MySQLPassword = "",
                MySQLUser = "root",
                ServerPort = 5119,
                Players = new List<PlayerData>()
                {
                    new PlayerData()
                    {
                        UserId = 1,
                        Name = "Skinz",
                        ChampionName = "Riven",
                        SkinId = 0,
                        Summoner1 = "FLASH",
                        Summoner2 = "TELEPORT",
                        Team = "PURPLE",
                        Rank = "DIAMOND",
                        Ribbon = 1,
                        SummonerIcon = 1,
                    },
                    new PlayerData()
                    {
                        UserId = 2,
                        Name = "Test",
                        ChampionName = "Yasuo",
                        SkinId = 1,
                        Team = "BLUE",
                        Summoner1 = "FLASH",
                        Summoner2 = "IGNITE",
                        Rank = "CHALLENGER",
                        Ribbon = 1,
                        SummonerIcon = 2,
                    }
                }
            };


            JsonSerializer<Configuration> config = new JsonSerializer<Configuration>();
            config.Serialize(Configuration, PATH);
        }

        public PlayerData GetPlayerData(long userId)
        {
            return Configuration.Players.FirstOrDefault(x => x.UserId == userId);
        }

        public void LoadConfiguration()
        {
            if (File.Exists(PATH) == false)
            {
                LoadDefault();
            }
            else
            {
                JsonSerializer<Configuration> serializer = new JsonSerializer<Configuration>();
                this.Configuration = serializer.Deserialize(PATH);
            }
        }

        public PlayerInformations[] GetPlayersInformations()
        {
            return Configuration.Players.ConvertAll<PlayerInformations>(x => x.GetPlayerInformations()).ToArray();
        }

        public long[] GetPurpleIds()
        {
            return Configuration.Players.FindAll(x => x.TeamId == TeamId.PURPLE).ConvertAll(x => x.UserId).ToArray();
        }

        public long[] GetBlueIds()
        {
            return Configuration.Players.FindAll(x => x.TeamId == TeamId.BLUE).ConvertAll(x => x.UserId).ToArray();
        }
    }


}
