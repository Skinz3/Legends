using Legends.Core.DesignPattern;
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
        public const float ITEM_SELL_SHOP_RATIO_DEFAULT = 0.70f;

        [MessageHandler(PacketCmd.PKT_C2S_BuyItemReq)]
        public static void HandleBuyItemRequestMessage(BuyItemRequestMessage message, LoLClient client)
        {
            ItemRecord itemRecord = ItemRecord.GetItemRecord(message.itemId);

            if (client.Hero.Stats.Gold >= itemRecord.Price)
            {
                int[] recipePrices = client.Hero.Inventory.RemoveRecipeItem(itemRecord);
                Item itemResult = client.Hero.Inventory.AddItem(itemRecord);

                if (itemResult != null)
                {
                    int priceTotal = itemRecord.GetTotalPrice();

                    foreach (var price in recipePrices)
                    {
                        priceTotal -= price;
                    }

                    client.Hero.RemoveGold(priceTotal);
                    client.Hero.UpdateStats();

                }
            }
        }
        [MessageHandler(PacketCmd.PKT_C2S_SellItem)]
        public static void HandleSellItemMessage(SellItemMessage message, LoLClient client)
        {
            Item itemRemoved = client.Hero.Inventory.RemoveItem(message.slotId);
            float sellRatio = itemRemoved.Record.SellBackModifier > 0 ? itemRemoved.Record.SellBackModifier : ITEM_SELL_SHOP_RATIO_DEFAULT;
            client.Hero.AddGold(itemRemoved.Record.GetTotalPrice() * sellRatio, false);
            client.Hero.UpdateStats();

        }
        [MessageHandler(PacketCmd.PKT_C2S_SwapItems)]
        public static void HandleSwapItemRequestMessage(SwapItemRequestMessage message, LoLClient client)
        {
            client.Hero.Inventory.SwapItems(message.slotFrom, message.slotTo);
            client.Hero.Game.Send(new SwapItemAnswerMessage(client.Hero.NetId, message.slotFrom, message.slotTo));
        }
    }
}
