using Legends.Core.Protocol;
using Legends.Network;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    public class ShopHandler
    {
        [MessageHandler(PacketCmd.PKT_C2S_BuyItemReq)]
        public static void HandleBuyItemRequestMessage(BuyItemRequestMessage message, LoLClient client)
        {
             
        }
    }
}
