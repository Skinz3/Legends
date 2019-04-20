using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
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

namespace Legends.bin.Debug.Scripts.Spells.Zed
{
    public class ZedShadowDash : SpellScript
    {
        public const string SPELL_NAME = "ZedShadowDash";

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return false;
            }
        }
        public ZedShadowDash(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
            target.InflictDamages(new Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
          
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var castInfo = Spell.GetCastInformations(position.ToVector3(),
            endPosition.ToVector3(), "ZedShadowDashMissile");
            castInfo.DesignerCastTime = 0f;
            castInfo.DesignerTotalTime = 0f;
            castInfo.ExtraCastTime = 0f;
            Owner.Game.Send(new CastSpellAnswerMessage(Owner.NetId, Environment.TickCount, false, castInfo));
        }
    }
   
}
