using Legends.Core.DesignPattern;
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

        public abstract bool DefaultAutoattackActivated
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
                AttackManager = new MeleeManager(this);
            }
            else
            {
                AttackManager = new RangedManager(this);
            }
            AttackManager.SetAutoattackActivated(DefaultAutoattackActivated);
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
                {
                    AttackManager.StopAttackTarget();

                    if (AttackManager.IsAttacking && !AttackManager.CurrentAutoattack.Hit)
                    {
                        AttackManager.DestroyAutoattack();
                    }
                }

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
        [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT, "Not sure about values...check again in RAF?")]
        public float GetAutoattackRange(AIUnit target)
        {
            return AIStats.AttackRange.Total + (AIStats.AttackRange.Total * (float)Record.ChasingAttackRangePercent) + ((float)target.Record.SelectionRadius * target.AIStats.ModelSize.Total);
        }
        [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT, "Not sure about values...check again in RAF?")]
        public float GetAutoattackRangeWhileChasing(AIUnit target)
        {
            return AIStats.AttackRange.Total + ((float)target.Record.SelectionRadius * target.AIStats.ModelSize.Total);
        }
        [InDeveloppement(InDeveloppementState.TODO,"We need to use pathfinding only for melee to join target.")]
        /// <summary>
        /// On essaye d'auto attack une cible, Si elle est a portée on lance l'animation
        /// Sinon, on marche jusqu'a elle
        /// </summary>
        /// <param name="targetUnit"></param>
        public void TryBasicAttack(AIUnit targetUnit)
        {
            if (!targetUnit.Alive)
            {
                return;
            }
            if (this.GetDistanceTo(targetUnit) <= GetAutoattackRange(targetUnit))
            {
                if (IsMoving) // Si on est en mouvement, on s'arrête
                {
                    StopMove();
                }
                AttackManager.BeginAttackTarget(targetUnit); // on lance la première auto
            }
            else
            {
                if (AIStats.MoveSpeed.Total > 0)
                {
                    AttackManager.StopAttackTarget(); // on arrête d'attaquer l'éventuelle cible, car on va se déplacer.

                    if (AttackManager.IsAttacking && !AttackManager.CurrentAutoattack.Hit) // Si on a cancel l'auto avant que les dégats soit infligés, alors on peut la disposer.
                    {
                        AttackManager.DestroyAutoattack(); 
                    }

                    Action onTargetReach = new Action(() => { TryBasicAttack(targetUnit); }); // recursive call 
                    PathManager.MoveToTarget(targetUnit, onTargetReach, GetAutoattackRangeWhileChasing(targetUnit));
                    OnMove();
                }
            }

        }
    }
}
