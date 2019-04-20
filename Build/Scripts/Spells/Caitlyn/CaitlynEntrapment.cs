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

namespace Legends.bin.Debug.Scripts.Spells.Caitlyn
{
    public class CaitlynEntrapment : SpellScript
    {
        public const string SPELL_NAME = "CaitlynEntrapment";

        public const float RANGE = 750;

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
        public CaitlynEntrapment(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
            target.InflictDamages(new Damages(Owner, target, 200f, false, DamageType.DAMAGE_TYPE_PHYSICAL, true));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            // Calculate net coords
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * 750;
            var trueCoords = current + range;

            // Calculate dash coords/vector
            var dash = Vector2.Negate(to) * 500;


            var dashCoords = current + dash;


            Action onDashEnded = () =>
            {
                SetAnimation("RUN", "");
            };

            SetAnimation("RUN", "Spell3b");
            Owner.Dash(dashCoords, 1000, true, onDashEnded);


            AddSkillShot("CaitlynEntrapmentMissile", trueCoords, endPosition, RANGE, false);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
