using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells.Projectiles
{
    public abstract class Projectile : Unit
    {
        protected AIUnit Unit
        {
            get;
            private set;
        }
        protected AttackableUnit Target
        {
            get;
            private set;
        }
        protected Action OnReach
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
        public Projectile(uint netId, AIUnit unit, AttackableUnit target, Vector2 startPosition, float speed, Action onReach) : base(netId)
        {
            this.Unit = unit;
            this.Target = target;
            this.OnReach = onReach;
            this.StartPosition = startPosition;
            this.Position = startPosition;
            this.Speed = speed;
        }
    }
}
