using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.Vayne
{
    public class VayneTumble : SpellScript
    {
        public const string SPELL_NAME = "VayneTumble";


        public VayneTumble(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }


        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * 300;
            var trueCoords = current + range;


            SetAnimation("RUN", "Spell1");


            Action onDashEnded = () =>
            {

                SetAnimation("RUN", "");
            };


            Owner.Dash(trueCoords, 600f, false, onDashEnded);

        }
    }
}
