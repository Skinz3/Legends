using Legends.Core.Protocol.Enum;
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

        public override Dictionary<int, Vector2[]> BlueSpawns => throw new NotImplementedException();

        public override Dictionary<int, Vector2[]> PurpleSpawns => throw new NotImplementedException();

        public HowlingAbyss(Game game) : base(game)
        {

        }

        protected override MapScript CreateScript(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
