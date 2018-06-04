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
        public override bool IsMoving => Path.IsMoving;


        public AIUnit()
        {
            this.AutoattackUpdater = new AutoattackManager(this);
            this.Path = new Path(this);
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            Path.Update(deltaTime);
            this.AutoattackUpdater.Update(deltaTime);
        }
        public void Move(Path path)
        {
            Path = path;
            SendVision(new MovementAnswerMessage(0, Path.GetWaypoints(), NetId, Game.Map.Size), Channel.CHL_LOW_PRIORITY);
            OnPathChanged?.Invoke(Path);

        }
        public void MoveTo(Vector2 targetVector)
        {
            Move(new Path(this, targetVector));
        }
     

        public void MoveToAutoattack(AIUnit targetUnit)
        {
            float distanceToTarget = AIStats.AttackRange.Total +
                ((float)targetUnit.Record.SelectionRadius * targetUnit.AIStats.ModelSize.Total);

            Move(new Path(this, targetUnit, distanceToTarget));

        }
        public void StopMove()
        {
            Move(new Path(this));
        }

    }
}
