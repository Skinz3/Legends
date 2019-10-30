using Legends.bin.Debug.Scripts.Buffs;
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
    public class BlindMonkWOne : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkWOne";


        public BlindMonkWOne(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("blindMonk_W_cas_01.troy", "", 1f, (AIUnit)Owner, false);

            ((AIUnit)target).BuffManager.AddBuff<Safeguard>(Owner);

            if (target != Owner)
            {
                SetAnimation("Run", "Spell2");

                Action onDashEnd = () =>
                {
                    SetAnimation("Run", "Run");

                };

                Owner.Dash(target.Position, 1800f, false, onDashEnd);
            }
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
