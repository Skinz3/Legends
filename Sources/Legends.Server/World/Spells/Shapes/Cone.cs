using Legends.World.Entities;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Legends.Core.Geometry;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities.AI;

namespace Legends.World.Spells.Shapes
{
    public struct Cone : IShape 
    {
        private float BeginAngle
        {
            get;
            set;
        }
        private float EndAngle
        {
            get;
            set;
        }
        private Vector2 Begin
        {
            get;
            set;
        }
        private Vector2 End
        {
            get;
            set;
        }
        private float Radius
        {
            get;
            set;
        }
        public Cone(Vector2 begin, Vector2 end, float angleDeg)
        {
            Radius = Vector2.Distance(begin, end);

            float middlePointAngle = Geo.GetAngleDegrees(begin, end);

            Begin = begin;
            End = end;
            BeginAngle = middlePointAngle - angleDeg;
            EndAngle = middlePointAngle + angleDeg;

        }
        public bool Collide(AttackableUnit target)
        {
            var unitCoords = target.Position;
            var targetDistance = Vector2.Distance(Begin, unitCoords);
            float targetAngle = Geo.GetAngleDegrees(Begin, unitCoords);
            bool result = targetDistance <= Radius && targetAngle >= BeginAngle && targetAngle <= EndAngle;
            return result;
        }
    }
}
