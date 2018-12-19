using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Deaths
{
    public abstract class Death<T> : IUpdatable where T : AttackableUnit
    {
        protected T Unit
        {
            get;
            private set;
        }
        public float TimeLeft
        {
            get;
            private set;
        }
        public float TimeLeftSeconds
        {
            get
            {
                return TimeLeft / 1000f;
            }
        }
        public Death(T unit)
        {
            this.Unit = unit;
        }
        public virtual void Update(long deltaTime)
        {
            if (!Unit.Alive)
            {
                TimeLeft -= deltaTime;

                if (TimeLeft <= 0)
                {
                    Revive();
                }
            }
        }
        public virtual void Revive()
        {
            Unit.OnRevive(Unit);
            TimeLeft = 0;
        }
        public virtual void OnDead()
        {
            TimeLeft = (GetTimeLeftSeconds()) * 1000f;
        }

        protected abstract float GetTimeLeftSeconds();
    }
}
