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
            this.Projectile = new TargetedProjectile(unit.Game.NetIdProvider.PopNextNetId(), unit, (AttackableUnit)target, Unit.Position, 500, new Action(() => { OnReach(); }));
            unit.GetAttackManager<RangedManager>().AddProjectile(Projectile);

        }
        private float GetAutocancelDistance()
        {
            return (float)Unit.GetAutoattackRange(Target) + 120f;
        }
        private void OnReach()
        {
            Hit = true;

            if (Target.Alive)
            {
                InflictDamages();
            }

            Unit.GetAttackManager<RangedManager>().RemoveProjectile(Projectile);
            Projectile = null;
        }
        public override void OnCancel()
        {
            //   Unit.GetAttackManager<RangedManager>().RemoveProjectile(Projectile);
            //    Projectile = null;
        }
        public override void Update(long deltaTime)
        {
            if (Hit && Unit.GetDistanceTo(Target) > GetAutocancelDistance() && !Cancelled)
            {
                Unit.AttackManager.StopAttackTarget();
                Unit.AttackManager.DestroyAutoattack();
                Unit.TryBasicAttack(Target);
                return;
            }

            DeltaAnimationTime -= deltaTime;

            if (DeltaAnimationTime <= 0)
            {
                if (OnBasicAttackEnded != null)
                {
                    if (OnBasicAttackEnded(this))
                    {
                        return;
                    }
                }

                if (Cancelled == false)
                {
                    if (Target.Alive)
                    {
                        if (Unit.GetDistanceTo(Target) <= Unit.GetAutoattackRange(Target))
                        {
                            Unit.AttackManager.NextAutoattack();
                        }
                        else
                        {
                            Unit.AttackManager.StopAttackTarget();
                            Unit.AttackManager.DestroyAutoattack();
                            Unit.TryBasicAttack(Target);
                        }
                    }
                    else
                    {
                        Console.WriteLine("We stop attack target caus dead");
                        Unit.AttackManager.StopAttackTarget();
                        Unit.AttackManager.DestroyAutoattack();
                    }
                }
                else
                {
                    Unit.AttackManager.DestroyAutoattack();
                }
            }


        }
    }

}
