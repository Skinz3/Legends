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

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class VeigarDarkMatter : SpellScript
    {
        public const string SPELL_NAME = "VeigarDarkMatter";

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectMinions;
            }
        }

        public VeigarDarkMatter(AIUnit unit, SpellRecord record) : base(unit, record)
        {
            
        }



        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            target.InflictDamages(new World.Spells.Damages(Owner, target, 500, false, Protocol.GameClient.Enum.DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            float start = 0;

            SpawnDarkMatter(position);

            /*float end = (float)(2 * Math.PI);

            for (float i = start; i < end; i += 0.5f)
            {
                var v = Geo.GetPointOnCircle(position, i, 300f);
                SpawnDarkMatter(v);
            } */

           
        }
        private void SpawnDarkMatter(Vector2 position)
        {
            CreateFX("Veigar_Base_W_cas.troy", "", 1f, position);

            CreateAction(() =>
            {
                CreateShapeCollider(new Circle(position, SpellRecord.CastRadius));
                CreateFX("Veigar_Base_W_aoe_explosion.troy", "", 1f, position);

            }, 1.2f);
        }
    }
}
