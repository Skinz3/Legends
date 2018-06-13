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

        [InDevelopment(InDevelopmentState.STARTED, "Something wrong with gold... RemoveGold = Price + All Item Recipe cost price")]
        [MessageHandler(PacketCmd.PKT_C2S_BuyItemReq)]
        public static void HandleBuyItemRequestMessage(BuyItemRequestMessage message, LoLClient client)
        {
            ItemRecord itemRecord = ItemRecord.GetItemRecord(message.itemId);
       
            if (client.Hero.Stats.Gold >= itemRecord.Price)
            {
                Item[] recipeItems = client.Hero.Inventory.RemoveRecipeItem(itemRecord);
                Item itemResult = client.Hero.Inventory.AddItem(itemRecord);

                if (itemResult != null)
                {
                    int price = itemRecord.GetTotalPrice();

                    foreach (var item in recipeItems)
                    {
                        price -= item.Record.GetTotalPrice();
                    }

                    client.Hero.Stats.RemoveGold(price);
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
            float sellRatio = itemRemoved.Record.SellBackModifier > 0 ? itemRemoved.Record.SellBackModifier : ITEM_SELL_SHOP_RATIO_DEFAULT;
            client.Hero.Stats.AddGold(itemRemoved.Record.GetTotalPrice() * sellRatio);
            client.Hero.UpdateStats();

        }
    }
}
