using Legends.Core;
using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.Utils;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Entities.AI.Particles;
using Legends.World.Games.Delayed;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Spells
{
    public abstract class SpellScript : Script, IUpdatable
    {
        protected AIUnit Owner
        {
            get;
            private set;
        }
        protected SpellRecord SpellRecord
        {
            get;
            private set;
        }
        protected Spell Spell
        {
            get;
            private set;
        }
        public virtual bool DestroyProjectileOnHit
        {
            get
            {
                return false;
            }
        }
        public virtual SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectAllSides | SpellFlags.AffectAllUnitTypes;
            }
        }
        protected float OwnerBonusAD
        {
            get
            {
                return Owner.Stats.AttackDamage.Total - Owner.Stats.AttackDamage.BaseValue;
            }
        }
        protected float OwnerADTotal
        {
            get
            {
                return Owner.Stats.AttackDamage.TotalSafe;
            }
        }
        protected float OwnerBonusAP
        {
            get
            {
                return Owner.Stats.AbilityPower.Total - Owner.Stats.AbilityPower.BaseValue;
            }
        }
        protected float OwnerAPTotal
        {
            get
            {
                return Owner.Stats.AbilityPower.TotalSafe;
            }
        }

        public virtual float OverrideCastTime
        {
            get
            {
                return -1f;
            }
        }

        public virtual bool AutoAttackAnimation
        {
            get
            {
                return false;
            }
        }

        public virtual bool StopMovement
        {
            get
            {
                return true;
            }
        }
        public SpellScript(AIUnit owner, SpellRecord spellRecord)
        {
            this.Owner = owner;
            this.SpellRecord = spellRecord;
        }
        public void Bind(Spell spell)
        {
            this.Spell = spell;
        }

        public virtual void Update(float deltaTime)
        {

        }
        public abstract void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target);

        public abstract void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target);

        protected void CreateShapeCollider(IShape collider)
        {
            foreach (var unit in GetTargets())
            {
                if (collider.Collide(unit))
                {
                    ApplyEffects(unit, collider);
                }
            }
        }

        /// <summary>
        /// Delay in seconds
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        protected DelayedAction CreateAction(Action action, float delay)
        {
            return Owner.Game.Action(action, delay);
        }

        protected void AddSkillShot(string name, Vector2 toPosition, Vector2 endPosition, float range, bool serverOnly = false)
        {
            var record = SpellRecord.GetSpell(name);
            var position = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var casterPosition = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var angle = Geo.GetAngle(Owner.Position, endPosition);
            var direction = Geo.GetDirection(angle);
            var velocity = new Vector3(1, 1, 1) * 100;
            var startPoint = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var endPoint = new Vector3(endPosition.X, endPosition.Y, 100);


            var skillShot = new SkillShot(Spell.GetNextProjectileId(),
                Owner, position.ToVector2(), record.MissileSpeed, record.LineWidth*2,
                OnProjectileReach, direction, range, OnSkillShotRangeReached);

            Owner.Game.AddUnitToTeam(skillShot, Owner.Team.Id);
            Owner.Game.Map.AddUnit(skillShot);

            if (!serverOnly)
            {
                var castInfo = Spell.GetCastInformations(position, endPoint, name, skillShot.NetId);

                Owner.Game.Send(new SpawnProjectileMessage(skillShot.NetId, position, casterPosition,
                    direction.ToVector3(), velocity, startPoint, endPoint, casterPosition,
                    0, record.MissileSpeed, 1f, 1f, 1f, true, castInfo));
            }

        }
        protected void SetAnimation(string slot, string value)
        {
            Owner.Game.Send(new SetAnimationsStatesMessage(Owner.NetId, new Dictionary<string, string>() { { slot, value } }));
        }
        protected void AddTargetedProjectile(string name, AttackableUnit target, bool serverOnly = false, float overrideSpeed = -1f)
        {
            var record = SpellRecord.GetSpell(name);
            float speed = overrideSpeed != -1 ? overrideSpeed : record.MissileSpeed;

            var position = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var casterPosition = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var angle = Geo.GetAngle(Owner.Position, target.Position);
            var direction = Geo.GetDirection(angle);
            var velocity = new Vector3(1, 1, 1) * 100;
            var startPoint = new Vector3(Owner.Position.X, Owner.Position.Y, 100);
            var endPoint = new Vector3(target.Position.X, target.Position.Y, 100);


            var targetedProjectile = new TargetedProjectile(Spell.GetNextProjectileId(),
                Owner, target, startPoint.ToVector2(), speed, OnProjectileReach);

            Owner.Game.AddUnitToTeam(targetedProjectile, Owner.Team.Id);
            Owner.Game.Map.AddUnit(targetedProjectile);

            if (!serverOnly)
            {
                var castInfo = Spell.GetCastInformations(position, endPoint, name, targetedProjectile.NetId, new AttackableUnit[] { target });

                Owner.Game.Send(new SpawnProjectileMessage(targetedProjectile.NetId, position, casterPosition,
                    direction.ToVector3(), velocity, startPoint, endPoint, casterPosition,
                    0, record.MissileSpeed, 1f, 1f, 1f, false, castInfo));
            }

        }
        /// <summary>
        /// Rather a push then a swap.
        /// </summary>
        protected void SwapSpell(string spellName, byte slotId)
        {
            if (Owner.SpellManager.ExistsAtSlot(slotId, spellName))
            {
                Spell previous = Owner.SpellManager.Pop(slotId);
                Spell next = Owner.SpellManager.GetCurrent(slotId);
                Owner.OnSpellSwaped(slotId, next);
                next.NotifyCooldownCurrent();
            }
            else
            {
                Spell spell = SpellProvider.Instance.GetSpell(Owner, slotId, spellName);
                Owner.SpellManager.AddSpell(slotId, spell);
                Owner.OnSpellSwaped(slotId, spell);
            }
        }

        public void DestroyProjectile(Projectile projectile, bool notify)
        {
            Owner.Game.DestroyUnit(projectile);

            if (notify)
                Owner.Game.Send(new DestroyClientMissile(projectile.NetId));
        }
        protected void DestroyFX(uint netId)
        {
            Owner.FXManager.DestroyFX(netId);
        }
        protected void DestroyFX(string name)
        {
            Owner.FXManager.DestroyFX(name);
        }
        protected void DestroyFX(AIUnit unit,string name)
        {
            unit.FXManager.DestroyFX(name);
        }
        public void CreateFX(string effectName, string bonesName, float size, AIUnit target, bool add)
        {
            uint netId = Owner.Game.NetIdProvider.Pop();
            FX fx = new FX(netId, effectName, bonesName, size, Owner, target);
            target.FXManager.CreateFX(fx, add);
        }
        public void CreateFX(string effectName, string bonesName, float size, Vector2 targetPosition)
        {
            uint netId = Owner.Game.NetIdProvider.Pop();
            FX fx = new FX(netId, effectName, bonesName, size, Owner, targetPosition);
            Owner.FXManager.CreateFX(fx, false);
        }

        public void CreateFXs(FX[] fxs, AIUnit target, bool add)
        {
            target.FXManager.CreateFXs(fxs, add);
        }
        protected AttackableUnit[] GetTargets()
        {
            return Owner.Game.Map.Units.OfType<AttackableUnit>().Where(x => x.Alive && IsAffected(x)).ToArray();
        }
        [InDevelopment]
        private bool IsAffected(AttackableUnit unit)
        {
            var flags = Flags; // SpellRecord.Flags?

            if (flags.HasFlag(SpellFlags.NotAffectSelf) && unit == Owner)
            {
                return false;
            }
            if (!flags.HasFlag(SpellFlags.AffectAllSides))
            {
                if (!unit.IsFriendly(Owner) && !flags.HasFlag(SpellFlags.AffectEnemies))
                {
                    return false;
                }
                if (unit.IsFriendly(Owner) && !flags.HasFlag(SpellFlags.AffectFriends))
                {
                    return false;
                }
                if (unit.Team.Id == TeamId.NEUTRAL && flags.HasFlag(SpellFlags.AffectNeutral))
                {
                    return true;
                }
            }
            if (flags.HasFlag(SpellFlags.AffectAllUnitTypes))
            {
                return true;
            }
            if (unit is AITurret)
            {
                return flags.HasFlag(SpellFlags.AffectTurrets);
            }
            if (unit is AIMinion)
            {
                return flags.HasFlag(SpellFlags.AffectMinions);
            }
            if (unit is AIHero)
            {
                return flags.HasFlag(SpellFlags.AffectHeroes);
            }
            return false;
        }
        public virtual void OnProjectileReach(AttackableUnit target, Projectile projectile)
        {
            if (IsAffected(target))
            {
                ApplyEffects(target, projectile);

                if (DestroyProjectileOnHit)
                {
                    DestroyProjectile(projectile, true);
                }
            }
        }
        public virtual void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            // !! --> keep this empty
        }

        public virtual void OnSkillShotRangeReached(SkillShot skillShot)
        {
            DestroyProjectile(skillShot, false);
        }
        protected void Teleport(Vector2 position, bool notify)
        {
            Owner.Teleport(position, notify);
        }

        public virtual bool CanCast()
        {
            return true;
        }
    }
}
