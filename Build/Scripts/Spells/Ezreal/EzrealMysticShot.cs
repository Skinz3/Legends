using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legends.Records;
using Legends.World.Entities.AI;
using System.Threading.Tasks;
using System.Numerics;
using Legends.World.Spells;
using Legends.World.Entities;
using Legends.Protocol.GameClient.Enum;
using Legends.World.Spells.Projectiles;

namespace Legends.Scripts.Spells
{
    public class EzrealMysticShot : SpellScript
    {
        public const string SPELL_NAME = "EzrealMysticShot";

        public const float RANGE = 1150;

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }
        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectNeutral;
            }
        }
        public EzrealMysticShot(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyProjectileEffects(AttackableUnit target, Projectile projectile)
        {
            var ad = OwnerADTotal * 1.1f;
            var ap = OwnerAPTotal * 0.4f;
            var damage = 15 + (Spell.Level * 20) + ad + ap;
            target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_PHYSICAL, true));

            Owner.SpellManager.LowerCooldowns(1f);
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition)
        {
            AddSkillShot("EzrealMysticShotMissile", position, endPosition, RANGE);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition)
        {
            AddParticle("ezreal_bow.troy", "L_HAND", 1f);
        }
    }
}
