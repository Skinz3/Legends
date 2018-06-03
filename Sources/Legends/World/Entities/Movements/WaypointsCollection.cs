using Legends.Core.Protocol.Game;
using Legends.Network;
using Legends.World.Entities.AI;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Movements
{
    /// <summary>
    /// I think something smooth is missing here? lerp? when changin waypoints?
    /// </summary>
    public class WaypointsCollection
    {
        private List<Vector2> Waypoints
        {
            get;
            set;
        }
        public int WaypointsIndex
        {
            get;
            private set;
        }
        public Vector2? TargetPosition
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
                return Vector2.Normalize(TargetPosition.Value - Unit.Position);
            }
        }
        private AIUnit Unit
        {
            get;
            set;
        }
        public WaypointsCollection(AIUnit unit)
        {
            Unit = unit;
            Waypoints = new List<Vector2>();
            WaypointsIndex = 0;
        }
        public void SetWaypoints(List<Vector2> waypoints)
        {
            this.Waypoints = waypoints;
            this.WaypointsIndex = 1;
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
        public void InterpolateMovement(long deltaTime)
        {
            if (TargetPosition != null && TargetPosition != Unit.Position)
            {
                float deltaMovement = ((AIStats)Unit.Stats).MoveSpeed.Total * 0.001f * deltaTime; // deltaTime

                float xOffset = Direction.X * deltaMovement;
                float yOffset = Direction.Y * deltaMovement;

                Unit.Position = new Vector2(Unit.Position.X + xOffset, Unit.Position.Y + yOffset);


                if (Math.Abs(Unit.Position.X - TargetPosition.Value.X) <= Math.Abs(xOffset) && Math.Abs(Unit.Position.Y - TargetPosition.Value.Y) <= Math.Abs(yOffset))
                {
                    Unit.Position = TargetPosition.Value;
                    WaypointsIndex++;

                    if (WaypointsIndex == Waypoints.Count)
                    {
                        SetWaypoints(new List<Vector2>());
                    }
                }

                //  var p = ((AIHero)Unit);
                //  p.AttentionPing(p.Position, p.NetId, Core.Protocol.Enum.PingTypeEnum.Ping_OnMyWay);
                //   p.Game.Send(new EnterVisionMessage(true, p.NetId, p.Position, WaypointsIndex, GetWaypoints(), p.Game.Map.Record.MiddleOfMap));
            }
        }
        public override string ToString()
        {
            return string.Join(",", Waypoints) + " Index is : " + WaypointsIndex;
        }
    }
}
