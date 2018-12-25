using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public interface IProtocolable<T> where T : class
    {
        T GetProtocolObject();
    }
}
