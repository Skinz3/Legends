using ENet;
using Legends.Core.Cryptography;
using Legends.Core.IO.MOB;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Network;
using Legends.Records;
using Legends.World.Buildings;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Entities.Buildings;
using Legends.World.Games.Maps;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Legends.Protocol.GameClient.LoadingScreen;

namespace Legends.World.Games
{
    public class Game
    {
        private Logger logger = new Logger();

        /// <summary>
        /// Theorically 30fps
        /// Aprox equal to (16.666 * 2)
        /// </summary>
        public const double REFRESH_RATE = (1000d / 30d);

        public NetIdProvider NetIdProvider
        {
            get;
            private set;
        }
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
        public AIHero[] Players
        {
            get
            {
                return BlueTeam.Units.Values.OfType<AIHero>().Concat(PurpleTeam.Units.Values.OfType<AIHero>()).ToArray();
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
        public bool CanSpawn
        {
            get
            {
                return Players.All(x => x.ReadyToSpawn);
            }
        }
        public bool CanStart
        {
            get
            {
                return Players.All(x => x.ReadyToStart);
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
        private HighResolutionTimer Timer
        {
            get;
            set;
        }
        private Stopwatch Stopwatch
        {
            get;
            set;
        }
        public float GameTime
        {
            get;
            private set;
        }
        public float GameTimeSeconds
        {
            get
            {
                return GameTime / 1000f;
            }
        }
        public float GameTimeMinutes
        {
            get
            {
                return GameTimeSeconds / 60f;
            }
        }
        private double NextSyncTime
        {
            get;
            set;
        }
        /// <summary>
        /// ConcurrentStack<T> is a threadsafe object.
        /// </summary>
        public ConcurrentStack<Action> SynchronizedActions
        {
            get;
            private set;
        }
        public Game(int id, string name, int mapId) // Enum MapType, switch -> instance
        {
            this.NetIdProvider = new NetIdProvider();
            this.Id = id;
            this.Name = name;
            this.BlueTeam = new Team(this, TeamId.BLUE);
            this.PurpleTeam = new Team(this, TeamId.PURPLE);
            this.Map = Map.CreateMap(mapId, this);
            this.Timer = new HighResolutionTimer((int)REFRESH_RATE);
            this.SynchronizedActions = new ConcurrentStack<Action>();
        }
        public void Invoke(Action action)
        {
            SynchronizedActions.Push(action);
        }
        /// <summary>
        /// Add player to the game and to his team 
        /// </summary>
        /// <param name="player"></param>
        public void AddUnit(Unit unit, TeamId teamId)
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
        }
        public T GetUnit<T>(string name) where T : Unit
        {
            return GetUnit<T>(x => x.Name == name);
        }
        public T GetUnit<T>(uint netId) where T : Unit
        {
            return GetUnit<T>(x => x.NetId == netId);
        }
        public T GetUnit<T>(Func<T, bool> predicate) where T : Unit
        {
            var blue = BlueTeam.GetUnit<T>(predicate);

            if (blue != null)
            {
                return blue;
            }
            else
            {
                return PurpleTeam.GetUnit<T>(predicate);
            }

        }
        public void RemoveUnit(Unit unit)
        {
            unit.Team.RemoveUnit(unit);
        }
        public bool Contains(long userId)
        {
            return Players.FirstOrDefault(x => x.Client.UserId == userId) != null;
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
        public void Spawn()
        {
            foreach (AIHero player in Players)
            {
                Map.AddUnit(player);
            }


            Map.Script.OnSpawn();
            Map.Script.CreateBindings();

            BlueTeam.Initialize();
            PurpleTeam.Initialize();

            Map.Script.OnUnitsInitialized();

            Send(new StartSpawnMessage());
            foreach (var player in Map.Units.OfType<AIHero>())
            {
                Send(new HeroSpawnMessage(player.NetId, player.PlayerNo, player.Data.TeamId, player.SkinId, player.Data.Name,
                    player.Model));

                player.UpdateInfos();
                player.UpdateStats(false);
                player.UpdateHeath();
            }

            foreach (var turret in Map.Units.OfType<AITurret>())
            {
                Send(new TurretSpawnMessage(0, turret.NetId, turret.GetClientName()));
                turret.UpdateStats(false);
                turret.UpdateHeath();
            }
            foreach (var building in Map.Units.OfType<Building>())
            {
                building.UpdateHeath();
                building.UpdateStats(false);
            }

            BlueTeam.InitializeFog();
            PurpleTeam.InitializeFog();

            Send(new EndSpawnMessage());



        }
        public void Start()
        {
            foreach (var player in Players)
            {
                player.Team.Send(new EnterVisionMessage(true, player.NetId, player.Position, player.PathManager.WaypointsIndex, player.PathManager.GetWaypoints(), player.Game.Map.Record.MiddleOfMap));
            }

            float gameTime = GameTime / 1000f;

            Send(new GameTimerMessage(0, gameTime));
            Send(new GameTimerUpdateMessage(0, gameTime));
            StartCallback();
            this.Started = true;
            Map.Script.OnStart();
            Send(new StartGameMessage(0));
        }
        public void StartCallback()
        {
            Stopwatch = Stopwatch.StartNew();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private void Timer_Elapsed()
        {
            long deltaTime = Stopwatch.ElapsedMilliseconds;

            if (deltaTime > 0)
            {
                GameTime += deltaTime;
                NextSyncTime += deltaTime;

                Console.Title = "Legends (FPS :" + 1000 / deltaTime + ")";

                Update(deltaTime);
                Stopwatch = Stopwatch.StartNew();
            }
        }
        private void Update(long deltaTime)
        {
            if (NextSyncTime >= 10 * 1000)
            {
                Send(new GameTimerMessage(0, GameTime / 1000f));
                NextSyncTime = 0;
            }
            foreach (var action in SynchronizedActions)
            {
                action();
            }
            SynchronizedActions.Clear();

            BlueTeam.Update(deltaTime);
            PurpleTeam.Update(deltaTime);
            Map.Update(deltaTime);
        }
        public void Announce(AnnounceEnum announce)
        {
            Send(new AnnounceMessage(0, (int)Map.Id, announce));
        }
        public void UnitAnnounce(UnitAnnounceEnum announce, uint netId, uint sourceNetId, uint[] assitsNetId)
        {
            Send(new UnitAnnounceMessage(netId, announce, sourceNetId, assitsNetId));
        }



    }
}
