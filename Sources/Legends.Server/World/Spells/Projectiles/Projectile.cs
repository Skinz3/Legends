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
        public AIUnit Unit
        {
            get;
            private set;
        }
        public AIUnit Target
        {
            get;
            private set;
        }
        public Action OnReach
        {
            get;
            private set;
        }
        public Vector2 StartPosition
        {
            get;
            private set;
        }
        public float Speed
        {
            get;
            private set;
        }
        public Projectile(uint netId, AIUnit unit, AIUnit target, Vector2 startPosition, float speed, Action onReach) : base(netId)
        {
            this.Unit = unit;
            this.Target = target;
            this.OnReach = onReach;
        }
    }
}
