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
    public class SummonersRift : Map
    {
        public override MapIdEnum Id => MapIdEnum.SummonersRift;

        public override Dictionary<int, Vector2[]> BlueSpawns => new Dictionary<int, Vector2[]>
        {
            {1, new Vector2[] {  new Vector2(26,280) } },
            {2, new Vector2[] {  new Vector2(-79, 379), new Vector2(143,171) } },
            {3, new Vector2[] {  new Vector2(-1, 433),  new Vector2(185,229), new Vector2(-80, 177) } },
            {4, new Vector2[] {  new Vector2(-79,395),  new Vector2(147,393), new Vector2(149, 165),new Vector2(-73,165) } },
            {5, new Vector2[] {  new Vector2(-53,433) } },
            {6, new Vector2[] {  new Vector2(-53,433) } },
        };

        public override Dictionary<int, Vector2[]> PurpleSpawns => new Dictionary<int, Vector2[]>
        {
            {1, new Vector2[] {  new Vector2(13927, 14175) } },
            {2, new Vector2[] {  new Vector2(-53,433) } },
            {3, new Vector2[] {  new Vector2(-53,433) } },
            {4, new Vector2[] {  new Vector2(-53,433) } },
            {5, new Vector2[] {  new Vector2(-53,433) } },
            {6, new Vector2[] {  new Vector2(-53,433) } },
        };

        public SummonersRift(Game game) : base(game)
        {

        }

        protected override MapScript CreateScript(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
