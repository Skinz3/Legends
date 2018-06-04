using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Legends.Core.Time
{
    public class ActionTimer
    {
        private Timer m_timer;
        private Action m_action;

        public ActionTimer(Action action, double delay)
        {
            m_action = action;
            m_timer = new Timer(delay);
            m_timer.AutoReset = false;
            m_timer.Elapsed += M_timer_Elapsed;
        }
        public void Start()
        {
            m_timer.Start();
        }
        public void Stop()
        {
            m_timer.Stop();
        }
        private void M_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_action();
            m_timer.Dispose();
        }

        public static void Execute(Action action, double delay)
        {
            new ActionTimer(action, delay).Start();
        }
    }
}
