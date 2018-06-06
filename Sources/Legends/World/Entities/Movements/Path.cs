using Legends.Core.Geometry;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Movements
{
    public class Path : IUpdatable
    {
        public event Action<Vector2> OnMoveEnd;
        public event Action<Vector2> OnPositionInterpolated;

        public int WaypointsIndex
        {
            get;
            private set;
        }
        /// <summary>
        /// Arrivé au waypoint final?
        /// </summary>
        private bool End
        {
            get;
            set;
        }
        public Vector2? NextPosition
        {
            get
            {
                if (Waypoints.Count < 2)
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
        private AttackableUnit TargetUnit
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

        public Path(AIUnit unit, List<Vector2> waypoints)
        {
            this.Unit = unit;
            this.Waypoints = waypoints;
            this.WaypointsIndex = 1;
            this.TargetUnit = null;
        }
        public Path(AIUnit unit, Vector2 targetPosition) : this(unit, new List<Vector2>() { unit.Position, targetPosition })
        {
           
        }
        public Path(AIUnit unit) : this(unit, new List<Vector2>() { unit.Position })
        {

        }
        /// <summary>
        /// Only use for auto attack
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="target"></param>
        public Path(AIUnit unit, AIUnit target, float distanceToTarget = 0)
        {
            this.Unit = unit;
            this.TargetUnit = target;
            this.WaypointsIndex = 1;

       

            Vector2 targetPosition = target.Position;

            if (distanceToTarget > 0)
                targetPosition = Geo.GetPointOnCircle(target.Position, target.GetAngleBetween(Unit), distanceToTarget);

            Waypoints = new List<Vector2>() { Unit.Position, targetPosition };

        }
        public Vector2[] GetWaypoints()
        {
            return Waypoints.ToArray();
        }
        /// <summary>
        /// Créer une interpolation entre un point a et b, avec a la position actuelle
        /// et b le prochain noeud du pathfinding (waypoints).
        /// On peut se permettre d'utiliser  Client.Send(new HeroSpawn2Message(NetId, Position.X, Position.Y));
        /// si l'on envoit pas MovementAnswerMessage pour pouvoir tester la qualitée de notre interpolation.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void InterpolateMovement(long deltaTime)
        {
            if (!End && IsMoving)
            {
                float deltaMovement = Unit.AIStats.MoveSpeed.Total * 0.001f * deltaTime; // deltaTime

                float xOffset = Direction.X * deltaMovement * 1.05f;
                float yOffset = Direction.Y * deltaMovement * 1.05f;

                Unit.Position = new Vector2(Unit.Position.X + xOffset, Unit.Position.Y + yOffset);
                OnPositionInterpolated?.Invoke(Unit.Position);

                if (Math.Abs(Unit.Position.X - NextPosition.Value.X) <= Math.Abs(xOffset) && Math.Abs(Unit.Position.Y - NextPosition.Value.Y) <= Math.Abs(yOffset))
                {
                    Unit.Position = NextPosition.Value;
                    WaypointsIndex++;

                    if (WaypointsIndex == Waypoints.Count)
                    {
                        if (TargetUnit != null)
                        {
                            Unit.AutoattackUpdater.OnTargetReach();
                        }
                        OnMoveEnd?.Invoke(Unit.Position);
                    //    Unit.StopMove();
                        End = true;
                    }
                }

                //     var p = ((AIHero)Unit); p.AttentionPing(p.Position, p.NetId, Core.Protocol.Enum.PingTypeEnum.Ping_OnMyWay);
            }
        }
        public override string ToString()
        {
            return string.Join(",", Waypoints) + " Index is : " + WaypointsIndex;
        }
        public void Update(long deltaTime)
        {
            InterpolateMovement(deltaTime);
        }
    }
}
