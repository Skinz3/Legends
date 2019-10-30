using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.Caitlyn
{
    public class CaitlynAceintheHole : SpellScript
    {
        public const string SPELL_NAME = "CaitlynAceintheHole";

        public const float RANGE = 1150;

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectNeutral;
            }
        }

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }
        public override float OverrideCastTime
        {
            get
            {
                return 1f;
            }
        }
        public CaitlynAceintheHole(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            if (target != null && target.Alive)
            {
                // 250/475/700
                var damage = 250 + Owner.Stats.AttackDamage.Total * 2;
                CreateFX("caitlyn_ace_tar.troy", "", 1f, (AIUnit)target, false);
                ((AIUnit)target).FXManager.DestroyFX("caitlyn_ace_target_indicator.troy");
                target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));
            }
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddTargetedProjectile("CaitlynAceintheHoleMissile", target, false, 3000);
        }
        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("caitlyn_ace_target_indicator.troy", "", 1f, (AIUnit)target, true);
        }
    }
}
