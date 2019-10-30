using Legends.Protocol.GameClient.Enum;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;
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

        public SummonerFlash(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
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

            CreateFX("global_ss_flash.troy", "", 1f, Owner,false);
            Teleport(trueCoords, true);
            CreateFX("global_ss_flash_02.troy", "", 1f, Owner,false);


        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {


        }
    }
}
