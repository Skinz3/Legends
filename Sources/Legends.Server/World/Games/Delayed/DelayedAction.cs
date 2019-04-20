using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games.Delayed
{
    public class DelayedAction : IUpdatable
    {
        public bool Finalized
        {
            get;
            set;
        }

        private Action action;
        private float delayCurrent;

        public DelayedAction(Action action, float delay)
        {
            this.action = action;
            this.delayCurrent = delay;
            this.Finalized = false;
        }
        public void Cancel()
        {
            Finalized = true;
        }
        public void Update(float deltaTime)
        {
            if (!Finalized)
            {
                delayCurrent -= deltaTime;
                if (delayCurrent <= 0)
                {

                    action();
                    Finalized = true;
                }
            }
            else
            {
                Console.WriteLine("Need diposition");
            }
        }
    }
}
