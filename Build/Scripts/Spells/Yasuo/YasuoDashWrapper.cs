using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
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

namespace Legends.bin.Debug.Scripts.Spells.Yasuo
{
    public class YasuoDashWrapper : SpellScript
    {
        public const string SPELL_NAME = "YasuoDashWrapper";

        public const int RANGE = 425;

        public YasuoDashWrapper(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }


        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("Yasuo_Base_E_Dash.troy", "", 1f, (AIUnit)Owner, false);

            CreateFX("Yasuo_base_E_timer1.troy", "", 1f, (AIUnit)target, false);

            var current = Owner.Position;
            var to = Vector2.Normalize(target.Position - current);
            var range = to * 425;
            var trueCoords = current + range;

            SetAnimation("RUN", "Spell3");


            Action onDashEnded = () =>
            {
                SetAnimation("RUN", "RUN");
            };



            target.InflictDamages(new World.Spells.Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_MAGICAL, false));
            Owner.Dash(trueCoords, 1000f, false, onDashEnded);
        }
        public override bool CanCast()
        {
            return Owner.DashManager.IsDashing == false;
        }
    }
}
