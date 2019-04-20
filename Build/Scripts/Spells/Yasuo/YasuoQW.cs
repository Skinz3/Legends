using Legends.Core;
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

namespace Legends.bin.Debug.Scripts.Spells.Yasuo
{
    public class YasuoQW : SpellScript
    {
        public const string SPELL_NAME = "YasuoQW";

        public const float RANGE = 1150;

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }
        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectNeutral;
            }
        }
        public YasuoQW(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {


        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            /*
             * spawn tornado
             * var castInfo = Spell.GetCastInformations(position.ToVector3(),
               endPosition.ToVector3(), "YasuoQ3Mis", 0);
            castInfo.DesignerCastTime = 0f;
            castInfo.DesignerTotalTime = 0f;
            castInfo.ExtraCastTime = 0f;
            Owner.Game.Send(new CastSpellAnswerMessage(Owner.NetId, Environment.TickCount, false, castInfo));*/

            var castInfo = Spell.GetCastInformations(position.ToVector3(),
               endPosition.ToVector3(), "YasuoQ", 0);
            castInfo.DesignerCastTime = 0.1f;
            castInfo.DesignerTotalTime = 0.1f;
            castInfo.ExtraCastTime = 0f;
            Owner.Game.Send(new CastSpellAnswerMessage(Owner.NetId, Environment.TickCount, false, castInfo));
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
           
        }

    }
}
