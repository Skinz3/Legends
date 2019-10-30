using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Messages.Game;
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

namespace Legends.bin.Debug.Scripts.Spells.LeeSin
{
    public class JaxCounterStrike : SpellScript
    {
        public const string SPELL_NAME = "JaxCounterStrike";


        public JaxCounterStrike(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }



        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            SetAnimation("Idle1", "Spell3");
            CreateFX("JaxDodger.troy", "R_HAND", 1f, Owner, false);
            //CreateFX("Counterstrike_tar.troy", "", 1f, Owner, false); targets hits
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
         
        }
    }
}
