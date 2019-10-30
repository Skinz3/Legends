using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
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

namespace Legends.bin.Debug.Scripts.Spells.Zed
{
    public class ZedShuriken : SpellScript
    {
        public const string SPELL_NAME = "ZedShuriken";

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return false;
            }
        }
        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectNeutral;
            }
        }
        public ZedShuriken(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            CreateFX("Zed_Q_tar.troy", "", 1f, (AIUnit)target, false);
            target.InflictDamages(new Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddSkillShot("ZedShurikenMisOne", position, endPosition, 1000);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
