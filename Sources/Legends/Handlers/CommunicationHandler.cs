using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Network;
using Legends.World.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    class CommunicationHandler
    {
        [MessageHandler(PacketCmd.PKT_C2S_AttentionPing)]
        public static void HandleAttentionPingRequestMessage(AttentionPingRequestMessage message,LoLClient client)
        {
            client.Hero.AttentionPing(message.position, message.targetNetId, message.pingType);
        }
        [MessageHandler(PacketCmd.PKT_ChatBoxMessage, Channel.CHL_COMMUNICATION)]
        public static void HandleChatBoxMessage(ChatBoxMessage message, LoLClient client)
        {
            if (message.content.StartsWith(CommandsProvider.COMMANDS_PREFIX))
            {
                CommandsProvider.Instance.Handle(client, message.content);
            }
            else
            {
                switch (message.channel)
                {
                    case ChatChannelType.ALL:
                        client.Hero.Game.Send(message, Channel.CHL_COMMUNICATION);
                        break;
                    case ChatChannelType.TEAM:
                        client.Hero.Team.Send(message, Channel.CHL_COMMUNICATION);
                        break;
                }
            }
        }
    }
}
