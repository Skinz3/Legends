using Legends.Core.Protocol.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Legends.World.Entities;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.LoadingScreen;
using System.Diagnostics;
using System.Numerics;
using Legends.Records;
using Legends.World.Entities.AI;

namespace Legends.World.Games.Maps
{
    public abstract class Map
    {

        public abstract int Id
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

        public Unit GetUnit(int targetNetId)
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
        public Map(Game game)
        {
            Record = MapRecord.GetMap(Id);

            Game = game;
            Units = new List<Unit>();
        }




        public virtual void Update(long deltaTime)
        {
            //    Console.Title = deltaTime.ToString();
            foreach (var unit in Units)
            {
                unit.Update(deltaTime);
            }
        }
        public void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }
        public Vector2 GetStartPosition(AIHero player)
        {
            int teamSize = player.Team.Size;
            int teamIndex = player.TeamNo - 1;
            return player.Team.Id == TeamId.BLUE ? BlueSpawns[teamSize][teamIndex] : PurpleSpawns[teamSize][teamIndex];
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
                    return new NewSummonersRift(game);
            }

            throw new Exception("Cannot define map...");
        }
    }
}
