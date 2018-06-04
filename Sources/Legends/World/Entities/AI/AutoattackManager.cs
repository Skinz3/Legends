using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Messages.Game;
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
        public AutoattackManager(AIUnit unit)
        {
            this.Unit = unit;
        }

        public void Update(long deltaTime)
        {

        }
        public void OnTargetReach()
        {
            /*  client.Hero.AttentionPing(targetPosition, target.NetId, PingTypeEnum.Ping_OnMyWay); */
            Unit.Game.Send(new BeginAutoAttackMessage(Unit.NetId, TargetUnit.NetId, 0x80, 0, false, TargetUnit.Position, Unit.Position, Unit.Game.Map.Record.MiddleOfMap));
        }
        public void UnsetTarget()
        {
            TargetUnit = null;
        }
        public void DefineTarget(AIUnit target)
        {
            this.TargetUnit = target;
        }
    }
}
