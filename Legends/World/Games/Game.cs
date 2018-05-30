using ENet;
using Legends.Core.Protocol;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.LoadingScreen;
using Legends.Core.Protocol.Messages.Game;
using Legends.Core.Protocol.Other;
using Legends.Network;
using Legends.World.Entities;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Legends.World.Games
{
    public class Game
    {
        public const double REFRESH_RATE = 1000 / 60; // 60fps

        public int Id
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
        public Player[] Players
        {
            get
            {
                return BlueTeam.Units.Values.OfType<Player>().Concat(PurpleTeam.Units.Values.OfType<Player>()).ToArray();
            }
        }
        public Team BlueTeam
        {
            get;
            private set;
        }
        public Team PurpleTeam
        {
            get;
            private set;
        }
        private int LastPlayerNo
        {
            get;
            set;
        }
        public bool CanStart
        {
            get
            {
                return Players.All(x => x.ReadyToSpawn);
            }
        }
        public bool Started
        {
            get;
            private set;
        }
        public Map Map
        {
            get;
            private set;
        }
        private Timer Timer
        {
            get;
            set;
        }
        private Stopwatch Stopwatch
        {
            get;
            set;
        }
        private double GameTime
        {
            get;
            set;
        }
        public Game(int id, string name, int mapId) // Enum MapType, switch -> instance
        {
            this.BlueTeam = new Team(this, TeamId.BLUE);
            this.PurpleTeam = new Team(this, TeamId.PURPLE);
            this.Id = id;
            this.Name = name;
            this.Map = Map.CreateMap(mapId, this);
            this.Timer = new Timer(REFRESH_RATE);
        }
        /// <summary>
        /// Add player to the game and to his team (using Player.Datas)
        /// </summary>
        /// <param name="player"></param>
        public void AddUnit(Unit unit,TeamId teamId)
        {
            if (teamId == TeamId.BLUE)
            {
                unit.DefineTeam(BlueTeam);
                BlueTeam.AddUnit(unit);

            }
            else if (teamId == TeamId.PURPLE)
            {
                unit.DefineTeam(PurpleTeam);
                PurpleTeam.AddUnit(unit);
            }
            unit.Initialize();
        }
        public int PopNextPlayerNo()
        {
            return LastPlayerNo++;
        }
        public void Send(Message message, Channel channel = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            foreach (var player in Players)
            {
                player.Client.Send(message, channel, flags);
            }
        }
        public void Start()
        {
            Spawn();

            foreach (var player in Players)
            {
                Map.AddUnit(player);
            }

            StartCallback();
            this.Started = true;
        }
        public void StartCallback()
        {
            Stopwatch = Stopwatch.StartNew();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GameTime += Stopwatch.ElapsedMilliseconds;
            Update(Stopwatch.ElapsedMilliseconds);
            Stopwatch = Stopwatch.StartNew();
        }
        private void Update(long deltaTime)
        {
            Console.Title = "Legends FPS: " + deltaTime;
            BlueTeam.Update(deltaTime);
            PurpleTeam.Update(deltaTime);
            Map.Update(deltaTime);
        }
        public void Announce(AnnounceEnum announce)
        {
            Send(new AnnounceMessage(0, Map.Id, announce));
        }
        private void Spawn()
        {
            Send(new StartSpawnMessage());

            foreach (var player in Players)
            {
                Send(new HeroSpawnMessage(player.NetId, player.PlayerNo, player.Data.TeamId, player.Data.SkinId, player.Data.Name,
                    player.Data.ChampionName));

                player.UpdateInfos();
                player.UpdateStats(false);
                player.UpdateHeath();

                Send(new TurretSpawnMessage(NetIdProvider.PopNextNetId(), "@Turret_T1_R_03_A"));
                Send(new TurretSpawnMessage(NetIdProvider.PopNextNetId(), "@Turret_T1_L_03_A"));
            }

            Send(new EndSpawnMessage());
        }
    }
}
