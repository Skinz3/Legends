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

namespace Legends.bin.Debug.Scripts.Spells.Annie
{
    public class Incinerate : SpellScript
    {
        public const string SPELL_NAME = "Incinerate";

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
        public Incinerate(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
          
            var ap = OwnerAPTotal * 0.85f;
            var damage = 25 + (Spell.Level * 45) + ap;
            target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * 625;
            var trueCoords = current + range;

            AddCone(trueCoords, 24.76f);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
