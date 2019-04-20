using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.Zed
{
    public class ZedPBAOEDummy : SpellScript
    {
        public const string SPELL_NAME = "ZedPBAOEDummy";

        public override SpellFlags Flags
        {
            get
            {
                return SpellFlags.AffectEnemies | SpellFlags.AffectHeroes | SpellFlags.AffectMinions;
            }
        }
        public override bool StopMovement
        {
            get
            {
                return false;
            }
        }
        public ZedPBAOEDummy(AIUnit unit, SpellRecord record) : base(unit, record)
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
            CreateFX("Zed_E_cas.troy", "", 1f, (AIUnit)Owner, false);

            foreach (var unit in GetTargets())
            {
                if (unit.GetDistanceTo(Owner) <= SpellRecord.CastRange)
                {
                    CreateFX("Zed_E_Tar.troy", "", 1f, (AIUnit)unit, false);
                    unit.InflictDamages(new Damages(Owner, target, 200, false, DamageType.DAMAGE_TYPE_PHYSICAL, false));
                }
            }
        }
    }
}
