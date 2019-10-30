using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Entities.AI.Particles;
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
    public class BlindMonkEOne : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkEOne";

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectMinions;
            }
        }
        public BlindMonkEOne(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }



        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            target.InflictDamages(new Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            FX[] fxs = new FX[]
            {
                  new FX(target.Game.NetIdProvider.Pop(),"blindMonk_thunderCrash_impact_02.troy","",1f,Owner,Owner),
                  new FX(target.Game.NetIdProvider.Pop(),"blindMonk_thunderCrash_impact_cas.troy","",1f,Owner,Owner),
            };

            CreateFXs(fxs, Owner, false);

            CreateShapeCollider(new Circle(Owner.Position, SpellRecord.CastRange));
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
