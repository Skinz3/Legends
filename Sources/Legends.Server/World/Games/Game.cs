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
using Legends.World.Games.Delayed;

namespace Legends.World.Games
{
    public class Game
    {
        private Logger logger = new Logger();

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
        [Obsolete("slow")]
        public AIHero[] Players
        {
            get
            {
                return BlueTeam.Units.Values.OfType<AIHero>().Concat(PurpleTeam.Units.Values.OfType<AIHero>()).ToArray();
            }
        }
        private List<Unit> UnitsToRemove
        {
            get;
            set;
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
        public Team NeutralTeam
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
        private List<DelayedAction> DelayedActions
        {
            get;
            set;
        }
        public Game(int id, string name, int mapId) // Enum MapType, switch -> instance
        {
            this.NetIdProvider = new NetIdProvider();
            this.Id = id;
            this.Name = name;

            this.BlueTeam = new BlueTeam(this);
            this.PurpleTeam = new PurpleTeam(this);
            this.NeutralTeam = new NeutralTeam(this);

            this.Map = Map.CreateMap(mapId, this);

            this.UnitsToRemove = new List<Unit>();
            this.DelayedActions = new List<DelayedAction>();

        }
        /// <summary>
        /// Add player to the game and to his team 
        /// </summary>
        /// <param name="player"></param>
        public void AddUnitToTeam(Unit unit, TeamId teamId)
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
            else if (teamId == TeamId.NEUTRAL)
            {
                unit.DefineTeam(NeutralTeam);
                NeutralTeam.AddUnit(unit);
            }
            else
            {
                throw new Exception("Unknown team...(" + teamId + ")!");
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
                var purple = PurpleTeam.GetUnit<T>(predicate);

                if (purple == null)
                {
                    return NeutralTeam.GetUnit<T>(predicate);
                }
                else
                {
                    return purple;
                }
            }

        }
        /// <summary>
        /// Delay in seconds
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        public DelayedAction Action(Action action, float delay)
        {
            DelayedAction dAction = new DelayedAction(action, delay * 1000);
            DelayedActions.Add(dAction);
            return dAction;
        }
        public void DestroyUnit(Unit unit)
        {
            UnitsToRemove.Add(unit);
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
            BlueTeam.Send(message, channel, flags);
            PurpleTeam.Send(message, channel, flags);
            //NeutralTeam.Send(message, channel, flags);
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
            NeutralTeam.Initialize();

            Map.Script.OnUnitsInitialized();

            Send(new StartSpawnMessage());


            foreach (var unit in Map.Units.OfType<AIUnit>())
            {
                unit.Create();
            }

            foreach (var building in Map.Units.OfType<Building>())
            {
                building.UpdateHeath();
                building.UpdateStats(false);
            }

            BlueTeam.InitializeFog();
            PurpleTeam.InitializeFog();
            NeutralTeam.InitializeFog();

            Send(new EndSpawnMessage());



        }
        public void Start()
        {

            foreach (var player in Players)
            {
                player.Team.Send(new OnEnterVisiblityClientMessage(player.NetId, player.GetVisibilityData()));
            }

            float gameTime = GameTime / 1000f;

            Send(new GameTimerMessage(0, gameTime));
            Send(new GameTimerUpdateMessage(0, gameTime));

            foreach (var unit in Map.Units)
            {
                unit.OnGameStart();
            }

            Map.Script.OnStart();
            Send(new StartGameMessage(0));
            this.Started = true;




        }

        public void Update(float deltaTime)
        {
            if (NextSyncTime >= 10 * 1000)
            {
                Send(new GameTimerMessage(0, GameTime / 1000f));
                NextSyncTime = 0;
            }

            BlueTeam.Update(deltaTime);
            PurpleTeam.Update(deltaTime);
            NeutralTeam.Update(deltaTime);
            Map.Update(deltaTime);


            DelayedActions.RemoveAll(x => x.Finalized);

            foreach (var action in DelayedActions)
            {
                action.Update(deltaTime);
            }

            foreach (var unit in UnitsToRemove)
            {
                Map.RemoveUnit(unit);
                unit.Team.RemoveUnit(unit);
                unit.DefineTeam(null);
                unit.DefineGame(null);
            }

            UnitsToRemove.Clear();

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