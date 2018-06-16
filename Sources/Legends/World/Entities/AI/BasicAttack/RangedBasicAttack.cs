using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;
using Legends.Core.DesignPattern;
using Legends.World.Spells.Projectiles;

namespace Legends.World.Entities.AI.BasicAttack
{
    [InDevelopment(InDevelopmentState.TODO, "just todo")]
    public class RangedBasicAttack : BasicAttack
    {
        private TargetedProjectile Projectile
        {
            get;
            set;
        }
        public RangedBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1) : base(unit, target, critical, first, slot)
        {
            this.Projectile = new TargetedProjectile(unit.Game.NetIdProvider.PopNextNetId(), unit, (AIUnit)target, Unit.Position, 500, new Action(() => { OnReach(); }));
        }
        private float GetAutocancelDistance()
        {
            return (float)Unit.GetAutoattackRange(Target) + 120f;
        }
        private void OnReach()
        {
            Hit = true;
            InflictDamages();
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            if (Cancelled == false && !Hit)
            {
                Projectile.Update(deltaTime);
            }
        }

    }
}
