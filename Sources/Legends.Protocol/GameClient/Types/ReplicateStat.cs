using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class ReplicateStat
    {
        public uint Value
        {
            get; set;
        }
        public bool IsFloat
        {
            get; set;
        }
        public bool Changed
        {
            get; set;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
