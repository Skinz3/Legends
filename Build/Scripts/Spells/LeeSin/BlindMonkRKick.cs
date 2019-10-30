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
    public class BlindMonkRKick : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkRKick";

        public BlindMonkRKick(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }


        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("blind_monk_ult_impact.troy", "", 1f, (AIUnit)target, false);
            var direction = Vector2.Normalize(target.Position - Owner.Position);
            (target as AIUnit).Dash(target.Position + (direction * 800f), 1000f, true);

            target.InflictDamages(new World.Spells.Damages(Owner, target, 400, false, Protocol.GameClient.Enum.DamageType.DAMAGE_TYPE_PHYSICAL, false));
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
