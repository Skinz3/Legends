using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Geometry
{
    public static class Geo
    {
        public static float GetDistance(Vector2 p1, Vector2 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
        public static Vector2 GetDirection(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float GetAngle(Vector2 p1, Vector2 p2)
        {
            return (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        }
        public static Vector2 GetPointOnCircle(Vector2 centerPoint, float angle, float distance)
        {
            return new Vector2((float)(centerPoint.X + (distance * Math.Cos(angle))), (float)(centerPoint.Y + (distance * Math.Sin(angle))));
        }
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.X, vector.Y, 0);
        }
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        public static Vector3 ForceSize(this Vector3 vector, int size)
        {
            if (size <= 2)
            {
                vector.Z = 0f;
            }
            if (size <= 1)
            {
                vector.Y = 0f;
            }
            if (size == 0)
            {
                vector.X = 0f;
            }
            return vector;
        }
    }
}
