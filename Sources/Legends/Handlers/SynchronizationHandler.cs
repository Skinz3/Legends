using Legends.Core.Protocol;
using Legends.Core.Protocol.LoadingScreen;
using Legends.Core.Protocol.Messages.Game;
using Legends.Core.Utils;
using Legends.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    class SynchronizationHandler
    {
        static Logger logger = new Logger();

        [MessageHandler(PacketCmd.PKT_C2S_Ping_Load_Info, Channel.CHL_C2S)]
        public static void HandlePingLoadInfoMessage(PingLoadInfoMessage message, LoLClient client)
        {
            client.Player.Game.Send(new PingLoadInfoAnswerMessage()
            {
                netId = message.netId,
                loaded = message.loaded,
                ping = message.ping,
                unk1 = message.unk1, 
                unk2 = message.unk2,
                unk3 = message.unk3,
                unk4 = message.unk4,
                userId = client.UserId.Value,
            }, Channel.CHL_LOW_PRIORITY, ENet.PacketFlags.None);
        }
        [MessageHandler(PacketCmd.PKT_C2S_HeartBeat)]
        public static void HandleHeartBeat(HeartBeatMessage message,LoLClient client)
        {
            if (message.receiveTime > message.ackTime)
            {
                var diff = message.ackTime - message.receiveTime;

                var msg = $"Player {client.Player.Name} sent an invalid heartbeat - Timestamp error (diff: {diff})";
                logger.Write(msg, MessageState.WARNING);
            }

        }
    }
}
