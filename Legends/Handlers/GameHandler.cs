using Legends.Core.Cryptography;
using Legends.Core.Protocol;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.LoadingScreen;
using Legends.Core.Protocol.Messages.Game;
using Legends.Core.Protocol.Other;
using Legends.Network;
using Legends.World.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Handlers
{
    class GameHandler
    {
        /// <summary>
        /// There is a bug while encoding message, we receive all good, but are sending the wrong way, mb
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler(PacketCmd.PKT_ChatBoxMessage, Channel.CHL_COMMUNICATION)]
        public static void HandleChatBoxMessage(ChatBoxMessage message, LoLClient client)
        {
            if (message.content.StartsWith(CommandsManager.COMMANDS_PREFIX))
            {
                CommandsManager.Instance.Handle(client, message.content);
            }
            else
            {
                switch (message.channel)
                {
                    case ChatChannelType.ALL:
                        client.Player.Game.Send(message, Channel.CHL_COMMUNICATION);
                        break;
                    case ChatChannelType.TEAM:
                        client.Player.Team.Send(message, Channel.CHL_COMMUNICATION);
                        break;
                }
            }
        }
        [MessageHandler(PacketCmd.PKT_C2S_StartGame)]
        public static void HandleStartGameRequestMessage(StartGameRequestMessage message, LoLClient client)
        {
            client.Send(new StartGameMessage(0));

            client.Player.Team.Send(new EnterVisionMessage(true, client.Player.NetId, client.Player.Position, client.Player.WaypointsCollection.WaypointsIndex, client.Player.WaypointsCollection.GetWaypoints(), client.Player.Game.Map.Record.MiddleOfMap));

            float gameTime = client.Player.Game.GameTime / 1000f;
            client.Send(new GameTimerMessage(0, gameTime));
            client.Send(new GameTimerUpdateMessage(0, gameTime));

            //client.Send(new FogUpdate2Message(client.Player.NetId, NetIdProvider.PopNextNetId()));
        }
        [MessageHandler(PacketCmd.PKT_C2S_CharLoaded, Channel.CHL_C2S)]
        public static void HandleCharLoadedMessage(CharLoadedMessage message, LoLClient client)
        {
            client.Player.ReadyToSpawn = true;
            client.Player.NetId = NetIdProvider.PopNextNetId(); // start timeout timer
            client.Player.Position = client.Player.Game.Map.GetStartPosition(client.Player);

            if (client.Player.Game.CanStart)
            {
                client.Player.Game.Start();
            }

        }
        [MessageHandler(PacketCmd.PKT_C2S_ViewReq)]
        public static void HandleViewRequestMessage(ViewRequestMessage message, LoLClient client)
        {
            byte requestNo = 0x00;

            if (message.requestNo == 0xFE)
            {
                requestNo = 0xFF;
            }
            else
            {
                requestNo = message.requestNo;
            }

            client.Send(new ViewMessage(requestNo));
        }

        [MessageHandler(PacketCmd.PKT_C2S_LockCamera)]
        public static void HandleLockCameraRequestMessage(LockCameraRequestMessage message, LoLClient client)
        {

        }
        [MessageHandler(PacketCmd.PKT_C2S_MoveConfirm, Channel.CHL_LOW_PRIORITY)]
        public static void HandleMovementConfirmationMessage(MovementConfirmationMessage message, LoLClient client)
        {

        }
        [MessageHandler(PacketCmd.PKT_C2S_SkillUp)]
        public static void HandleSkillUpRequestMessage(SkillUpRequestMessage message, LoLClient client)
        {

        }
        [MessageHandler(PacketCmd.PKT_C2S_MoveReq)]
        public static void HandleMovementRequestMessage(MovementRequestMessage message, LoLClient client)
        {

            switch (message.type)
            {
                case MovementType.EMOTE:
                    break;
                case MovementType.MOVE:

                    WaypointsReader wayPointsReader = new WaypointsReader(message.moveData, message.coordCount, client.Player.Game.Map.Size);

                    client.Player.Invoke(new Action(() =>
                    {
                        client.Player.WaypointsCollection.SetWaypoints(wayPointsReader.Waypoints);

                        client.Player.SendVision(new MovementAnswerMessage(0, Environment.TickCount, client.Player.WaypointsCollection.GetWaypoints(), client.Player.NetId,
                        client.Player.Game.Map.Size), Channel.CHL_LOW_PRIORITY);

                    }));




                    break;
                case MovementType.ATTACK:
                    break;
                case MovementType.ATTACKMOVE:
                    break;
                case MovementType.STOP:

                    break;
                default:
                    break;




            }

        }
    }
}
