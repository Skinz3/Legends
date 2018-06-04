using Legends.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Time
{
    public class HighResolutionTimer
    {
        private delegate void TimerEventDel(int id, int msg, IntPtr user, int dw1, int dw2);
        private const int TIME_PERIODIC = 1;
        private const int EVENT_TYPE = TIME_PERIODIC;// + 0x100;  // TIME_KILL_SYNCHRONOUS causes a hang ?!
        [DllImport("winmm.dll")]
        private static extern int timeBeginPeriod(int msec);
        [DllImport("winmm.dll")]
        private static extern int timeEndPeriod(int msec);
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerEventDel handler, IntPtr user, int eventType);
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);

        public event Action Elapsed;
        private int mTimerId;
        private TimerEventDel mHandler;  // NOTE: declare at class scope so garbage collector doesn't release it!!!

        public int Interval
        {
            get;
            private set;
        }
        public HighResolutionTimer(int interval)
        {
            Interval = interval;
            timeBeginPeriod(1);
            mHandler = new TimerEventDel(TimerCallback);
         
        }

        [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT,"Thread.Sleep() still necessary?")]
        public void Stop()
        {
            int err = timeKillEvent(mTimerId);
            timeEndPeriod(1);
            System.Threading.Thread.Sleep(100);// Ensure callbacks are drained
        }
        public void Start()
        {
            mTimerId = timeSetEvent(Interval, 0, mHandler, IntPtr.Zero, EVENT_TYPE);
        }
        private void TimerCallback(int id, int msg, IntPtr user, int dw1, int dw2)
        {
            Elapsed();
        }
    }
}
