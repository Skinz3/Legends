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
        public AIUnit Target
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
        public TargetedProjectile(uint netId, AIUnit unit, AIUnit target, Vector2 startPosition, float speed, Action onReach)
        {
            this.NetId = netId;
            this.Unit = unit;
            this.Target = target;
            this.Position = startPosition;
            this.Speed = speed;
            this.OnReach = onReach;
            Speed = 1200f * Unit.Stats.AttackSpeed.TotalSafe;
        }


        public bool destroy = false;

        public float GetDistanceTo(Unit other)
        {
            return Geo.GetDistance(Position, other.Position);
        }
        int test = 0;
        public void Update(long deltaTime)
        {
            if (!destroy)
            {
                float deltaMovement = Speed * 0.001f * deltaTime; // deltaTime


                float xOffset = 1 * deltaMovement;
                float yOffset = 1 * deltaMovement;

                Position = new Vector2(Position.X + xOffset, Position.Y + yOffset);
                test++;

                if (Target is AIHero)
                {
                    if (test >= 5)
                    {
                  //      (Target as AIHero).AttentionPing(Position, NetId, Protocol.GameClient.Enum.PingTypeEnum.Ping_OnMyWay);
                        test = 0;
                    }
                }
                if (this.GetDistanceTo(Target) <= 100)
                {
                    destroy = true;
                    OnReach();
                }
            }
        }

    }
}
