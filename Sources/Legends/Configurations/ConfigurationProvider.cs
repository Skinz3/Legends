using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Enum;
using Legends.Core.Utils;
using Legends.World;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using Legends.Protocol.GameClient.LoadingScreen;

namespace Legends.Configurations
{
    public class ConfigurationProvider : Singleton<ConfigurationProvider>
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

            File.WriteAllText(PATH, JsonConvert.SerializeObject(Configuration));
        }

        public PlayerData GetPlayerData(long userId)
        {
            return Configuration.Players.FirstOrDefault(x => x.UserId == userId);
        }

        [StartupInvoke("Configuration",StartupInvokePriority.Primitive)]
        public void LoadConfiguration()
        {
            if (File.Exists(PATH) == false)
            {
                LoadDefault();
            }
            else
            {
                this.Configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(PATH));
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
