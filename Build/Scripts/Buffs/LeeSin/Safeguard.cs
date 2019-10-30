using Legends.Protocol.GameClient.Enum;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Buffs
{
    public class Safeguard : BuffScript
    {
        public override BuffTypeEnum BuffType => BuffTypeEnum.CombatEnchancer;

        public override string BuffName => "BlindMonkWOne";

        public Safeguard(AIUnit source, AIUnit target) : base(source, target)
        {

        }
        public override float MaxDuration => 3000;

        public override void OnAdded()
        {
            CreateFX("blindMonk_W_shield_self.troy", "", 1f, Target, true);
            Target.AddGlobalShield(100);
        }

        public override void OnRemoved()
        {
            DestroyFX("blindMonk_W_shield_self.troy");
            Target.RemoveGlobalShield(100);
        }
    }
}
