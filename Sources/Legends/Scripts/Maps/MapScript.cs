﻿using Legends.Core.Cryptography;
using Legends.Core.CSharp;
using Legends.Protocol.GameClient.Enum;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Records;
using Legends.World;
using Legends.World.Buildings;
using Legends.World.Entities.AI;
using Legends.World.Entities.Buildings;
using Legends.World.Games;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Legends.Scripts.Maps
{
    public struct BindingDescription
    {
        public string[] UnitsThatProtect;

        public string[] UnitsProtected;

        public bool AllDead;

        public BindingDescription(string[] unitsThatProtect, string[] unitsProtected, bool alldead)
        {
            this.UnitsThatProtect = unitsThatProtect;
            this.UnitsProtected = unitsProtected;
            this.AllDead = alldead;
        }
    }
    public abstract class MapScript : Script, IUpdatable
    {
        protected Logger logger = new Logger();

        private const string SPAWN_EX_STRING = "Unable to spawn {0} on map! : {1}";

        protected Game Game
        {
            get;
            private set;

        }
        private List<KeyValuePair<Action, float>> DelayedActions
        {
            get;
            set;
        }
        private List<BindingDescription> Bindings
        {
            get;
            set;
        }
        private MapBindingManager BindingManager
        {
            get;
            set;
        }
        public abstract bool RecallAllowed
        {
            get;
        }
        public abstract float GoldsPerSeconds
        {
            get;
        }
        public MapScript(Game game)
        {
            this.Game = game;
            this.DelayedActions = new List<KeyValuePair<Action, float>>();
            this.Bindings = new List<BindingDescription>();
        }
        public void AddBinding(string[] sources, string[] targets, bool allDead)
        {
            this.Bindings.Add(new BindingDescription(sources, targets, allDead));
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract void OnSpawn();

        public virtual void OnUnitsInitialized()
        {
            this.BindingManager = new MapBindingManager(Game, Bindings);
            BindingManager.Initialize();
        }

        public abstract void CreateBindings();

        /// <summary>
        /// GameTime = 0
        /// </summary>
        public abstract void OnStart();
        /// <summary>
        /// Delay = seconds
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        protected void Action(Action action, float delay)
        {
            DelayedActions.Add(new KeyValuePair<Action, float>(action, delay));
        }

        protected void Announce(AnnounceEnum announce, float delay)
        {
            Action(new Action(() => { Game.Announce(announce); }), delay);
        }
        protected void SpawnNexus(string name)
        {
            uint netId = BuildingProvider.BUILDING_NETID_X | CRC32.Compute(Encoding.ASCII.GetBytes(name));
            Nexus nexus = new Nexus(netId, BuildingRecord.GetBuildingRecord(this.Game.Map.Id, name), Game.Map.Record.GetObject(name));
            nexus.DefineGame(Game);
            Game.AddUnitToTeam(nexus, BuildingProvider.Instance.GetTeamId(name));
            Game.Map.AddUnit(nexus);
        }
        protected void SpawnInhibitor(string name)
        {
            uint netId = BuildingProvider.BUILDING_NETID_X | CRC32.Compute(Encoding.ASCII.GetBytes(name));
            Inhibitor inhibitor = new Inhibitor(netId, BuildingRecord.GetBuildingRecord(this.Game.Map.Id, name), Game.Map.Record.GetObject(name));
            inhibitor.DefineGame(Game);
            Game.AddUnitToTeam(inhibitor, BuildingProvider.Instance.GetTeamId(name));
            Game.Map.AddUnit(inhibitor);
        }

        protected AIUnit GetAIUnit(string name)
        {
            return Game.GetUnit<AIUnit>(name);
        }
        protected void SpawnMonster(string name, Vector2 position, int delay)
        {
            uint netId = Game.NetIdProvider.Pop();
            AIUnitRecord record = AIUnitRecord.GetAIUnitRecord(name);
            AIMonster monster = new AIMonster(netId, record, delay);
            monster.SpawnPosition = position;
            monster.Position = position;
            monster.DefineGame(Game);
            Game.AddUnitToTeam(monster, TeamId.NEUTRAL);
            Game.Map.AddUnit(monster);
        }
        protected void SpawnAITurret(string turretName, string aiUnitRecordName, string customAIUnitRecord = null)
        {
            AIUnitRecord aIUnitRecord = AIUnitRecord.GetAIUnitRecord(customAIUnitRecord != null ? customAIUnitRecord : aiUnitRecordName);

            MapObjectRecord objectRecord = Game.Map.Record.GetObject(turretName);

            if (objectRecord == null)
            {
                logger.Write(string.Format(SPAWN_EX_STRING, turretName, "the GameObjectRecord do not exist."), MessageState.WARNING);
                return;
            }
            if (aIUnitRecord == null)
            {
                logger.Write(string.Format(SPAWN_EX_STRING, turretName, "the AIUnitRecord do not exist."), MessageState.WARNING);
                return;
            }

            string fullName = turretName + BuildingProvider.TOWER_SUFFIX;

            var teamId = BuildingProvider.Instance.GetTeamId(turretName);

            if (teamId != TeamId.UNKNOWN)
            {
                uint netId = (uint)(BuildingProvider.BUILDING_NETID_X | CRC32.Compute(Encoding.ASCII.GetBytes(fullName)));
                AITurret turret = new AITurret(netId, aIUnitRecord, objectRecord, BuildingRecord.GetBuildingRecord(Game.Map.Id, turretName), BuildingProvider.TOWER_SUFFIX);
                turret.DefineGame(Game);
                Game.AddUnitToTeam(turret, teamId);
                Game.Map.AddUnit(turret);

                if (customAIUnitRecord != null)
                {
                    turret.AddStackData(customAIUnitRecord, 0, false, true, true, false);
                }
            }
            else
            {
                logger.Write(string.Format(SPAWN_EX_STRING, turretName, "Unable to find a team."), MessageState.WARNING);
            }
        }
        public void Update(float deltaTime)
        {
            if (DelayedActions.Count > 0)
            {
                var pairs = DelayedActions.FindAll(x => x.Value <= Game.GameTimeSeconds);

                foreach (var pair in pairs)
                {
                    pair.Key();
                    DelayedActions.Remove(pair);
                }
            }
        }
    }
}
