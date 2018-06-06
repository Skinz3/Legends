using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    [InDeveloppement(InDeveloppementState.BAD_SPELLING, "Find a better name!")]
    public class AutoattackManager : IUpdatable
    {
        public AIUnit Unit
        {
            get;
            private set;
        }
        private AttackableUnit TargetUnit
        {
            get;
            set;
        }
        private bool HaveTarget
        {
            get
            {
                return TargetUnit != null;
            }
        }
        public bool Auto
        {
            get;
            private set;
        }
        private bool IsAttacking
        {
            get;
            set;
        }
        public AutoattackManager(AIUnit unit, bool auto)
        {
            this.Unit = unit;
            this.Auto = auto;
        }

        private long test = 0;

        public void Update(long deltaTime)
        {
            if (IsAttacking)
            {
                test++;
                if (test == 30)
                {
                    TargetUnit.InflictDamages(new Damages(Unit, TargetUnit, 12, DamageType.DAMAGE_TYPE_PHYSICAL, DamageResultEnum.DAMAGE_TEXT_CRITICAL));
                    Unit.Game.Send(new NextAutoattackMessage(Unit.NetId, TargetUnit.NetId, Unit.Game.NetIdProvider.PopNextNetId(), true, true));
                    test = 0;
                }
            }
            else
            {
                test = 0;
            }
        }
        public void OnTargetReach()
        {
            Unit.StopMove(false);
            /*  client.Hero.AttentionPing(targetPosition, target.NetId, PingTypeEnum.Ping_OnMyWay); */
            Unit.Game.Send(new BeginAutoAttackMessage(Unit.NetId, TargetUnit.NetId, 0x80, 0, false, TargetUnit.Position, Unit.Position, Unit.Game.Map.Record.MiddleOfMap));
            TargetUnit.InflictDamages(new Damages(Unit, TargetUnit, 12, DamageType.DAMAGE_TYPE_PHYSICAL, DamageResultEnum.DAMAGE_TEXT_CRITICAL));
            IsAttacking = true;

        }
        public void UnsetTarget()
        {
            Unit.Game.Send(new StopAutoAttackMessage(Unit.NetId));
            IsAttacking = false;
            TargetUnit = null;
        }
        public void DefineTarget(AIUnit target)
        {
            this.TargetUnit = target;
        }
    }
}
