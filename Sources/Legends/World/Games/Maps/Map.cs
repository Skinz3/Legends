using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Legends.World.Entities;
using Legends.Protocol.GameClient.Enum;
using System.Diagnostics;
using System.Numerics;
using Legends.Records;
using Legends.World.Entities.AI;
using Legends.Scripts.Maps;

namespace Legends.World.Games.Maps
{
    public abstract class Map
    {
        public abstract MapIdEnum Id
        {
            get;
        }
        public MapRecord Record
        {
            get;
            private set;
        }
        public Vector2 Size
        {
            get
            {
                return new Vector2(Record.Width / 2, Record.Height / 2);
            }
        }

        public Unit GetUnit(uint targetNetId)
        {
            return Units.FirstOrDefault(x => x.NetId == targetNetId);
        }

        public List<Unit> Units
        {
            get;
            private set;
        }
      
        /// <summary>
        /// int is team size and Vector2 are positions ([0] = first player, [1] = second, etc)
        /// todo = put this in MapScript.cs
        /// </summary>
        public abstract Dictionary<int, Vector2[]> BlueSpawns
        {
            get;
        }
        public abstract Dictionary<int, Vector2[]> PurpleSpawns
        {
            get;
        }

        private Game Game
        {
            get;
            set;
        }
        public MapScript Script
        {
            get;
            private set;
        }
        public Map(Game game)
        {
            Record = MapRecord.GetMap((int)Id);
            Script = CreateScript(game);
            Game = game;
            Units = new List<Unit>();
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var unit in Units.ToArray())
            {
                unit.Update(deltaTime);
            }
         
            Script.Update(deltaTime);
        }
        public void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }
        public Vector2 GetSpawnPosition(AIHero player)
        {
            int teamSize = player.Team.Size;
            int teamIndex = player.TeamNo - 1;
            return player.Team.Id == TeamId.BLUE ? BlueSpawns[teamSize][teamIndex] : PurpleSpawns[teamSize][teamIndex];
        }
        protected MapScript CreateScript(Game game)
        {
            return MapScriptsManager.Instance.GetMapScript(Id, game);
        }

        public static Map CreateMap(int id, Game game)
        {
            switch (id)
            {
                case 1: // summonersrift
                    return new SummonersRift(game);
                case 12: // howling abyss
                    return new HowlingAbyss(game);
                case 11: // summonersrift , reworked
                    return new SummonersRiftUpdated(game);
            }

            throw new Exception("Cannot define map...");
        }
    }
}
