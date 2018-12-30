using Legends.Protocol.GameClient.Enum;
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

namespace Legends.bin.Debug.Scripts.Spells.Global
{
    public class SummonerFlash : SpellScript
    {
        public const string SPELL_NAME = "SummonerFlash";

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
        public SummonerFlash(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        public override void ApplyProjectileEffects(AttackableUnit target, Projectile projectile)
        {


        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition)
        {
            var current = Owner.Position;
            var to = new Vector2(position.X, position.Y) - current;
            Vector2 trueCoords;

            if (to.Length() > 425)
            {
                to = Vector2.Normalize(to);
                var range = to * 425;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = position;
            }

            AddParticle("global_ss_flash.troy", "", 1f);
            Owner.Teleport(trueCoords);
            AddParticle("global_ss_flash_02.troy", "", 1f);


        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition)
        {


        }
    }
}
