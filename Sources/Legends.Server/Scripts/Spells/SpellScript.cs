using Legends.Core;
using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
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
        public abstract SpellFlags Flags
        {
            get;
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
        public abstract void OnStartCasting(Vector2 position, Vector2 endPosition);

        public abstract void OnFinishCasting(Vector2 position, Vector2 endPosition);

        protected void AddProjectile(string name, Vector2 toPosition, Vector2 endPosition, float range, bool serverOnly = false)
        {
            var record = SpellRecord.GetSpell(name);
            var position = new Vector3(Owner.Position.X, Owner.Position.Y, 0);
            var casterPosition = Owner.GetPositionVector3();
            var angle = Geo.GetAngle(Owner.Position, endPosition);
            var direction = Geo.GetDirection(angle);
            var velocity = new Vector3(1, 1, 1) * 100;
            var startPoint = new Vector3(Owner.Position.X, Owner.Position.Y, 0);
            var endPoint = new Vector3(endPosition.X, endPosition.Y, 0);


            var skillShot = new SkillShot(Owner.Game.NetIdProvider.PopNextNetId(),
                Owner, position.ToVector2(), record.MissileSpeed, record.LineWidth,
                OnProjectileReach, direction, range, OnSkillShotRangeReached);


            Owner.Game.AddUnitToTeam(skillShot, Owner.Team.Id);
            Owner.Game.Map.AddUnit(skillShot);

            if (!serverOnly)
            {
                var castInfo = Spell.GetCastInformations(position, endPoint, name, skillShot.NetId);

                Owner.Game.Send(new SpawnProjectileMessage(skillShot.NetId, position, casterPosition,
                    direction.ToVector3(), velocity, startPoint, endPoint, casterPosition,
                    0, record.MissileSpeed, 1f, 1f, 1f, false, castInfo));
            }

        }
        public void DestroyProjectile(Projectile projectile, bool notify)
        {
            Owner.Game.DestroyUnit(projectile);
            if (notify)
                Owner.Game.Send(new DestroyClientMissile(projectile.NetId));
        }
        [InDevelopment]
        private bool IsAffected(AttackableUnit unit)
        {
            var flags = Flags; // SpellRecord.Flags?

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
                ApplyProjectileEffects(target, projectile);

                if (DestroyProjectileOnHit)
                {
                    DestroyProjectile(projectile, true);
                }
            }
        }
        public abstract void ApplyProjectileEffects(AttackableUnit target, Projectile projectile);

        public virtual void OnSkillShotRangeReached(SkillShot skillShot)
        {
            DestroyProjectile(skillShot, false);
        }
        protected void Teleport(Vector2 position)
        {
            Owner.Teleport(position);
        }
    }
}
