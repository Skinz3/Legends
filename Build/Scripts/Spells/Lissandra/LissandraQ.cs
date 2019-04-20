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
    public class LissandraQ : SpellScript
    {
        public const string SPELL_NAME = "LissandraQ";


        public LissandraQ(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        public override bool DestroyProjectileOnHit
        {
            get
            {
                return true;
            }
        }

        public override void ApplyEffects(AttackableUnit target, IMissile projectile)
        {
            target.InflictDamages(new World.Spells.Damages(Owner, target, 500, false, Protocol.GameClient.Enum.DamageType.DAMAGE_TYPE_MAGICAL, false));
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

            SetAnimation("React", "Spell1");
            AddSkillShot("LissandraQMissile", position, endPosition, 500);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {

        }
    }
}
