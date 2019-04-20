using Legends.Core.Geometry;
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

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class JaxLeapStrike : SpellScript
    {
        public const string SPELL_NAME = "JaxLeapStrike";


        public JaxLeapStrike(AIUnit unit, SpellRecord record) : base(unit, record)
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

        }
    }
}
