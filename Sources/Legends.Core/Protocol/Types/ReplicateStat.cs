using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.Types
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
