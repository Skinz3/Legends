using Legends.Core.Geometry;
using Legends.Core.Protocol;
using Legends.Core.Protocol.Game;
using Legends.Records;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
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
        public Action<Path> OnPathChanged;

        public Path Path
        {
            get;
            private set;
        }
        public AutoattackManager AutoattackUpdater
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
                return AIStats.AttackRange.Total + 150; // + 150?
            }
        }
        public override bool IsMoving => Path.IsMoving;

        public abstract bool Autoattack
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
            this.AutoattackUpdater = new AutoattackManager(this, Autoattack);
            this.Path = new Path(this);
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            Path.Update(deltaTime);
            this.AutoattackUpdater.Update(deltaTime);
        }
        public void Move(Path path, bool unsetTarget = true)
        {
            if (unsetTarget)
                AutoattackUpdater.UnsetTarget();

            Path = path;
            SendVision(new MovementAnswerMessage(0, Path.GetWaypoints(), NetId, Game.Map.Size), Channel.CHL_LOW_PRIORITY);
            OnPathChanged?.Invoke(Path);

        }
        public void MoveTo(Vector2 targetVector, bool unsetTarget = true)
        {
            Move(new Path(this, targetVector), unsetTarget);
        }


        public void MoveToAutoattack(AIUnit targetUnit)
        {
            AutoattackUpdater.DefineTarget(targetUnit);

            float distanceToTarget = AIStats.AttackRange.Total +
                ((float)targetUnit.Record.SelectionRadius * targetUnit.AIStats.ModelSize.Total);

            if (this.GetDistanceTo(targetUnit) < distanceToTarget)
            {
                AutoattackUpdater.OnTargetReach();
            }
            else
            {
                if (AIStats.MoveSpeed.Total > 0)
                    Move(new Path(this, targetUnit, distanceToTarget), false);
            }

        }
        public void StopMove(bool unsetTarget = true)
        {
            Move(new Path(this), unsetTarget);
        }

    }
}
