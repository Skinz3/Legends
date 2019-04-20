using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.Scripts.Spells;
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

namespace Legends.bin.Debug.Scripts.Spells.Annie
{
    public class Disintegrate : SpellScript
    {
        public const string SPELL_NAME = "Disintegrate";

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
        public Disintegrate(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
            var ap = OwnerAPTotal * 0.8f;
            var damage = 45 + (Spell.Level * 35) + ap;
            target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_MAGICAL, false));

            if (!target.Alive)
                Spell.LowerCooldown(Spell.GetTotalCooldown() / 2f);
            // add mana
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddTargetedProjectile("Disintegrate", target);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
