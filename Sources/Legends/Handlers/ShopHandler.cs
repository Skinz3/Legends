using Legends.Core.Protocol;
using Legends.Network;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    public class ShopHandler
    {
        public const float ITEM_SELL_SHOP_RATIO = 0.70f;

        [MessageHandler(PacketCmd.PKT_C2S_BuyItemReq)]
        public static void HandleBuyItemRequestMessage(BuyItemRequestMessage message, LoLClient client)
        {
            ItemRecord itemRecord = ItemRecord.GetItemRecord(message.itemId);
            if (client.Hero.Stats.Gold >= itemRecord.Price)
            {
                Item itemResult = client.Hero.Inventory.AddItem(itemRecord);

                if (itemResult != null)
                {
                    client.Hero.Stats.RemoveGold(itemRecord.Price);
                    client.Hero.UpdateStats();

                }
                else
                {
                    //on envoie un message pour dire que l'inventaire est plein 
                }
            }
        }
        [MessageHandler(PacketCmd.PKT_C2S_SellItem)]
        public static void HandleSellItem(SellItemMessage message, LoLClient client)
        {
            Item itemRemoved = client.Hero.Inventory.RemoveItem(message.slotId);
            client.Hero.Stats.AddGold(itemRemoved.Record.Price * ITEM_SELL_SHOP_RATIO);
            client.Hero.UpdateStats();

        }
    }
}
