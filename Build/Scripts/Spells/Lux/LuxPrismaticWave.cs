using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.Lux
{
    public class LuxPrismaticWave : SpellScript
    {
        public const string SPELL_NAME = "LuxPrismaticWave";

        public const float RANGE = 20000;

        public LuxPrismaticWave(AIUnit owner, SpellRecord spellRecord) : base(owner, spellRecord)
        {

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
