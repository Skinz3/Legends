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

namespace Legends.bin.Debug.Scripts.Spells.Ezreal
{
    public class EzrealArcaneShift : SpellScript
    {
        public const string SPELL_NAME = "EzrealArcaneShift";

        public EzrealArcaneShift(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.InstantCast;
            }
        }


        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            var current = Owner.Position;

            var to = new Vector2(position.X, position.Y) - current;
            Vector2 trueCoords;
            if (to.Length() > 475)
            {
                to = Vector2.Normalize(to);
                var range = to * 475;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = position;
            }
            Teleport(trueCoords, true);
            CreateFX("Ezreal_arcaneshift_cas_pulsefire.troy", "", 1f, Owner, false);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("Ezreal_arcaneshift_flash_pulsefire.troy", "", 1f, Owner, false);
        }
    }
}
