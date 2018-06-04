using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Network;
using Legends.World.Entities.AI;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using Legends.World.Spells;
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
            client.Hero.DebugMessage(client.Hero.Position.ToString());
            client.Hero.AttentionPing(client.Hero.Position, client.Hero.NetId, PingTypeEnum.Ping_OnMyWay);
        }
        [Command("test")]
        public static void TestCommand(LoLClient client)
        {
            client.Hero.AIStats.AttackSpeed.SetBaseValue(1.60f);
            client.Hero.Stats.Health.Current += 100;
            client.Hero.UpdateStats();




        }
        [Command("speed")]
        public static void SpeedCommand(LoLClient client, float speed)
        {
            client.Hero.PlayerStats.MoveSpeed.SetBaseValue(speed);
            client.Hero.UpdateStats(true);
        }
        [Command("size")]
        public static void SizeCommand(LoLClient client, float size)
        {
            client.Hero.PlayerStats.ModelSize.SetBaseValue(size);
            client.Hero.UpdateStats(true);
        }
        [InDeveloppement(InDeveloppementState.HAS_BUG, "When player leave vision, the model is swap back.")]
        [Command("model")]
        public static void ModelCommand(LoLClient client, string model)
        {
            client.Hero.UpdateModel(model, false, 0);
        }
        [Command("skin")]
        public static void SkinCommand(LoLClient client, int skinId)
        {
            client.Hero.UpdateModel(client.Hero.Model, false, skinId);
        }
        [Command("vision")]
        public static void VisionCommand(LoLClient client)
        {
            string str = "I have vision on : ";
            str += Environment.NewLine;
            foreach (var unit in client.Hero.Team.GetVisibleUnits())
            {
                str += unit.Name + " distance: (" + unit.GetDistanceTo(client.Hero) + ")  ";

            }
            client.Hero.DebugMessage(str);
        }
    }
}
