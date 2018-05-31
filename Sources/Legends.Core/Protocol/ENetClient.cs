using ENet;
using Legends.Core.Cryptography;
using Legends.Core.IO;
using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ENet.Native;

namespace Legends.Core.Protocol
{
    public abstract unsafe class ENetClient
    {
        protected ENetPeer* Peer
        {
            get;
            set;
        }
        public abstract string Ip
        {
            get;
        }

        public ENetClient(ENetPeer* peer)
        {
            this.Peer = peer;
        }

        public abstract void Disconnect();
    }
}
