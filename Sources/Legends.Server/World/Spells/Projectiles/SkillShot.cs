using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Types;
using Legends.World.Entities;
using Legends.World.Entities.AI;

namespace Legends.World.Spells.Projectiles
{
    public class SkillShot : Projectile
    {
        private Vector2 Direction
        {
            get;
            set;
        }
        private float CollisionRadius
        {
            get;
            set;
        }
        private float Range
        {
            get;
            set;
        }
        private Action<SkillShot> OnRangeReached
        {
            get;
            set;
        }
        private List<AttackableUnit> Hitten
        {
            get;
            set;
        }
        public SkillShot(uint netId, AIUnit unit, Vector2 startPosition, float speed, float collisionRadius, Action<AttackableUnit, Projectile> onReach, Vector2 direction, float range, Action<SkillShot> onRangedReached) : base(netId, unit, startPosition, speed, onReach)
        {
            this.Direction = direction;
            this.Range = range;
            this.OnRangeReached = onRangedReached;
            this.CollisionRadius = collisionRadius;
            this.Hitten = new List<AttackableUnit>();
        }

        public override string Name => NetId + " (SkillShot)";

        public override bool IsMoving => false;

        public override bool AddFogUpdate => true;

        public override float PerceptionBubbleRadius => 500f;

        public override void Update(float deltaTime)
        {
            float deltaMovement = Speed * 0.001f * deltaTime; // deltaTime

            float xOffset = Direction.X * deltaMovement;
            float yOffset = Direction.Y * deltaMovement;

            Position = new Vector2(Position.X + xOffset, Position.Y + yOffset);

            foreach (var team in Unit.Team.GetOposedTeams())
            {
                foreach (var target in team.AliveUnits.OfType<AttackableUnit>())
                {
                    if (Hitten.Contains(target) == false)
                    {
                        if (Geo.GetDistance(Position, target.Position) <= CollisionRadius + (target.PathfindingCollisionRadius * target.Stats.ModelSize.TotalSafe))
                        {
                            Hitten.Add(target);
                            OnReach(target, this);
                        }
                    }
                }
            }
            base.Update(deltaTime);

            if (Geo.GetDistance(Position, StartPosition) >= Range)
            {
                OnRangeReached(this);
            }
        }

        public int GetHittenCount()
        {
            return Hitten.Count;
        }
        public override VisibilityData GetVisibilityData()
        {
            return new VisibilityDataSpellMissile()
            {

            };
        }
        [InDevelopment]
        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }
    }
}
