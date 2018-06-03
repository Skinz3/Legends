using Legends.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Network
{
    public class NetIdProvider
    {
        private const int DEFAULT_NET_ID = 0x40000000;

        private object locker = new object();

        private int LastNetId
        {
            get;
            set;
        }
        public NetIdProvider()
        {
            this.LastNetId = DEFAULT_NET_ID;
        }

        public int PopNextNetId()
        {
            lock (locker)
            {
                LastNetId++;
                return LastNetId;
            }
        }
    }
}
