using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.Core.Geometry;

namespace Legends.World.Spells.Projectiles
{
    public class TargetedProjectile
    {
        public uint NetId
        {
            get;
            private set;
        }
        public AIUnit Unit
        {
            get;
            private set;
        }
        public AttackableUnit Target
        {
            get;
            private set;
        }
        public float Speed
        {
            get;
            private set;
        }
        public Action OnReach
        {
            get;
            private set;
        }
        public Vector2 Position
        {
            get;
            set;
        }
        public TargetedProjectile(uint netId, AIUnit unit, AttackableUnit target, Vector2 startPosition, float speed, Action onReach)
        {
            this.NetId = netId;
            this.Unit = unit;
            this.Target = target;
            this.Position = startPosition;
            this.Speed = speed;
            this.OnReach = onReach;

        }

        public bool destroy = false;

        public float GetDistanceTo(Unit other)
        {
            return Geo.GetDistance(Position, other.Position);
        }

        public void Update(long deltaTime)
        {
            Speed = 1200f;

            if (!destroy)
            {

                float deltaMovement = Speed * 0.001f * deltaTime; // deltaTime

                float angle = Geo.GetAngle(Position, Target.Position);

                float xOffset = (float)Math.Cos(angle) * deltaMovement;
                float yOffset = (float)Math.Sin(angle) * deltaMovement;


                Position = new Vector2(Position.X + xOffset, Position.Y + yOffset);

                if (Target is AIHero)
                {
                    //     (Target as AIHero).AttentionPing(Position, NetId, Protocol.GameClient.Enum.PingTypeEnum.Ping_OnMyWay);
                }

                if (this.GetDistanceTo(Target) <= 20f)
                {
                    destroy = true;
                    OnReach();
                }
            }
        }

    }
}
