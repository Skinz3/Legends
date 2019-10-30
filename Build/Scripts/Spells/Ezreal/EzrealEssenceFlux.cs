using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legends.Records;
using Legends.World.Entities.AI;
using System.Threading.Tasks;
using System.Numerics;
using Legends.World.Spells;
using Legends.World.Entities;
using Legends.Protocol.GameClient.Enum;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;

namespace Legends.Scripts.Spells
{
    public class EzrealEssenceFlux : SpellScript
    {
        public const string SPELL_NAME = "EzrealEssenceFlux";

        public const float RANGE = 1000;

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectAllSides | SpellFlags.AffectHeroes;
            }
        }
        public EzrealEssenceFlux(AIUnit unit, SpellRecord record) : base(unit, record)
        {
        }

        public override void ApplyEffects(AttackableUnit target, IShape projectile)
        {
            if (target.IsFriendly(Owner))
            {
                // add as buff
            }
            else
            {
                var ap = OwnerAPTotal * 1.25f;
                var damage = 25 + (Spell.Level * 45) + ap;
                target.InflictDamages(new Damages(Owner, target, damage, false, DamageType.DAMAGE_TYPE_MAGICAL, false));
            }
        }

        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            AddSkillShot("EzrealEssenceFluxMissile", position, endPosition, 1000);
        }

        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            CreateFX("ezreal_bow_yellow.troy", "L_HAND", 1f, Owner,false);
        }
    }
}
