using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol
{
    /// <summary>
    /// Following League Of Legends architecture
    /// </summary>
    public abstract class StateMessage : BaseMessage
    {
        public StateMessage(uint netId) : base(netId)
        {

        }
        public StateMessage()
        {

        }
       
    }
}
