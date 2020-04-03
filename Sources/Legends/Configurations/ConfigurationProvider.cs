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
using Legends.Protocol.GameClient.LoadingScreen;
using Legends.Core;
using Legends.Core.IO;

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
                ServerIp = "127.0.0.1",
                ServerPort = 5119,
                DatabaseName = "legends",
                LeaguePath = @"D:\Emulateur LoL\League of Legends 4.20\League of Legends\RADS\solutions\lol_game_client_sln\releases\0.0.1.68\deploy\",
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
                        Summoner1 =  SummonerSpellId.SummonerFlash,
                        Summoner2 =  SummonerSpellId.SummonerTeleport,
                        Team = TeamId.PURPLE,
                        Rank = "DIAMOND",
                        Ribbon = 1,
                        SummonerIcon = 1,
                    },
                }
            };

            File.WriteAllText(PATH, Json.Serialize(Configuration));
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
                this.Configuration = Json.Deserialize<Configuration>(File.ReadAllText(PATH));
            }
        }

        public PlayerInformations[] GetPlayersInformations()
        {
            return Configuration.Players.ConvertAll<PlayerInformations>(x => x.GetPlayerInformations()).ToArray();
        }

        public long[] GetPurpleIds()
        {
            return Configuration.Players.FindAll(x => x.Team == TeamId.PURPLE).ConvertAll(x => x.UserId).ToArray();
        }

        public long[] GetBlueIds()
        {
            return Configuration.Players.FindAll(x => x.Team == TeamId.BLUE).ConvertAll(x => x.UserId).ToArray();
        }
    }


}
