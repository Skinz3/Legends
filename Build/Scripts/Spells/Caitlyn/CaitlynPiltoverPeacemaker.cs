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
    public class CaitlynPiltoverPeacemaker : SpellScript
    {
        public const string SPELL_NAME = "CaitlynPiltoverPeacemaker";

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
        public CaitlynPiltoverPeacemaker(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyProjectileEffects(AttackableUnit target, Projectile projectile)
        {
            target.InflictDamages(new Damages(Owner, target, 200f, false, DamageType.DAMAGE_TYPE_PHYSICAL, true));

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition)
        {
            AddSkillShot("CaitlynPiltoverPeacemaker", position, endPosition, RANGE, true);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition)
        {

        }
    }
}
