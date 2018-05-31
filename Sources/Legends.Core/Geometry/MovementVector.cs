using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Geometry
{
    public class MovementVector
    {
        public short X
        {
            get;
            private set;
        }
        public short Y
        {
            get;
            private set;
        }

        public Vector2 MiddleOfMap
        {
            get;
            private set;
        }

        public MovementVector(short x, short y, Vector2 middleOfMap)
        {
            this.X = x;
            this.Y = y;
        }

        public static short FormatCoordinate(float coordinate, float origin)
        {
            return (short)((coordinate - origin) / 2f);
        }

        public static short TargetXToNormalFormat(float value, Vector2 middleOfMap)
        {
            return FormatCoordinate(value, middleOfMap.X);
        }

        public static short TargetYToNormalFormat(float value, Vector2 middleOfMap)
        {
            return FormatCoordinate(value, middleOfMap.Y);
        }
    }
}
