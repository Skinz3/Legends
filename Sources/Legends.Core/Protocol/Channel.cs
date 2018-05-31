using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol
{   //#define CHL_MAX = 7
    public enum Channel : byte
    {
        CHL_HANDSHAKE = 0,
        CHL_C2S = 1,
        CHL_GAMEPLAY = 2,
        CHL_S2C = 3,
        CHL_LOW_PRIORITY = 4,
        CHL_COMMUNICATION = 5,
        CHL_LOADING_SCREEN = 7,
    };
}
