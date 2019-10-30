using Legends.Protocol.GameClient.Enum;
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

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class BlindMonkQTwo : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkQTwo";

        public bool Casted
        {
            get;
            set;
        }
        public BlindMonkQTwo(AIUnit unit, SpellRecord record) : base(unit, record)
        {
            this.Casted = false;
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }
        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            this.Casted = true;

            var script = Owner.SpellManager.GetSpell("BlindMonkQOne").GetScript<BlindMonkQOne>();

            AIUnit dashTarget = script.GetTarget();

            SetAnimation("Run", "Spell1b");

            Action onDashEnd = () =>
            {
                Owner.PathManager.DestroyPendingPoint();
                SetAnimation("Run", "Run");
                DestroyFX("blindMonk_W_self_mis.troy");
                DestroyFX(dashTarget, "blindMonk_Q_tar.troy");
                DestroyFX(dashTarget, "blindMonk_Q_tar_indicator.troy");
                dashTarget.InflictDamages(new World.Spells.Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));
                script.DestroyTarget();
                Owner.TryBasicAttack(dashTarget);
            };

            Owner.Dash(dashTarget.Position, 1800f, false, onDashEnd);

            SwapSpell("BlindMonkQOne", 0);
        }
    }
}
