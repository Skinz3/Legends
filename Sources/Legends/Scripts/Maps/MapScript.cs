using Legends.Core.Cryptography;
using Legends.Core.CSharp;
using Legends.Core.Protocol.Enum;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Records;
using Legends.World;
using Legends.World.Buildings;
using Legends.World.Entities.AI;
using Legends.World.Games;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Maps
{
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
        public MapScript(Game game)
        {
            this.Game = game;
            this.DelayedActions = new List<KeyValuePair<Action, float>>();
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract void OnSpawn();

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
        protected void SpawnAITurret(string turretName, string aiUnitRecordName)
        {
            AIUnitRecord aIUnitRecord = AIUnitRecord.GetAIUnitRecord(aiUnitRecordName);

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
                int netId = (int)(BuildingProvider.TOWER_NETID_X | CRC32.Compute(Encoding.ASCII.GetBytes(fullName)));
                AITurret turret = new AITurret(netId, aIUnitRecord, objectRecord, BuildingProvider.TOWER_SUFFIX);
                turret.DefineGame(Game);
                Game.AddUnit(turret, teamId);
                Game.Map.AddUnit(turret);
            }
            else
            {
                logger.Write(string.Format(SPAWN_EX_STRING, turretName, "Unable to find a team."), MessageState.WARNING);
            }
        }

        public void Update(long deltaTime)
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
