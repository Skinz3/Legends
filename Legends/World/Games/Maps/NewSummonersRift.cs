using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Legends.World.Games.Maps
{   
    public class NewSummonersRift : Map
    {
        public override int Id => 11;

        public override Dictionary<int, Vector2[]> BlueSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(382,395) } },
        };

        public override Dictionary<int, Vector2[]> PurpleSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(14284, 14361) } },
        };

        public NewSummonersRift(Game game) : base(game)
        {

        }

    }
}
