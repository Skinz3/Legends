using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.Core.Geometry;
using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Types;

namespace Legends.World.Spells.Projectiles
{
    public class TargetedProjectile : Projectile
    {
        public TargetedProjectile(uint netId, AIUnit unit, AttackableUnit target, Vector2 startPosition, float speed, Action onReach)
            : base(netId, unit, target, startPosition, speed, onReach)
        {

        }

        public bool destroy = false;

        public override string Name => Unit.Name + " (Projectile)";

        /// <summary>
        /// Ce n'est pas un mouvement dépendant de PathManager.cs
        /// </summary>
        public override bool IsMoving => false;

        public override bool AddFogUpdate => false;

        public override float PerceptionBubbleRadius => 0;

        public override void Update(float deltaTime)
        {

            if (!destroy)
            {

                float deltaMovement = Speed * 0.001f * deltaTime; // deltaTime

                float angle = Geo.GetAngle(Position, Target.Position);

                float xOffset = (float)Math.Cos(angle) * deltaMovement;
                float yOffset = (float)Math.Sin(angle) * deltaMovement;


                Position = new Vector2(Position.X + xOffset, Position.Y + yOffset);

               // if (Unit is AIHero)
                 //   ((AIHero)Unit).AttentionPing(Position, 0, Protocol.GameClient.Enum.PingTypeEnum.Ping_OnMyWay);


                var distanceToTarget = Target.GetDistanceTo(this);
                if (distanceToTarget  <= deltaMovement)
                {
                    destroy = true;
                    OnReach();
                }
            }
            else
            {
                throw new Exception("What the fuck are you doing");
            }
            base.Update(deltaTime);
        }

        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }

        public override VisibilityData GetVisibilityData()
        {
            throw new NotImplementedException();
        }
    }
}
