using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Legends.Core.Protocol.Enum;
using Legends.Scripts.Maps;

namespace Legends.World.Games.Maps
{
    public class SummonersRiftUpdated : Map
    {
        public override MapIdEnum Id => MapIdEnum.SummonersRiftUpdated;

        public override Dictionary<int, Vector2[]> BlueSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(382,395) } },
             {2, new Vector2[]{ new Vector2(382,395), new Vector2(400, 395) } }
        };

        public override Dictionary<int, Vector2[]> PurpleSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(14284, 14361) } },
        };

        public SummonersRiftUpdated(Game game) : base(game)
        {

        }

        protected override MapScript CreateScript(Game game)
        {
            return new SummonersRiftUpdatedScript(game);
        }
    }
}
