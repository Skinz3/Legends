using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class VeigarPrimordialBurst : SpellScript
    {
        public const string SPELL_NAME = "VeigarPrimordialBurst";

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }
        public VeigarPrimordialBurst(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            CreateFX("Veigar_Base_R_tar.troy", "", 2f, target as AIUnit, false);
            target.InflictDamages(new World.Spells.Damages(Owner, target, 500, false, Protocol.GameClient.Enum.DamageType.DAMAGE_TYPE_MAGICAL, false));
        }
        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddTargetedProjectile("VeigarPrimordialBurst", target, true);
        }
        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
