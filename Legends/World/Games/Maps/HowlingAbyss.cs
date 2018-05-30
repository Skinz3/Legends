using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games.Maps
{
    public class HowlingAbyss : Map
    {
        public override int Id => 12;

        public override Dictionary<int, Vector2[]> BlueSpawns => throw new NotImplementedException();

        public override Dictionary<int, Vector2[]> PurpleSpawns => throw new NotImplementedException();

        public HowlingAbyss(Game game) : base(game)
        {

        }
    }
}
