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

namespace Legends.bin.Debug.Scripts.Spells.Graves
{
    public class GravesMove : SpellScript
    {
        public const string SPELL_NAME = "GravesMove";

        public const int RANGE = 425;

        public GravesMove(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }


        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {



        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;
            var to = Vector2.Normalize(position - current);
            var range = to * RANGE;
            var trueCoords = current + range;


            SetAnimation("RUN", "Spell3");

            CreateFX("Graves_Move_OnBuffActivate.troy", "", 1f, Owner,true);

            Action onDashEnded = () =>
            {
                DestroyFX("Graves_Move_OnBuffActivate.troy");
                SetAnimation("RUN", "");
            };

            Owner.Dash(trueCoords, 1200f, false,onDashEnded);


            // owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell, 4.0f, true);
        }
        public override bool CanCast()
        {
            return Owner.DashManager.IsDashing == false;
        }
    }
}
