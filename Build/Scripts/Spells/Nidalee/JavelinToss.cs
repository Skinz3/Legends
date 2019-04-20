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

namespace Legends.bin.Debug.Scripts.Spells.Nidalee
{
    public class JavelinToss : SpellScript
    {
        public const string SPELL_NAME = "JavelinToss";

        public const float RANGE = 1300f;

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
        public JavelinToss(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
            var ap = OwnerAPTotal * 1f;
            var damage = 15 + (Spell.Level * 20) + ap;
            target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_MAGICAL, true));

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

            //AddSkillShot("JavelinToss", position, endPosition, RANGE, true);

        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }

    }
}
