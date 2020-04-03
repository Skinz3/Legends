using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells.Projectiles
{
    public abstract class Projectile : Unit, IShape
    {
        protected AIUnit Unit
        {
            get;
            private set;
        }
        protected Action<AttackableUnit, Projectile> OnReach
        {
            get;
            private set;
        }
        protected Vector2 StartPosition
        {
            get;
            private set;
        }
        protected float Speed
        {
            get;
            private set;
        }
        public Projectile(uint netId, AIUnit unit, Vector2 startPosition, float speed, Action<AttackableUnit, Projectile> onReach) : base(netId)
        {
            this.Unit = unit;
            this.OnReach = onReach;
            this.StartPosition = startPosition;
            this.Position = startPosition;
            this.Speed = speed;
        }

        public abstract bool Collide(AttackableUnit target);

    }
}
