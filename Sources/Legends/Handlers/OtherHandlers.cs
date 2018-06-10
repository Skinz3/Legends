using Legends.Core.Protocol;
using Legends.Network;
using Legends.Protocol.GameClient.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    class OtherHandlers
    {
        [MessageHandler(PacketCmd.PKT_C2S_Exit)]
        public static void HandleExit(ExitMessage message, LoLClient client)
        {
            client.OnDisconnect();
        }
    }
}
