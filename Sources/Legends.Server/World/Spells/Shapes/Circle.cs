using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;

namespace Legends.World.Spells.Shapes
{
    public class Circle : IShape
    {
        public Vector2 StartPosition
        {
            get;
            private set;
        }
        public float Radius
        {
            get;
            private set;
        }
        public Circle(Vector2 startPosition, float radius)
        {
            this.StartPosition = startPosition;
            this.Radius = radius;
        }
        public bool Collide(AttackableUnit target)
        {
            return Vector2.Distance(target.Position, StartPosition) <= Radius;
        }
    }
}
