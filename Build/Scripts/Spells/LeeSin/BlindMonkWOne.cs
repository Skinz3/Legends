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

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class BlindMonkWOne : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkWOne";


        public BlindMonkWOne(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }



        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("blindMonk_W_cas_01.troy", "", 1f, (AIUnit)target, false);
            CreateFX("blindMonk_W_self_mis.troy", "", 1f, (AIUnit)Owner, true);

            /*CreateFX("blindMonk_W_shield_self.troy", "", 1f, (AIUnit)target, true); <----- this is in buff part
            CreateFX("blindMonk_W_shield_self.troy", "", 1f, (AIUnit)Owner, true); */

            SetAnimation("Run", "Spell2");

            Action onDashEnd = () =>
            {
                SetAnimation("Run", "Run");
                DestroyFX("blindMonk_W_self_mis.troy");
            };

            Owner.Dash(target.Position, 1500f, false, onDashEnd);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
