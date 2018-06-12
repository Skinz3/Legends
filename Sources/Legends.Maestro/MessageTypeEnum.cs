using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Maestro
{
    public enum MessageTypeEnum
    {
        GAMESTART = 0,
        GAMEEND = 1,
        GAMECRASHED = 2,

        EXIT = 3,
        ACK = 5,
        HEARTBEAT = 4,

        GAMECLIENT_LAUNCHED = 8,
        GAMECLIENT_CONNECTED = 10,
        CHATMESSAGE_TO_GAME = 11,
        CHATMESSAGE_FROM_GAME = 12
    }
}
