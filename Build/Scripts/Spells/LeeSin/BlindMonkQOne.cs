using Legends.Core;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Entities.AI.Particles;
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
    public class BlindMonkQOne : SpellScript
    {
        public const string SPELL_NAME = "BlindMonkQOne";


        public BlindMonkQOne(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }
        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectNeutral;
            }
        }
        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }

        private AIUnit CurrentTarget
        {
            get;
            set;
        }
        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            var targetAI = (AIUnit)target;

            FX[] fxs = new FX[]
            {
                 new FX(target.Game.NetIdProvider.Pop(),"blindMonk_Q_tar.troy","",1f,Owner,targetAI),
                 new FX(target.Game.NetIdProvider.Pop(),"blindMonk_Q_tar_indicator.troy","",1f,Owner,targetAI),
            };

            CreateFXs(fxs, targetAI, true);

            CurrentTarget = targetAI;

            target.InflictDamages(new World.Spells.Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));

            if (target.Alive)
            {
                SwapSpell("BlindMonkQTwo", 0);
            }

            CreateAction(() =>
            {
                if (CurrentTarget != null && CurrentTarget.ObjectAvailable)
                {
                    var blindMonkQTwo = Owner.SpellManager.GetSpell("BlindMonkQTwo");

                    if (blindMonkQTwo != null && !blindMonkQTwo.GetScript<BlindMonkQTwo>().Casted)
                    {
                        SwapSpell("BlindMonkQOne", 0);
                        CurrentTarget.FXManager.DestroyFX("blindMonk_Q_tar.troy");
                        CurrentTarget.FXManager.DestroyFX("blindMonk_Q_tar_indicator.troy");
                    }
                }
                CurrentTarget = null;

            }, 3f);

        }
        public AIUnit GetTarget()
        {
            return CurrentTarget;
        }
        public void DestroyTarget()
        {
            CurrentTarget = null;
        }
        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddSkillShot("BlindMonkQOne", position, endPosition, 1000, true);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
        public override bool CanCast()
        {
            return CurrentTarget == null;
        }
    }
}
