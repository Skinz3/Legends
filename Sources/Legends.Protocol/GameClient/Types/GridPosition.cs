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
        public short X;

        public short Y;

        public GridPosition(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }
        public static GridPosition TranslateToGrid(Vector2 inputPosition, Vector2 mapSize, float halfCellSize)
        {
            var X = ((inputPosition.X - mapSize.X) - halfCellSize) / 2f;
            var Y = ((inputPosition.Y - mapSize.Y) - halfCellSize) / 2f;
            return new GridPosition((short)X, (short)Y);
        }
        public static GridPosition[] TranslateToGrid(Vector2[] positions, Vector2 mapSize, float halfCellSize)
        {
            GridPosition[] result = new GridPosition[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                result[i] = TranslateToGrid(positions[i], mapSize, halfCellSize);
            }

            return result;
        }
        public static Vector2 TranslateFromGrid(short X, short Y, Vector2 mapSize)
        {
            return new Vector2(2f * X + mapSize.X, 2f * Y + mapSize.Y);
        }

    }
}
