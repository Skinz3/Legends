using Legends.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public struct GridPosition
    {
        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "What the fuck? working with this offset!!!!!!???????")]
        public const short WTF_OFFSET = 12;

        public short X;

        public short Y;

        public GridPosition(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }
        public static GridPosition TranslateToGrid(Vector2 inputPosition, Vector2 mapSize)
        {
            var X = ((inputPosition.X - mapSize.X) / 2f) - WTF_OFFSET;
            var Y = ((inputPosition.Y - mapSize.Y) / 2f) - WTF_OFFSET;
            return new GridPosition((short)X, (short)Y);
        }
        public static GridPosition[] TranslateToGrid(Vector2[] positions, Vector2 mapSize)
        {
            GridPosition[] result = new GridPosition[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                result[i] = TranslateToGrid(positions[i], mapSize);
            }

            return result;
        }
        public static Vector2 TranslateFromGrid(short X, short Y, Vector2 mapSize)
        {
            return new Vector2(2f * X + mapSize.X, 2f * Y + mapSize.Y);
        }

    }
}
