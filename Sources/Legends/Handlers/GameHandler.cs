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
        [MessageHandler(PacketCmd.PKT_C2S_Click, Channel.CHL_C2S)]
        public static void HandleClickMessage(ClickMessage message, LoLClient client)
        {
            var targetUnit = client.Hero.Game.Map.GetUnit(message.targetNetId);

            if (targetUnit != null)
            {
                string msg = "You clicked on {0} Position : {1} Distance to me : {2}";
                client.Hero.DebugMessage(string.Format(msg, targetUnit.Name, targetUnit.Position, targetUnit.GetDistanceTo(client.Hero)));
            }
        }
       
        [MessageHandler(PacketCmd.PKT_C2S_StartGame)]
        public static void HandleStartGameRequestMessage(StartGameRequestMessage message, LoLClient client)
        {
            client.Send(new StartGameMessage(0));

            client.Hero.Team.Send(new EnterVisionMessage(true, client.Hero.NetId, client.Hero.Position, client.Hero.WaypointsCollection.WaypointsIndex, client.Hero.WaypointsCollection.GetWaypoints(), client.Hero.Game.Map.Record.MiddleOfMap));

            float gameTime = client.Hero.Game.GameTime / 1000f;
            client.Send(new GameTimerMessage(0, gameTime));
            client.Send(new GameTimerUpdateMessage(0, gameTime));

            //client.Send(new FogUpdate2Message(client.Player.NetId, NetIdProvider.PopNextNetId()));
        }
        [MessageHandler(PacketCmd.PKT_C2S_CharLoaded, Channel.CHL_C2S)]
        public static void HandleCharLoadedMessage(CharLoadedMessage message, LoLClient client)
        {
            client.Hero.ReadyToSpawn = true;
            client.Hero.NetId = client.Hero.Game.NetIdProvider.PopNextNetId();
            client.Hero.Position = client.Hero.Game.Map.GetStartPosition(client.Hero);

            if (client.Hero.Game.CanStart)
            {
                client.Hero.Game.Start();
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

                    WaypointsReader wayPointsReader = new WaypointsReader(message.moveData, message.coordCount, client.Hero.Game.Map.Size);

                    client.Hero.Invoke(new Action(() =>
                   {
                       client.Hero.WaypointsCollection.SetWaypoints(wayPointsReader.Waypoints);
                       client.Hero.SendVision(new MovementAnswerMessage(0, Environment.TickCount, client.Hero.WaypointsCollection.GetWaypoints(), client.Hero.NetId,
                     client.Hero.Game.Map.Size), Channel.CHL_LOW_PRIORITY);

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
