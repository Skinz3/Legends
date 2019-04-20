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
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectMinions;
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
            castInfo.DesignerCastTime = 0.2f;
            castInfo.DesignerTotalTime = 0.2f;
            Owner.Game.Send(new CastSpellAnswerMessage(Owner.NetId, Environment.TickCount, false, castInfo));

            var a = GetTargets();

            foreach (var b in a)
            {
                CreateFX("Yasuo_Base_Q_hit_tar.troy", "", 1f, (AIUnit)b, false);
                b.InflictDamages(new Damages(Owner, b, 100f, false, DamageType.DAMAGE_TYPE_PHYSICAL, true));
            }
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }

    }
}
