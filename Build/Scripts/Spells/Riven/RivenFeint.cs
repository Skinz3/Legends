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

namespace Legends.bin.Debug.Scripts.Spells.Riven
{
    public class RivenFeint : SpellScript
    {
        public const string SPELL_NAME = "RivenFeint";


        public RivenFeint(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * 200;
            var trueCoords = current + range;


            SetAnimation("RUN", "Spell1b");

            CreateFX("Riven_Groundwave.troy.troy", "", 2f, Owner,false);

            Action onDashEnded = () =>
            {

                SetAnimation("RUN", "");
            };


            Owner.Dash(trueCoords, 300f, false, onDashEnded);

        }
    }
}
