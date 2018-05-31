using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Network
{
    class NetIdProvider
    {
        private static int dwStart = 0x40000000; //new netid

        private static object locker = new object();

        public static int PopNextNetId()
        {
            lock (locker)
            {
                dwStart++;
                return dwStart;
            }
        }
    }
}
