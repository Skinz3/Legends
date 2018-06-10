using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.BasicAttack
{
    public abstract class BasicAttack : IUpdatable
    {
        /// <summary>
        /// L'autoattaque a t-elle appliquée les dégats?
        /// </summary>
        public bool Hit
        {
            get;
            set;
        }
        /// <summary>
        /// L'autoattaque a t-elle été cancel ?
        /// </summary>
        public bool Cancelled
        {
            get;
            set;
        }
        /// <summary>
        /// La durée totale de l'animation de l'autoattaque.
        /// </summary>
        protected float AnimationTime
        {
            get
            {
                return (1 / Unit.Stats.AttackSpeed.TotalSafe) * 1000;
            }
        }
        /// <summary>
        /// La durée actuelle de l'animation
        /// </summary>
        protected float DeltaAnimationTime
        {
            get;
            set;
        }
        protected AIUnit Unit
        {
            get;
            set;
        }
        public AttackableUnit Target
        {
            get;
            private set;
        }
        protected bool First
        {
            get;
            set;
        }
        public bool Finished
        {
            get
            {
                return DeltaAnimationTime <= 0;
            }
        }
        public AttackSlotEnum Slot
        {
            get;
            private set;
        }
        public Func<BasicAttack, bool> OnBasicAttackEnded
        {
            get;
            set;
        }
        public bool Critical
        {
            get;
            private set;
        }
        public BasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASIC_ATTACK_1)
        {
            this.Unit = unit;
            this.Target = target;
            this.Critical = critical;
            this.DeltaAnimationTime = AnimationTime;
            this.First = first;
            this.Slot = slot;
        }
        public void Cancel()
        {
            Cancelled = true;
            Unit.Game.Send(new StopAutoAttackMessage(Unit.NetId));
        }
        public void Notify()
        {
            if (First)
            {
                Unit.Game.Send(new BeginAutoAttackMessage(Unit.NetId, Target.NetId, 0x80, 0, Critical, Target.Position, Unit.Position, Unit.Game.Map.Record.MiddleOfMap));
            }
            else
            {
                Unit.Game.Send(new NextAutoattackMessage(Unit.NetId, Target.NetId, 0, Slot, false));
            }
        }
        public void InflictDamages()
        {
            Target.InflictDamages(new Damages(Unit, Target, Unit.Stats.AttackDamage.TotalSafe, Critical, DamageType.DAMAGE_TYPE_PHYSICAL));
            Hit = true;
        }
        public virtual void Update(long deltaTime)
        {
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
