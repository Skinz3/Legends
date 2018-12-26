using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Types;

namespace Legends.World.Entities.AI.BasicAttack
{
    public abstract class BasicAttack : IProtocolable<ProtocolBasicAttack>, IUpdatable
    {
        /// <summary>
        /// Représente le moment ou l'autoattaque applique son effet (dégats pour les mélées, lancé de projectile pour les tireurs)
        /// </summary>
        public static float CAST_TIME_MULTIPLIER = 0.25f;
        /// <summary>
        /// L'autoattaque a t-elle appliquée les dégats? 
        /// </summary>
        public bool Hit
        {
            get;
            set;
        }
        /// <summary>
        /// L'autoattaque a t-elle atteint le stade d'animation 0.75 x AnimationTotalTime
        /// </summary>
        public bool Casted
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

        public BasicAttack(AIUnit unit, AttackableUnit target, bool critical, bool first = true, AttackSlotEnum slot = AttackSlotEnum.BASE_ATTACK_1)
        {
            this.Unit = unit;
            this.Target = target;
            this.Critical = critical;
            this.DeltaAnimationTime = AnimationTime;
            this.First = first;
            this.Slot = slot;
        }

        protected abstract void OnCancel();

        public void Cancel()
        {
            Cancelled = true;
            Unit.Game.Send(new StopAutoAttackMessage(Unit.NetId));
            OnCancel();
        }

        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "BeginAuto donne une position legerement differente au client")]
        public void Notify()
        {
            Unit.Game.Send(new BasicAttackMessage(Unit.NetId, GetProtocolObject()));


        }
        public void InflictDamages()
        {
            Target.InflictDamages(new Damages(Unit, Target, Unit.Stats.AttackDamage.TotalSafe, Critical, DamageType.DAMAGE_TYPE_PHYSICAL, true));
            Hit = true;
        }

        protected abstract float GetAutocancelDistance();

        protected abstract void OnCastTimeReach();

        public void Update(float deltaTime)
        {
            if (Casted && Unit.GetDistanceTo(Target) > GetAutocancelDistance() && !Cancelled)
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
                        Unit.AttackManager.StopAttackTarget();
                        Unit.AttackManager.DestroyAutoattack();
                    }
                }
                else
                {
                    Unit.AttackManager.DestroyAutoattack();
                }
            }

            if (Cancelled == false && !Casted)
            {
                if (DeltaAnimationTime / AnimationTime <= (1 - CAST_TIME_MULTIPLIER))
                {
                    Casted = true;
                    OnCastTimeReach();
                }

            }
        }

        public ProtocolBasicAttack GetProtocolObject()
        {
            return new ProtocolBasicAttack()
            {
                AttackSlot = Slot,
                ExtraTime = 0,
                MissileNextId = Unit.Game.NetIdProvider.PopNextNetId(),
                TargetNetId = Target.NetId,
                TargetPosition = Target.GetPositionVector3(),
            };
        }
    }
}
