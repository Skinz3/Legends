using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;
using System.Collections.Concurrent;
using Legends.World.Spells.Projectiles;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class RangedManager : AttackManager
    {
        private List<TargetedProjectile> Projectiles
        {
            get;
            set;
        }
        public RangedManager(AIUnit unit) : base(unit)
        {
            this.Projectiles = new List<TargetedProjectile>();
        }
        public override void Update(long deltaTime)
        {
            foreach (var projectile in Projectiles.ToArray())
            {
                projectile.Update(deltaTime);
            }
            base.Update(deltaTime);
        }
        public void AddProjectile(TargetedProjectile projectile)
        {
            this.Projectiles.Add(projectile);
        }
        public void RemoveProjectile(TargetedProjectile projectile)
        {
            this.Projectiles.Remove(projectile);
        }
        public override BasicAttack CreateBasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1)
        {
            return new RangedBasicAttack(unit, target, critical, first, slot);
        }
    }
}
