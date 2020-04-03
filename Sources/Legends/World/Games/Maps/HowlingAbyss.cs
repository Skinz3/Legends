using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Scripts.Maps;

namespace Legends.World.Games.Maps
{
    public class HowlingAbyss : Map
    {
        public override MapIdEnum Id => MapIdEnum.HowlingAbyss;

        public override Dictionary<int, Vector2[]> BlueSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(963,1100) } },
        };

        public override Dictionary<int, Vector2[]> PurpleSpawns => new Dictionary<int, Vector2[]>()
        {
             {1, new Vector2[] {  new Vector2(11649,11347) } },
        };

        public HowlingAbyss(Game game) : base(game)
        {

        }

    }
}
