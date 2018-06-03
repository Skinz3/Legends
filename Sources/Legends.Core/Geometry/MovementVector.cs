using System;
using System.Collections.Generic;
using System.IO;
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
        public static byte[] EncodeWaypoints(Vector2[] waypoints, Vector2 mapSize)
        {
            var numCoords = waypoints.Length * 2;

            var maskBytes = new byte[((numCoords - 3) / 8) + 1];
            var tempStream = new MemoryStream();
            var resultStream = new MemoryStream();
            var tempBuffer = new BinaryWriter(tempStream);
            var resultBuffer = new BinaryWriter(resultStream);

            var lastCoord = new Vector2();
            var coordinate = 0;
            foreach (var waypoint in waypoints)
            {
                var curVector = new Vector2((waypoint.X - mapSize.X) / 2, (waypoint.Y - mapSize.Y) / 2);
                if (coordinate == 0)
                {
                    tempBuffer.Write((short)curVector.X);
                    tempBuffer.Write((short)curVector.Y);
                }
                else
                {
                    var relative = new Vector2(curVector.X - lastCoord.X, curVector.Y - lastCoord.Y);
                    var isAbsolute = IsAbsolute(relative);

                    if (isAbsolute.Key)
                        tempBuffer.Write((short)curVector.X);
                    else
                        tempBuffer.Write((byte)relative.X);

                    if (isAbsolute.Value)
                        tempBuffer.Write((short)curVector.Y);
                    else
                        tempBuffer.Write((byte)relative.Y);

                    SetBitmaskValue(ref maskBytes, coordinate - 2, !isAbsolute.Key);
                    SetBitmaskValue(ref maskBytes, coordinate - 1, !isAbsolute.Value);
                }
                lastCoord = curVector;
                coordinate += 2;
            }
            if (numCoords > 2)
            {
                resultBuffer.Write(maskBytes);
            }
            resultBuffer.Write(tempStream.ToArray());

            return resultStream.ToArray();
        }
        public static KeyValuePair<bool, bool> IsAbsolute(Vector2 vec)
        {
            return new KeyValuePair<bool, bool>(vec.X < sbyte.MinValue || vec.X > sbyte.MaxValue, vec.Y < sbyte.MinValue || vec.Y > sbyte.MaxValue);
        }
        public static void SetBitmaskValue(ref byte[] mask, int pos, bool val)
        {
            if (val)
                mask[pos / 8] |= (byte)(1 << (pos % 8));
            else
                mask[pos / 8] &= (byte)(~(1 << (pos % 8)));
        }
    }
}
