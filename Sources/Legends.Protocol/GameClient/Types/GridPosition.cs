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

        public GridPosition(short x,short y)
        {
            this.X = x;
            this.Y = y;
        }
        public static GridPosition TranslateCenter(Vector2 inputPosition, Vector2 mapSize)
        {
            return new GridPosition((short)((inputPosition.X - mapSize.X) / 2), (short)((inputPosition.Y - mapSize.Y) / 2));
        }
    }
}
