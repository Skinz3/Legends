using Legends.Core.DesignPattern;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Movements
{
    public class DashManager
    {
        private Dash Dash
        {
            get;
            set;
        }
        public bool IsDashing
        {
            get
            {
                return Dash != null;
            }
        }
        public AIUnit Unit
        {
            get;
            set;
        }
        public DashManager(AIUnit unit)
        {
            this.Unit = unit;
        }
        public void Update(float deltaTime)
        {
            if (IsDashing)
            {
                float deltaMovement = Dash.Speed * 0.001f * deltaTime;

                float xOffset = Dash.Direction.X * deltaMovement;
                float yOffset = Dash.Direction.Y * deltaMovement;

                Unit.Position = new Vector2(Unit.Position.X + xOffset, Unit.Position.Y + yOffset);

                if (Math.Abs(Unit.Position.X - Dash.TargetPoint.X) <= Math.Abs(xOffset) && Math.Abs(Unit.Position.Y - Dash.TargetPoint.Y) <= Math.Abs(yOffset))
                {
                    Unit.Position = Dash.TargetPoint;
                    var onEnded = Dash.OnDashEnded;
                    Dash = null;

                    Unit.PathManager.MoveToPendingPoint();
                    onEnded?.Invoke();
                }
            }
        }

        public void StartDashing(Vector2 targetPoint, float speed, bool facing, Action onDashEnded)
        {
            this.Dash = new Dash(Unit.Position, targetPoint, speed, facing, onDashEnded);
        }
        public void CancelDash()
        {
            if (Dash != null)
            {
                Dash.OnDashEnded?.Invoke(); // on canceled should be different?
                Dash = null;
            }
        }

        public Dash GetDash()
        {
            return Dash;
        }
    }
    [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "Make a struct?")]
    public class Dash
    {
        public Vector2 Direction
        {
            get
            {
                return Vector2.Normalize(TargetPoint - StartPoint);
            }
        }
        public Vector2 StartPoint
        {
            get;
            set;
        }
        public Vector2 TargetPoint
        {
            get;
            set;
        }
        public float Speed
        {
            get;
            set;
        }
        public Action OnDashEnded
        {
            get;
            set;
        }
        public bool Facing
        {
            get;
            set;
        }

        public Dash(Vector2 startPoint, Vector2 targetPoint, float speed, bool facing, Action onDashEnded)
        {
            this.StartPoint = startPoint;
            this.TargetPoint = targetPoint;
            this.Speed = speed;
            this.OnDashEnded = onDashEnded;
            this.Facing = facing;
        }
    }
}
