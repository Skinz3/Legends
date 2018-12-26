using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Time
{
    public class UpdateTimer
    {
        private float interval;
        private float intervalCurrent;
        private bool running;

        private bool finished;

        /// <summary>
        /// Temps restant, en secondes
        /// </summary>
        public float Current
        {
            get
            {
                return intervalCurrent / 1000f;
            }
            set
            {
                intervalCurrent = value * 1000f;
            }
        }
        public UpdateTimer(float interval)
        {
            this.interval = interval;
            this.intervalCurrent = interval;
            this.running = false;
        }
        public void Update(float deltaTime)
        {
            if (running)
            {
                finished = false;
                intervalCurrent -= deltaTime;

                if (intervalCurrent <= 0)
                {
                    finished = true;
                    intervalCurrent = interval;
                }
            }
        }
        public void Start()
        {
            running = true;
        }
        public bool Finished()
        {
            return finished;
        }
        public void Pause()
        {
            running = false;
        }
    }
}
