using Legends.Core.Geometry;
using Legends.Core.Protocol;
using Legends.Core.Protocol.Game;
using Legends.Records;
using Legends.World.Entities.AI.BasicAttack;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public abstract class AIUnit : AttackableUnit
    {
        public PathManager PathManager
        {
            get;
            private set;
        }
        public AttackManager AttackManager
        {
            get;
            private set;
        }
        public AIStats AIStats
        {
            get
            {
                return (AIStats)Stats;
            }
        }
        public AIUnitRecord Record
        {
            get;
            protected set;
        }
        public float AttackRange
        {
            get
            {
                return AIStats.AttackRange.Total;
            }
        }
        public override bool IsMoving => PathManager.IsMoving;

        public abstract bool IsAttackAutomatic
        {
            get;
        }
        public virtual void OnTargetSet(AttackableUnit target)
        {

        }
        public virtual void OnTargetUnset(AttackableUnit target)
        {

        }
        public AIUnit()
        {

        }

        public override void Initialize()
        {
            if (Record.IsMelee)
            {
                AttackManager = new MeleeManager(this, IsAttackAutomatic);
            }
            else
            {
                AttackManager = new RangedManager(this, IsAttackAutomatic);
            }

            PathManager = new PathManager(this);
            base.Initialize();
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            PathManager.Update(deltaTime);
            this.AttackManager.Update(deltaTime);
        }
        public void Move(List<Vector2> waypoints, bool unsetTarget = true)
        {
            if (AIStats.MoveSpeed.Total > 0)
            {
                if (unsetTarget)
                    AttackManager.StopAttackTarget();

                PathManager.Move(waypoints);
                OnMove();
            }

        }
        public virtual void OnMove()
        {
            SendVision(new MovementAnswerMessage(0, PathManager.GetWaypoints(), NetId, Game.Map.Size), Channel.CHL_LOW_PRIORITY);
        }
        public void MoveTo(Vector2 targetVector, bool unsetTarget = true)
        {
            Move(new List<Vector2>() { Position, targetVector }, unsetTarget);
        }
        public void StopMove(bool unsetTarget = true)
        {
            Move(new List<Vector2>() { Position }, unsetTarget);
        }
        public float GetAutoattackRange(AIUnit target)
        {
            return AIStats.AttackRange.Total + (AIStats.AttackRange.Total * (float)Record.ChasingAttackRangePercent) + ((float)target.Record.SelectionRadius * target.AIStats.ModelSize.Total);
        }
        public float GetAutoattackRangeWhileChasing(AIUnit target)
        {
            return AIStats.AttackRange.Total + ((float)target.Record.SelectionRadius * target.AIStats.ModelSize.Total);
        }
        /// <summary>
        /// Try to autoattack target, if the unit dont have range, it follows target
        /// </summary>
        /// <param name="targetUnit"></param>
        public void TryAutoattack(AIUnit targetUnit)
        {
            if (!targetUnit.Alive)
            {
                return;
            }
            if (this.GetDistanceTo(targetUnit) <= GetAutoattackRange(targetUnit))
            {
                if (IsMoving)
                {
                    StopMove();
                }
                AttackManager.BeginAttackTarget(targetUnit);
            }
            else
            {
                if (AIStats.MoveSpeed.Total > 0)
                {
                    Action onTargetReach = new Action(() => { TryAutoattack(targetUnit); }); // recursive call 
                    PathManager.MoveToTarget(targetUnit, onTargetReach, GetAutoattackRangeWhileChasing(targetUnit));
                    OnMove();
                }
            }

        }
    }
}
