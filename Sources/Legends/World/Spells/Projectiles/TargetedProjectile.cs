using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.World.Entities.AI;

namespace Legends.World.Spells.Projectiles
{
    public class TargetedProjectile : Projectile
    {
        public TargetedProjectile(uint netId, AIUnit unit, AIUnit target, Vector2 startPosition, float speed, Action onReach) : base(netId, unit, target, startPosition, speed, onReach)
        {

        }

        public override string Name => "name";

        public override bool IsMoving => true;

        public override float PerceptionBubbleRadius => 200;

        public override bool AddFogUpdate => false;// ??

        public override void OnUnitEnterVision(Unit unit)
        {
            throw new NotImplementedException();
        }

        public override void OnUnitLeaveVision(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
