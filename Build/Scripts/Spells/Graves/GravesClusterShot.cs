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

namespace Legends.bin.Debug.Scripts.Spells.Graves
{
    public class GravesClusterShot : SpellScript
    {
        public const string SPELL_NAME = "GravesClusterShot";


        public GravesClusterShot(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }



        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            target.InflictDamages(new World.Spells.Damages(Owner, target, 500, false, Protocol.GameClient.Enum.DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddSkillShot("GravesChargeShotFxMissile2", position, endPosition, 5000);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
