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
using Legends.Core;

namespace Legends.Configurations
{
    public class ConfigurationProvider : Singleton<ConfigurationProvider>
    {
        public static string PATH = Environment.CurrentDirectory + "/config.xml";

        public Configuration Configuration
        {
            get;
            private set;
        }

        public void LoadDefault()
        {
            Configuration = new Configuration()
            {
                ServerIp = "127.0.0.1",
                ServerPort = 5119,
                DatabaseName = "legends",
                LeaguePath = @"C:\Users\Skinz\Desktop\Emulateur LoL\League of Legends 4.20\League of Legends\RADS\solutions\lol_game_client_sln\releases\0.0.1.68\deploy\",
                MySQLHost = "127.0.0.1",
                MySQLUser = "root",
                MySQLPassword = "",
                StartClient = true,

                Players = new List<PlayerData>()
                {
                    new PlayerData()
                    {
                        UserId = 1,
                        Name = "Skinz",
                        ChampionName = "Ezreal",
                        SkinId = 5,
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
                        Name = "Skinz2",
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

            File.WriteAllText(PATH, Configuration.XMLSerialize());
        }

        public PlayerData GetPlayerData(long userId)
        {
            return Configuration.Players.FirstOrDefault(x => x.UserId == userId);
        }

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public void LoadConfiguration()
        {
            if (File.Exists(PATH) == false)
            {
                LoadDefault();
            }
            else
            {
                this.Configuration = File.ReadAllText(PATH).XMLDeserialize<Configuration>();
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
