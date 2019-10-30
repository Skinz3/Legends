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
using Legends.World.Spells.Shapes;

namespace Legends.Scripts.Spells
{
    public class EzrealTrueshotBarrage : SpellScript
    {
        public const string SPELL_NAME = "EzrealTrueshotBarrage";

        public const float RANGE = 20000;

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return false;
            }
        }

        public EzrealTrueshotBarrage(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectMinions;
            }
        }

        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            SkillShot skillShot = projectile as SkillShot;

            float finalDamagePercent = 1 - ((skillShot.GetHittenCount() - 1) * 0.1f);

            finalDamagePercent = Math.Max(finalDamagePercent, 0.3f);

            var ad = OwnerBonusAD * 1.0f;
            var ap = OwnerAPTotal * 0.9f;
            var baseDamage = 200 + (Spell.Level * 150);
            var total = baseDamage + ap + ad;
            total *= finalDamagePercent;
            target.InflictDamages(new Damages(Owner, target, total, false, DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

            AddSkillShot("EzrealTrueshotBarrage", position, endPosition, RANGE, true);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("Ezreal_bow_huge.troy", "L_HAND", 1f, Owner,false);
        }
    }
}
