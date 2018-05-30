using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Network;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends.World.Commands
{
    class CommandsRepertory
    {
        [Command("position")]
        public static void PositionCommand(LoLClient client)
        {
            client.Player.DebugMessage(client.Player.Position.ToString());
        }
        [Command("test")]
        public static void TestCommand(LoLClient client)
        {
          
        }
        [Command("speed")]
        public static void SpeedCommand(LoLClient client, float speed)
        {
            client.Player.PlayerStats.MoveSpeed.SetBaseValue(speed);
            client.Player.UpdateStats(true);
        }
        [Command("size")]
        public static void SizeCommand(LoLClient client,float size)
        {
            client.Player.PlayerStats.ModelSize.SetBaseValue(size);
            client.Player.UpdateStats(true);
        }
    }
}
