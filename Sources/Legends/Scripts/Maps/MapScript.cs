using Legends.Core.Cryptography;
using Legends.Core.Protocol.Enum;
using Legends.Core.Utils;
using Legends.Records;
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
    public abstract class MapScript
    {
        protected Logger logger = new Logger();

        private const string SPAWN_EX_STRING = "Unable to spawn {0} on map! : {1}";

        protected Game Game
        {
            get;
            private set;
        }
        public MapScript(Game game)
        {
            this.Game = game;
        }
        public abstract void Spawn();

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

            string fullName = turretName + BuildingManager.TOWER_SUFFIX;

            var teamId = BuildingManager.Instance.GetTeamId(turretName);

            if (teamId != TeamId.UNKNOWN)
            {
                int netId = (int)(BuildingManager.TOWER_NETID_X | CRC32.Compute(Encoding.ASCII.GetBytes(turretName)));
                AITurret turret = new AITurret(netId, objectRecord, aIUnitRecord, BuildingManager.TOWER_SUFFIX);
                turret.DefineGame(Game);
                Game.AddUnit(turret, teamId);
                Game.Map.AddUnit(turret);
            }
            else
            {
                logger.Write(string.Format(SPAWN_EX_STRING, turretName, "Unable to find a team."), MessageState.WARNING);
            }
        }
    }
}
