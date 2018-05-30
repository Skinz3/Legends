using Legends.Core.Protocol.Enum;
using Legends.World.Entities.Movements;
using Legends.World.Games;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities
{
    public abstract class Unit
    {
        public int NetId
        {
            get;
            set;
        }
        public Team Team
        {
            get;
            private set;
        }
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// Numero de l'unitée dans son équipe.
        /// </summary>
        public int TeamNo
        {
            get;
            set;
        }
        public abstract bool IsMoving
        {
            get;
        }
        private List<Action> SynchronizedActions
        {
            get;
            set;
        }
        public Unit()
        {
          
            VisibleUnit = new List<Unit>();
            SynchronizedActions = new List<Action>();
        }
       
        public Vector2 Position
        {
            get;
            set;
        }
  
        public List<Unit> VisibleUnit
        {
            get;
            private set;
        }
        public Game Game
        {
            get;
            private set;
        }
        public abstract float PerceptionBubbleRadius
        {
            get;
        }

        public virtual void Initialize()
        {

        }

        public void DefineTeam(Team team)
        {
            this.Team = team;
        }
        public void DefineGame(Game game)
        {
            this.Game = game;
        }
        public void Invoke(Action action)
        {
            SynchronizedActions.Add(action);
        }
        public virtual void Update(long deltaTime)
        {
            for (int i = 0; i < SynchronizedActions.Count; i++)
            {
                SynchronizedActions[i].Invoke();
            }
         
            SynchronizedActions.Clear();
        }
        public abstract void OnUnitEnterVision(Unit unit);

        public abstract void OnUnitLeaveVision(Unit unit);

        public Team GetOposedTeam()
        {
            return Team.Id == TeamId.BLUE ? Game.PurpleTeam : Game.BlueTeam;
        }
        public bool InFieldOfView(Unit other)
        {
            var dist = this.GetDistanceTo(other);
            return dist <= PerceptionBubbleRadius;
        }
        public float GetDistanceTo(Unit other)
        {
            return (float)Math.Sqrt(Math.Pow(other.Position.X - Position.X, 2) + Math.Pow(other.Position.Y - Position.Y, 2));
        }
        public bool HasVision(Unit other)
        {
            return VisibleUnit.Contains(other) || other.Team == this.Team;
        }

    }
}
