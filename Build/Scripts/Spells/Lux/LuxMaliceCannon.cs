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

namespace Legends.bin.Debug.Scripts.Spells.Lux
{
    public class LuxMaliceCannon : SpellScript
    {
        public const string SPELL_NAME = "LuxMaliceCannonMis";

        public const float RANGE = 20000;

        public LuxMaliceCannon(AIUnit owner, SpellRecord spellRecord) : base(owner, spellRecord)
        {
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * 3340;
            var trueCoords = current + range;
        }
    }
}
