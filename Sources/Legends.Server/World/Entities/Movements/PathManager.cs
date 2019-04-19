using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Types;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Movements
{
    public class PathManager : IUpdatable
    {
        public static float TARGET_DETECTION_OFFSET = 1f;

        public int WaypointsIndex
        {
            get;
            private set;
        }
        public Vector2? NextPosition
        {
            get
            {
                if (Waypoints.Count < 2 || WaypointsIndex == Waypoints.Count)
                {
                    return null;
                }
                else
                {
                    return Waypoints[WaypointsIndex];
                }
            }
        }
        /// <summary>
        /// Direction will be equals to Vector2.NaN if TargetPosition = Position because we Vector2.Normalize(0,0);
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                return Vector2.Normalize(NextPosition.Value - Unit.Position);

            }
        }
        private List<Vector2> Waypoints
        {
            get;
            set;
        }
        public Vector2? PendingPoint
        {
            get;
            set;
        }
        public bool MovePending
        {
            get
            {
                return PendingPoint != null;
            }
        }
        private AttackableUnit TargetUnit
        {
            get;
            set;
        }
        private Action OnTargetReachAction
        {
            get;
            set;
        }
        private float DistanceToTarget
        {
            get;
            set;
        }
        private AIUnit Unit
        {
            get;
            set;
        }

        public bool IsMoving => NextPosition != null && NextPosition != Unit.Position;

        private bool End = false;

        public PathManager(AIUnit unit)
        {
            this.Unit = unit;
            this.Waypoints = new List<Vector2>() { Unit.Position };
            this.OnTargetReachAction = null;
            this.DistanceToTarget = 0f;
            this.TargetUnit = null;
            End = false;
        }
        public void Move(List<Vector2> waypoints)
        {
            this.Waypoints = waypoints;
            this.WaypointsIndex = 1;
            this.TargetUnit = null;
            this.OnTargetReachAction = null;
            this.DistanceToTarget = 0f;
            End = false;
        }
        [InDevelopment(InDevelopmentState.TODO, "Use A* to calculate Waypoints")]
        /// <summary>
        /// Only use for auto attack
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="target"></param>
        public void MoveToTarget(AttackableUnit target, Action onTargetReach, float distanceToTarget = 0)
        {
            this.TargetUnit = target;
            this.WaypointsIndex = 1;
            this.DistanceToTarget = distanceToTarget;
            this.OnTargetReachAction = onTargetReach;
            Vector2 targetPosition = target.Position;

            if (distanceToTarget > 0)
                targetPosition = Geo.GetPointOnCircle(target.Position, target.GetAngleBetween(Unit), distanceToTarget);

            Waypoints = new List<Vector2>() { Unit.Position, targetPosition }; // A* right here
            End = false;
        }

        public void Reset()
        {
            PendingPoint = null;
            this.Waypoints = new List<Vector2>() { Unit.Position };
            this.OnTargetReachAction = null;
            this.DistanceToTarget = 0f;
            this.TargetUnit = null;
            End = false;
        }

        public bool MoveToPendingPoint()
        {
            if (Unit.PathManager.PendingPoint != null)
            {
                Unit.MoveTo(Unit.PathManager.PendingPoint.Value);
                return true;
            }
            return false;
        }
        public Vector2[] GetWaypoints()
        {
            return Waypoints.ToArray();
        }
        /// <summary>
        /// Cells coords
        /// </summary>
        /// <returns></returns>
        public GridPosition[] GetWaypointsTranslated()
        {
            List<GridPosition> result = new List<GridPosition>();
            var mapSize = Unit.Game.Map.Size;
            var halfCellSize = Unit.Game.Map.Record.HalfCellSize;
            var wayPoints = GetWaypoints();

            result.Add(GridPosition.TranslateToGrid(Unit.Position, mapSize, halfCellSize));

            for (int i = WaypointsIndex; i < wayPoints.Length; i++)
            {
                result.Add(GridPosition.TranslateToGrid(wayPoints[i], mapSize, halfCellSize));
            }
            return result.ToArray();
        }
        /// <summary>
        /// Créer une interpolation entre un point a et b, avec a la position actuelle
        /// et b le prochain noeud du pathfinding (waypoints).
        /// On peut se permettre d'utiliser  Client.Send(new HeroSpawn2Message(NetId, Position.X, Position.Y));
        /// si l'on envoit pas MovementAnswerMessage pour pouvoir tester la qualitée de notre interpolation.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void InterpolateMovement(float deltaTime)
        {
            if (IsMoving && !End)
            {
                float deltaMovement = Unit.Stats.MoveSpeed.TotalSafe * 0.001f * deltaTime; // deltaTime

                float xOffset = Direction.X * deltaMovement * 1.00f;
                float yOffset = Direction.Y * deltaMovement * 1.00f;

                Unit.Position = new Vector2(Unit.Position.X + xOffset, Unit.Position.Y + yOffset);

                if (TargetUnit != null)
                {
                    if (TargetUnit.GetDistanceTo(Unit) <= DistanceToTarget)
                    {
                        OnTargetReachAction();
                        End = true;
                        return;

                    }
                }


                if (Math.Abs(Unit.Position.X - NextPosition.Value.X) <= Math.Abs(xOffset) && Math.Abs(Unit.Position.Y - NextPosition.Value.Y) <= Math.Abs(yOffset))
                {
                    Unit.Position = NextPosition.Value;


                    WaypointsIndex++;

                    if (WaypointsIndex == Waypoints.Count)
                    {
                        if (TargetUnit != null)
                        {
                            End = true;
                            OnTargetReachAction();
                        }


                    }
                }
            }

        }
        public override string ToString()
        {
            return string.Join(",", Waypoints) + " Index is : " + WaypointsIndex;
        }
        public void Update(float deltaTime)
        {
            InterpolateMovement(deltaTime);
        }
    }
}
